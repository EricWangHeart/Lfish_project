using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PUIType
{

    /// <summary>
    /// 窗体类型
    /// </summary>
    public enum WindowType
    {
       
        StartWindow,
        LoadingWindow,
        PauseWindow,
        GameWindow,
        SettingWindow,
        MainWindow,
        TipsWindow,
        DialogueWindow,
        HUDPanel,
        BeginWindow
    }
    /// <summary>
    /// 场景类型,目的:提供根据场景类型进行预加载
    /// </summary>
    public enum ScenesType
    {
        None,
        Begin,
        Battle
    }


}

