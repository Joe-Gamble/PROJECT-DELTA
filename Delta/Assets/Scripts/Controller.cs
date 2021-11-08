using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude < speed)
        {
            float move = Input.GetAxis("Vertical");
            if(move != 0)
            {
                rb.AddForce(0, 0, move * Time.fixedDeltaTime * 1000f);
            }
        }
    }
}
