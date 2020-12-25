using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image bar;
    /// <summary>
    /// 设置进度条填充百分比 value为填充百分比
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        bar.fillAmount = value;
        //bar.DOFillAmount(value, 0.2f);
    }

}
