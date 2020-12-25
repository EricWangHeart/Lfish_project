using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using PUIType;
using System;
using PEventCenter;

public class LoadingPanel : BasePanel
{
    private ProgressBar bar;
    private const string loadingState = "loadingState";
    public LoadingPanel()
    {
        resName = "BasePrefabs/LoadingPanel";
        resident = false;
        selfType = WindowType.LoadingWindow;
        scenesType = ScenesType.Begin;
    }
    protected override void Awake()
    {
        base.Awake();
        bar = UITool.GetUIComponent<ProgressBar>(transform.gameObject, "LoadingBar");
    }
    protected override void OnAddListener()
    {
        base.OnAddListener();
        EventCenter.AddListener<float>(PEventCenter.PEventType.Loading,Loading);
        EventCenter.AddListener(PEventCenter.PEventType.LoadComplete,LoadingComplete);
    }
    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
        EventCenter.RemoveListener<float>(PEventCenter.PEventType.Loading, Loading);
        EventCenter.RemoveListener(PEventCenter.PEventType.LoadComplete, LoadingComplete);
    }
   
    public void Loading(float percentage)
    {
        bar.SetValue(percentage);
        SetText(loadingState, "加载中");
    }

    public void LoadingComplete()
    {
        SetText(loadingState, "加载完成");
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
    }

    
}
