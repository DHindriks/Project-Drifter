using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] int turnSpeed;

    Rigidbody rb;
    [SerializeField] List<GameObject> FloatPoints;
    int FloatLayers = 1 << 3;
    // Start is called before the first frame update
    void Start()
    {
        FloatLayers = ~FloatLayers;
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        //boost
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(transform.forward * turnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.up * -turnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * turnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddTorque(transform.right * -turnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddTorque(transform.right * turnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(transform.forward * -turnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(transform.forward * turnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }

        foreach(GameObject booster in FloatPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(booster.transform.position, -booster.transform.up, out hit, 10, FloatLayers))
            {
                if (hit.distance <= 5)
                {
                    rb.AddForceAtPosition(booster.transform.up * 500 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
                }else if (hit.distance > 5)
                {
                    rb.AddForceAtPosition(booster.transform.up * -500 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
                }
            }else
            {
                rb.AddForceAtPosition(booster.transform.up * -500 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
            }
        }
    }
}
