using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

using UIFramework;
using PUIType;
using PEventCenter;

public class LoadingManager : MonoSingleton<LoadingManager>
{
    private  float loadPercentage;
    private AsyncOperation async = null;
    public  bool loadComplete = false;
    public float Percentage { get { return loadPercentage; } }
    
    /// <summary>
    /// 设置要跳转的场景名字
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        loadComplete = false;
        StartCoroutine(Load(sceneName));
    }
    IEnumerator Load(string sceneName)
    {
        UIManager.Instance.OpenWindow(WindowType.LoadingWindow);
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
            {
                loadPercentage = async.progress;
            }
            else
            {
                loadPercentage = 1.0f;
              
            }
           
            EventCenter.Broadcast<float>(PEventCenter.PEventType.Loading, loadPercentage);
            
            if (loadPercentage >= 0.9f)
            {
                EventCenter.Broadcast(PEventCenter.PEventType.LoadComplete);
                loadComplete = true;
                yield return new WaitForSecondsRealtime(0.5f);
                async.allowSceneActivation = true;
            }
           
            yield return null;
        }
     
    }
}
