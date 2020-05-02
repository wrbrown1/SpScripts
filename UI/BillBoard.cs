using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] bool isBillboard = false;

    private void LateUpdate()
    {
        if (isBillboard)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}
