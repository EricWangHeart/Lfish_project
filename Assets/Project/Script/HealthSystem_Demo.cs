using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PEventCenter;
using System;

public class HealthSystem_Demo : MonoBehaviour
{
    public Image healthBar;
    public Image infectBar;

    private void Awake()
    {
        EventCenter.AddListener<float ,float>(PEventType.Damage, UpdateHealthBar);
        EventCenter.AddListener<float, float>(PEventType.Infect, UpdateInfectBar);
    }

    private void UpdateHealthBar(float currentValue,float maxValue)
    {
        healthBar.fillAmount = currentValue / maxValue;
    }
    private void UpdateInfectBar(float currentValue, float maxValue)
    {
        infectBar.fillAmount = currentValue / maxValue;
    }


}
