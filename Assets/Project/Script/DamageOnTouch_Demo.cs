using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;
using MoreMountains.CorgiEngine;
using PEventCenter;

public class DamageOnTouch_Demo : MonoBehaviour
{
    public enum DamageType{ Normal, Infect }
    /// the possible ways to add knockback : noKnockback, which won't do nothing, set force, or add force
    public enum KnockbackStyles { NoKnockback, SetForce, AddForce }
    [Tooltip("攻击对象")]
    [Header("目标")]
    public LayerMask TargetMasks;
    [Header("对敌人伤害")]
    public DamageType type = DamageType.Normal;
    public float DamageValue = 0;
    public float InfectValue = 0;
    [Tooltip("击退方式")]
    public KnockbackStyles DamageCausedKnockbackType = KnockbackStyles.SetForce;
    /// The force to apply to the object that gets damaged
    [Tooltip("击退冲量")]
    public Vector2 DamageCausedKnockbackForce = new Vector2(10, 0);
    [Tooltip("无敌时间")]
    public float invincibilityDuration;
    [Header("自我伤害")]
    [Tooltip("无敌时间")]
    public float DamageTakenDamageable = 0;
    public float DamageTakenNoDamageable = 0;
    public float DamageTakenInvincibilityDuration;
    [Tooltip("攻击反馈")]
    [Header("反馈")]
    public MMFeedbacks HitDamageableFeedback;
    public MMFeedbacks HitNonDamageableFeedback;
    [MMReadOnly]
    public GameObject Owner;



    private WaitForSeconds DamageCauseDelay;
    private WaitForSeconds DamageTakenDelay;
    private Collider2D _collidingCollider;
    private Health_Demo _collidingHealth;
    private CorgiController _colliderController;
    private Health_Demo _health;
    private Vector2 _knockbackForce;
    
    private void Awake()
    {
        DamageCauseDelay = new WaitForSeconds(invincibilityDuration);
        DamageTakenDelay = new WaitForSeconds(DamageTakenInvincibilityDuration);
        _health = this.gameObject.MMGetComponentNoAlloc<Health_Demo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Colliding(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Colliding(collision);
    }

    public void Colliding(Collider2D collider)
    {
        if (!MMLayers.LayerInLayerMask(collider.gameObject.layer, TargetMasks))
        {
            return;
        }
        _collidingCollider = collider;
        _collidingHealth = collider.gameObject.MMGetComponentNoAlloc<Health_Demo>();
        if (_collidingHealth != null)
        {
            OnCollideWithDamage(_collidingHealth);
        }
        else
        {
            OnCollideWithNoDamage();
        }
    }

    private void OnCollideWithDamage(Health_Demo health)
    {
        _colliderController = health.gameObject.MMGetComponentNoAlloc<CorgiController>();
        //如果未检测到控制器，则不击退
        if(_colliderController!=null)
        {
            ApplyDamageCauseKnockback();
        }
        //伤害反馈
        HitDamageableFeedback?.PlayFeedbacks();
        //检测伤害类型是感染还是正常伤害
        switch(type)
        {
            case DamageType.Normal:
                _collidingHealth.Damage(DamageValue, DamageCauseDelay);
                break;
            case DamageType.Infect:
                _collidingHealth.Infect(InfectValue, DamageCauseDelay);
                break;
        }
        //自我伤害
        SelfDamage(DamageTakenDamageable);
    }

    private void OnCollideWithNoDamage()
    {
        if (DamageTakenNoDamageable > 0) 
        {
            HitNonDamageableFeedback?.PlayFeedbacks();
            SelfDamage(DamageTakenNoDamageable);
        }
    }

    private void ApplyDamageCauseKnockback()
    {
        if ((_colliderController != null) &&
           (DamageCausedKnockbackForce != Vector2.zero) &&
           (!_collidingHealth.TemporaryInvulnerable) &&
           (!_collidingHealth.Invulnerable))
        {
            _knockbackForce.x = DamageCausedKnockbackForce.x;
            if (Owner == null) 
            {
                Owner = this.gameObject;
            }
            Vector2 relativePosition = _colliderController.transform.position - Owner.transform.position;
            _knockbackForce.x *= Mathf.Sign(relativePosition.x);
            _knockbackForce.y = DamageCausedKnockbackForce.y;

            if (DamageCausedKnockbackType == KnockbackStyles.SetForce)
            {
                _colliderController.SetForce(_knockbackForce);
            }
            if (DamageCausedKnockbackType == KnockbackStyles.AddForce)
            {
                _colliderController.AddForce(_knockbackForce);
            }
        }    
    }

    private void SelfDamage(float damageValue)
    {
        if(_health!=null)
        {
            _health.Damage(damageValue, DamageTakenDelay);
        }
    }
}
