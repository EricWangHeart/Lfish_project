using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFramework;
using PUIType;
public class TipPanel : BasePanel
{
    private const string close = "close";
    public TipPanel()
    {
        resName = "BasePrefabs/TipPanel";
        resident = false;
        selfType = WindowType.TipsWindow;
        scenesType = ScenesType.None;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnAddListener()
    {
        base.OnAddListener();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

    }

    protected override void OnRemoveListener()
    {
        base.OnRemoveListener();
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        foreach (var button in buttonList)
        {
            switch (button.name)
            {
                case close:
                    button.onClick.AddListener(OnCloseClick);
                    break;
              
            }
        }
    }

    private void OnCloseClick()
    {
        UIManager.Instance.CloseWindow(WindowType.TipsWindow);
    }
}
