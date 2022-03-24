using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarStatus
{
    Lock = 0,
    Unlock = 1,
    Bought = 2
}

[System.Serializable]
public class PlayerOwnCar
{
    public CGGameSceneData.EPlayerTrailerType Index = CGGameSceneData.EPlayerTrailerType.ePotoType;
    public CarStatus Status;
    public bool IsSeen;

    public PlayerOwnCar(CGGameSceneData.EPlayerTrailerType SetIndex, CarStatus _Status, bool _IsSeen)
    {
        Index = SetIndex;
        SetData(_Status, _IsSeen);
    }

    public void SetData(CarStatus _Status, bool _IsSeen)
    {
        Status = _Status;
        IsSeen = _IsSeen;
    }

    public void Copy(PlayerOwnCar origin)
    {
        Status = origin.Status;
        IsSeen = origin.IsSeen;
    }
}
