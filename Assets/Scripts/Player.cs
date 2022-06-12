using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] int TurnSpeed;
    [SerializeField] int MoveSpeed;
    [SerializeField] int BoostSpeed;
    [SerializeField] float MaxBoost;
    public float boostAmount;

    bool NearGround;
    bool Boosting;
    Rigidbody rb;
    [SerializeField] List<GameObject> FloatPoints;
    [SerializeField] LayerMask FloatLayers;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        //boost
        if (Input.GetKey(KeyCode.LeftShift) && boostAmount > 0)
        {
            rb.AddForce(transform.forward * BoostSpeed * Time.deltaTime, ForceMode.Acceleration);
            Boosting = true;
            boostAmount -= Time.deltaTime;
        }else if(Input.GetKeyUp(KeyCode.LeftShift)) {
            Boosting = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.up * -TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (NearGround)
            {
                rb.AddForce(transform.forward * MoveSpeed * Time.deltaTime, ForceMode.Acceleration);
            }
            else
            {
                rb.AddTorque(transform.right * -TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (NearGround)
            {
                rb.AddForce(-transform.forward * MoveSpeed * Time.deltaTime, ForceMode.Acceleration);
            }
            else
            {
                rb.AddTorque(transform.right * TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (NearGround)
            {
                rb.AddForce(-transform.right * MoveSpeed * Time.deltaTime, ForceMode.Acceleration);
            }
            else
            {
                rb.AddTorque(transform.forward * -TurnSpeed * 2 * Time.deltaTime, ForceMode.Acceleration);
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (NearGround)
            {
                rb.AddForce(transform.right * MoveSpeed * Time.deltaTime, ForceMode.Acceleration);
            }
            else
            {
                rb.AddTorque(transform.forward * TurnSpeed * 2 * Time.deltaTime, ForceMode.Acceleration);
            }
        }

        FloatCraft();

    }

    void FloatCraft()
    {
        NearGround = false;
        foreach (GameObject booster in FloatPoints)
        {
            RaycastHit GroundCheck;
            if (Physics.Raycast(booster.transform.position, -booster.transform.up, out GroundCheck, 10, FloatLayers))
            {
                NearGround = true;
                boostAmount += Time.deltaTime;
                boostAmount = Mathf.Clamp(boostAmount, 0, MaxBoost);
            }
        }

        foreach (GameObject booster in FloatPoints)
        {
            RaycastHit hit;
            if (Physics.Raycast(booster.transform.position, -booster.transform.up, out hit, 10, FloatLayers))
            {
                if (hit.distance <= 5)
                {
                    rb.AddForceAtPosition(booster.transform.up * 500 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
                }
                else if (hit.distance > 5)
                {
                    rb.AddForceAtPosition(booster.transform.up * -1000 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
                }
            }
            else if (NearGround && !Boosting)
            {
                rb.AddForceAtPosition(booster.transform.up * -5000 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
            }
        }
    }
}
