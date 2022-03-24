using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBodySkin : MonoBehaviour
{
    [SerializeField] GameObject[] Skins;
    [SerializeField] Transform SkinParent;
    void Start()
    {
        GameObject Target = Skins[Random.Range(0, Skins.Length)];
        Instantiate(Target, SkinParent);
    }


}
