using System.Collections;
using System.Collections.Generic;
using UIFramework;
using PUIType;
using UnityEngine;

public class DialoguePanel : BasePanel
{
    public readonly string DialogueInfo = "DialogueInfo";
    public readonly string Header = "Header";
    public readonly string Name = "Name";

    public DialoguePanel()
    {
        resName = "BasePrefabs/DialoguePanel";
        resident = false;
        selfType = WindowType.DialogueWindow;
        scenesType = ScenesType.None;
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
    }
}
