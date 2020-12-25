using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PEventCenter;
public class LevelManager_Demo : MonoBehaviour
{

    public GameObject CharacterObject;
    public Transform initSpwanPoint;
    private Transform currentSpawnPoint;
    private GameObject _characterObject;
    private Health_Demo _characterHealth;

    private void Awake()
    {
        if (initSpwanPoint != null)
        {
            currentSpawnPoint = initSpwanPoint;
        }
        else
        {
            Debug.LogError("出生点不存在");
        }
       
        RegisterGameEvent();
    }

    private void Start()
    {
        _characterObject = Instantiate(CharacterObject, initSpwanPoint.position, Quaternion.identity);
    }


    private void RegisterGameEvent()
    {
        EventCenter.AddListener<Transform>(PEventType.ChangeSpwanPoint,ChangeSpawmPoint);
        EventCenter.AddListener(PEventType.Revive, Revive);
       
    }

    private void ChangeSpawmPoint(Transform position)
    {
        currentSpawnPoint = position;
    }

    private void Revive()
    {
        _characterObject.transform.position = currentSpawnPoint.position;
       
    }


}
