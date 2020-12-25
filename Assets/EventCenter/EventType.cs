namespace PEventCenter
{
    //自定义
    public enum PEventType
    {
        GetWindowType,
        //加载
        Loading,
        //加载完成
        LoadComplete,
        //
        OpenMap,
        //
        CloseMap,
        //游戏结束
        GameOver,
        ChangeSpwanPoint,
        Revive,
        AddSkillPoint,
        SubSkillPoint,
        ResetMaxPointNum,
        InitSkillPoint,
        ClearPointNum,
        ResetDash,
        UnEnableDash,
        SetGroundDash,
        BossActive,
        UpdateHealth,
        UpdateSkillPointDisplay,
        Damage,
        Infect,
        SetPlayerState

    }
}