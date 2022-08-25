using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] Transform ResetPos;

    [SerializeField] int TurnSpeed;
    [SerializeField] int MoveSpeed;
    [SerializeField] int BoostSpeed;
    [SerializeField] ParticleSystem BoostPart;
    [SerializeField] float MaxBoost;
    public float boostAmount;

    bool NearGround;
    bool WkeyRepressed;
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            WkeyRepressed = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = ResetPos.position;
        }

        //boost
        if (Input.GetKey(KeyCode.LeftShift) && boostAmount > 0)
        {
            rb.AddForce(transform.forward * BoostSpeed * Time.deltaTime, ForceMode.Acceleration);
            Boosting = true;
            boostAmount -= Time.deltaTime;
            if (!BoostPart.isEmitting)
            {
                BoostPart.Play();
            }
        }else if(Input.GetKeyUp(KeyCode.LeftShift) || boostAmount <= 0) {
            BoostPart.Stop();
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
            else if (WkeyRepressed)
            {
                rb.AddTorque(transform.right * TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
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
                rb.AddTorque(transform.right * -TurnSpeed * Time.deltaTime, ForceMode.Acceleration);
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
            if (Physics.Raycast(booster.transform.position, -booster.transform.up, out GroundCheck, 14, FloatLayers))
            {
                NearGround = true;
                boostAmount += Time.deltaTime;
                boostAmount = Mathf.Clamp(boostAmount, 0, MaxBoost);
                break; 
            }

        }

        if (NearGround)
        {
            WkeyRepressed = false;
        }

        foreach (GameObject booster in FloatPoints)
        {
            RaycastHit hit;
            ParticleSystem particles = booster.GetComponentInChildren<ParticleSystem>();

            if (Physics.Raycast(booster.transform.position, -booster.transform.up, out hit, 14, FloatLayers))
            {

                particles.transform.position = hit.point;
                particles.transform.rotation = Quaternion.LookRotation(transform.TransformDirection(Vector3.forward), hit.normal);
                particles.Play();

                if (hit.distance <= 7)
                {
                    rb.AddForceAtPosition(booster.transform.up * 500 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
                }
                else if (hit.distance > 7)
                {
                    rb.AddForceAtPosition(booster.transform.up * -1000 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
                }

            }
            else if (NearGround && !Boosting)
            {
                rb.AddForceAtPosition(booster.transform.up * -5000 * Time.deltaTime, booster.transform.position, ForceMode.Acceleration);
            }else
            {
                particles.Stop();
            }
        }
    }
}
