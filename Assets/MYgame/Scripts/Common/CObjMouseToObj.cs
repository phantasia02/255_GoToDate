using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CObjMouseToObj : MonoBehaviour
{
    public  void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }

    public void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    }

    public void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag");
    }

    public void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
    }
}
