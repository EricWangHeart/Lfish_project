using System.Collections;
using System.Collections.Generic;
using UIFramework;
using PUIType;
using UnityEngine;

public class BeginPanel : BasePanel
{
    public BeginPanel()
    {
        resName = "BasePrefabs/BeginPanel";
        resident = true;
        selfType = WindowType.BeginWindow;
        scenesType = ScenesType.None;
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
    }
}
