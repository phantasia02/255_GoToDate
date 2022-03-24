using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Car : ScriptableObject
{
    public CGGameSceneData.EPlayerTrailerType Index;
    public int Price;
    public Sprite Image;
    public Sprite LockImage;

}
