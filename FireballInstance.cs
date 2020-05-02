using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballInstance : MonoBehaviour
{
    [SerializeField]
    Transform fireBall;
    [SerializeField]
    Transform startingPoint;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(fireBall, startingPoint.position, transform.rotation);
        }
    }
}
