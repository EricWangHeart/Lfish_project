using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;

public class GameManager_Demo : Singleton<GameManager_Demo>
{

    
    //执行读取存档，初始化等操作
    public void GameStart()
    {
        
    }

    public void GameOver()
    {
        //开启死亡界面
        Time.timeScale = 0;
    }

    public void RegisterGameEvent()
    {
        //注册游戏事件
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        UIManager.Instance.OpenWindow(PUIType.WindowType.PauseWindow);
    }

    

}
