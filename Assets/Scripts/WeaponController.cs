using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] List<WeaponBase> EquippedWeapons;
    int FloatLayers = 1 << 3;

    void Start()
    {
        FloatLayers = ~FloatLayers;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, FloatLayers))
        {
            foreach (WeaponBase weapon in EquippedWeapons)
            {
                weapon.transform.LookAt(hit.point);
            }
        }

    }
}
