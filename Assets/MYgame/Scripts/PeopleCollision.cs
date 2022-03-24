using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleCollision : MonoBehaviour
{

    void Update()
    {
        Collider[] PlayerColliders = Physics.OverlapSphere(transform.position, 0.5f, StaticGlobalDel.g_PlayerMask);
        if (PlayerColliders.Length > 0)
        {
            Destroy(gameObject);
        }
    }

}
