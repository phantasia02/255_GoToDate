using UnityEngine;

public class VarRename : PropertyAttribute
{
    public string g_VarName;
    public int g_Index;
    public string[] g_StrList;

    public VarRename(string elementTitleVar)
    {
        Init();
        g_VarName = elementTitleVar;
    }

    public VarRename(int index)
    {
        Init();
        g_Index = index;
    }

    public VarRename(string[] strList)
    {
        Init();
        g_StrList = strList;
    }

    public VarRename(StaticGlobalDel.ECompleteBuilding[] EnumList)
    {
        Init();
        g_StrList = new string[EnumList.Length];
        for (int i = 0; i < EnumList.Length; i++)
            g_StrList[i] = EnumList[i].ToString();
    }

    public VarRename(StaticGlobalDel.EBrickColor[] EnumList)
    {
        Init();
        g_StrList = new string[EnumList.Length];
        for (int i = 0; i < EnumList.Length; i++)
            g_StrList[i] = EnumList[i].ToString();
    }

    public VarRename(CGGameSceneData.EOtherObj EnumMax)
    {
        Init();
        int lTempCount = (int)EnumMax;
        g_StrList = new string[lTempCount];
        for (int i = 0; i < lTempCount; i++)
            g_StrList[i] = ((CGGameSceneData.EOtherObj)i).ToString();
    }

    public VarRename(CSEPlayObj.ESE SEEnumMax)
    {
        Init();
        int lTempCount = (int)SEEnumMax;
        g_StrList = new string[lTempCount];
        for (int i = 0; i < lTempCount; i++)
            g_StrList[i] = ((CSEPlayObj.ESE)i).ToString();
    }

    public VarRename(CAudioManager.EBGM BGMEnumMax)
    {
        Init();
        int lTempCount = (int)BGMEnumMax;
        g_StrList = new string[lTempCount];
        for (int i = 0; i < lTempCount; i++)
            g_StrList[i] = ((CAudioManager.EBGM)i).ToString();
    }

    public VarRename(CSEPlayObj.EUpdateFuncType UpdateFuncTypeEnumMax)
    {
        Init();
        int lTempCount = (int)UpdateFuncTypeEnumMax;
        g_StrList = new string[lTempCount];
        for (int i = 0; i < lTempCount; i++)
            g_StrList[i] = ((CSEPlayObj.EUpdateFuncType)i).ToString();
    }

    public VarRename(CReadyGameWindow.EChildGroup ChildGroupMax)
    {
        Init();
        int lTempCount = (int)ChildGroupMax;
        g_StrList = new string[lTempCount];
        for (int i = 0; i < lTempCount; i++)
            g_StrList[i] = ((CReadyGameWindow.EChildGroup)i).ToString();
    }

    public VarRename(CActorSetSkin.ESlinType Max)
    {
        Init();
        int lTempCount = (int)Max;
        g_StrList = new string[lTempCount];
        for (int i = 0; i < lTempCount; i++)
            g_StrList[i] = ((CActorSetSkin.ESlinType)i).ToString();
    }

    private void Init()
    {
        g_VarName = "";
        g_StrList = new string[0];
        g_Index = -1;
    }
}