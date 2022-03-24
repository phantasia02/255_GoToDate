using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpinImage : MonoBehaviour
{
    [SerializeField] float Speed;
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z + Time.deltaTime * Speed);
    }
}
