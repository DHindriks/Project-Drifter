using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] List<GameObject> FloatPoints;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * 20, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.up * -25, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * 25, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(transform.right * -25, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(transform.right * 25, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(transform.forward * -25, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(transform.forward * 25, ForceMode.Acceleration);
        }
    }
}
