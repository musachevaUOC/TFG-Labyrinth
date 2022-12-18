using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.layer == 5)
        {
            MeshRenderer mr = other.GetComponent<MeshRenderer>();
            mr.enabled = true;
            other.enabled = false;
        }
    }
}
