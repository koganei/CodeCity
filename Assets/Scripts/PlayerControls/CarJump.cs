using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarJump : MonoBehaviour
{
    public float jumpMultiplier = 200f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody>().velocity += Vector3.up * jumpMultiplier * Time.deltaTime;
        }

        if (GetComponent<Rigidbody>().velocity.y > 0 && Input.GetKeyDown("s"))
        {
            GetComponent<Rigidbody>().velocity += Vector3.forward * (jumpMultiplier/2) * Time.deltaTime;
        }

        if (GetComponent<Rigidbody>().velocity.y > 0 && Input.GetKeyDown("w"))
        {
            GetComponent<Rigidbody>().velocity += Vector3.back * (jumpMultiplier/2) * Time.deltaTime;
        }
    }
}
