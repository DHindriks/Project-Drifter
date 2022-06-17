using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDialogue : MonoBehaviour
{
    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] bool PlayOnce;
    [SerializeField] float CooldownSeconds = 0;
    float CurrentCooldownSeconds;
    bool enabledd = true;

    [SerializeField] string Message;
    [SerializeField] string Name;
    [SerializeField] bool AutoSkip;
    [SerializeField] Sprite CharIcon;



    void OnTriggerEnter(Collider other)
    {
        if(enabledd && CurrentCooldownSeconds <= 0 && other.tag == "Player")
        {
            dialogueSystem.AddDialogueToQueue(new Dialogue(Message, Name, AutoSkip, CharIcon));
            if (PlayOnce)
            {
                enabledd = false;
            }else
            {
                CurrentCooldownSeconds = CooldownSeconds;
            }
        }
    }

    private void Update()
    {
        if (CurrentCooldownSeconds > 0)
        {
            CurrentCooldownSeconds -= Time.deltaTime;
        }
    }
}
