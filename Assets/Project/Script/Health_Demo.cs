using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Feedbacks;
using PEventCenter;

public class Health_Demo : MonoBehaviour
{
    [Header("生命值和感染值")]
    public float InitialHealth = 100;
    public float InitialInfection = 100;
    [Tooltip("当该参数为true时，玩家不会受到伤害")]
    public bool Invulnerable = false;

    [Header("伤害")]
    public MMFeedbacks DamageFeedbacks;

    [Header("感染")]
    public MMFeedbacks InfectFeedbacks;

    [Header("死亡")]
    public float delayOfDead = 0;
    [Tooltip("死亡后关闭重力")]
    public bool GravityOffOnDeath = false;
    [Tooltip("死亡后重置冲量（例如冲刺，击退效果）")]
    public bool ResetForcesOnDeath = false;
    public MMFeedbacks DeathFeedbacks;
 
    [MMFReadOnly]
    public bool TemporaryInvulnerable = false;

    private float currentHealth;
    private float currentInfect;
    private Character _character;
    private Animator _animator;
    private CorgiController _controller;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        Initialization();
        RegisiterGameEvent();
       
    }
    //初始化角色数据
    private void Initialization()
    {
        _character = GetComponent<Character>();
        _animator = _character.CharacterAnimator;
        _controller = GetComponent<CorgiController>();
        _collider2D = GetComponent<Collider2D>();
        _spriteRenderer = _character.CharacterModel.GetComponent<SpriteRenderer>();
        currentHealth = InitialHealth;
        TemporaryInvulnerable = false;
        currentInfect = 0;
    }
    //注册游戏事件
    private void RegisiterGameEvent()
    {
        EventCenter.Broadcast<float, float>(PEventType.Damage, currentHealth, InitialHealth);
        EventCenter.Broadcast<float, float>(PEventType.Infect, currentInfect, InitialInfection);
    }
    //受伤
    public void Damage(float damageValue,WaitForSeconds invincibilityDuration)
    {
        if (TemporaryInvulnerable || Invulnerable) 
        {
            return;
        }

        currentHealth -= damageValue;

        if (currentHealth < currentInfect || currentHealth <= 0) 
        {
            currentHealth = 0;
        }

        if (invincibilityDuration != null) 
        {
            DamageDisabled();
            StartCoroutine(DamageEnabled(invincibilityDuration));
        }

        if (_animator != null) 
        {
            _animator.SetTrigger("Damage");
        }

        DamageFeedbacks?.PlayFeedbacks();

        if (_character.CharacterType == Character.CharacterTypes.Player)
        {
            EventCenter.Broadcast<float, float>(PEventType.Damage, currentHealth, InitialHealth);
        }
        if (currentHealth <= 0) 
        {
            //GameOver
            Kill();
        }
    }

    public void Kill()
    {

        DeathFeedbacks?.PlayFeedbacks();

        DamageDisabled();

        if (_controller != null)
        {
            _controller.CollisionsOff();

            if (_collider2D != null)
            {
                _collider2D.enabled = false;
            }
            _controller.ResetParameters();

            if (GravityOffOnDeath)
            {
                _controller.GravityActive(false);
            }
            
            if (ResetForcesOnDeath)
            {
                _controller.SetForce(Vector2.zero);
            }
        }

        // if we have a character, we want to change its state
        if (_character != null)
        {
            // we set its dead state to true
            _character.ConditionState.ChangeState(CharacterStates.CharacterConditions.Dead);
            _character.Reset();

            // if this is a player, we quit here
            if (_character.CharacterType == Character.CharacterTypes.Player)
            {
                //EventCenter.Broadcast(PEventType.GameOver);
                TemporaryInvulnerable = true;
                return;
            }
        }

        if (delayOfDead > 0f)
        {
            Invoke("DestroyProject", delayOfDead);
        }
        else
        {
            DestroyGameObject();
        }
    }

    //实用技能时减少血量
    public void DecreaseHealthValue(float value)
    {
        currentHealth -= value;
        if (currentHealth < currentInfect || currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (_character.CharacterType == Character.CharacterTypes.Player)
        {
            EventCenter.Broadcast<float, float>(PEventType.Damage, currentHealth, InitialHealth);
        }
    }
    //感染
    public void Infect(float infectValue, WaitForSeconds invincibilityDuration)
    {
        if (TemporaryInvulnerable || Invulnerable)
        {
            return;
        }
        currentInfect += infectValue;
        if (currentInfect >= InitialInfection) 
        {
            currentInfect = InitialInfection;
        }

        if (invincibilityDuration != null)
        {
            DamageDisabled();
            StartCoroutine(DamageEnabled(invincibilityDuration));
        }
        if (_animator != null)
        {
            _animator.SetTrigger("Damage");
        }
        InfectFeedbacks?.PlayFeedbacks();
        if (_character.CharacterType == Character.CharacterTypes.Player)
        {
            EventCenter.Broadcast<float, float>(PEventType.Infect, currentInfect, InitialInfection);
        }
    }

    //治疗
    public void Treat(float treatValue)
    {
        currentHealth += treatValue;
        if(currentHealth>=InitialHealth)
        {
            currentHealth = InitialHealth;
        }
        if (_character.CharacterType == Character.CharacterTypes.Player)
        {
            EventCenter.Broadcast<float, float>(PEventType.Damage, currentHealth, InitialHealth);
        }
    }
    //重置感染值
    public void ResetInfection()
    {
        currentInfect = 0;

        if (_character.CharacterType == Character.CharacterTypes.Player)
        {
            EventCenter.Broadcast<float, float>(PEventType.Infect, currentInfect, InitialInfection);
        }
    }

    public void Revive()
    {
        if (_collider2D != null) 
        {
            _collider2D.enabled = true;
        }
        if (_controller != null)
        {
            _controller.CollisionsOn();
            _controller.GravityActive(true);
            _controller.SetForce(Vector2.zero);
            _controller.ResetParameters();
        }
        if (_character != null) 
        {
            _character.ConditionState.ChangeState(CharacterStates.CharacterConditions.Normal);
        }

        Initialization();

        if(_character.CharacterType == Character.CharacterTypes.Player)
        {
            GameManager_Demo.Instance.GameOver();
            EventCenter.Broadcast<float, float>(PEventType.Damage, currentHealth, InitialHealth);
            EventCenter.Broadcast<float, float>(PEventType.Infect, currentInfect, InitialInfection);
        }
    }

    public void DamageDisabled()
    {
        TemporaryInvulnerable = true;
    }

    public void DamageEnabled()
    {
        TemporaryInvulnerable = false;
    }

    public IEnumerator DamageEnabled(WaitForSeconds delay)
    {
        yield return delay;
        TemporaryInvulnerable = false;
    }

    private void DestroyGameObject()
    {
        gameObject.SetActive(false);   
    }
}
