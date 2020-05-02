using SP.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    int enters = 0;
    private void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    public void StartSwing()
    {
        GetComponent<BoxCollider>().enabled = true;
        print("s");
    }
    public void EndSwing()
    {
        GetComponent<BoxCollider>().enabled = false;
        print("e");
    }

}
