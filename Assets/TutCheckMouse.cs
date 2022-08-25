using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutCheckMouse : MonoBehaviour
{
    bool Fading = false;

    // Update is called once per frame
    void Update()
    {
        if (!Fading && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            Fading = true;
            GetComponent<Image>().color = Color.green;
            GetComponent<Animator>().SetBool("Faded", true);
            Destroy(gameObject, 6);
        }
    }
}
