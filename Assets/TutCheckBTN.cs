using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutCheckBTN : MonoBehaviour
{
    bool Fading = false;

    // Update is called once per frame
    void Update()
    {
        if (!Fading && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            Fading = true;
            GetComponent<Image>().color = Color.green;
            GetComponent<Animator>().SetBool("Faded", true);
            Destroy(gameObject, 6);
        } 
    }
}
