using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoostCheck : MonoBehaviour
{
    [SerializeField] Transform ResetPos;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !Input.GetKey(KeyCode.LeftShift))
        {
            other.transform.root.position = ResetPos.position;
        }
    }
}
