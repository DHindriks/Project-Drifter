using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class StartTimeLineTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField]bool TPPlayer;
    [SerializeField] DialogueSystem dialogueSystem;
    GameObject Player;
    bool Activated = false;
    void OnTriggerEnter(Collider other)
    {
        if (!Activated)
        {
            Player = other.gameObject;
            director.Play();
            Activated = true;
        }
    }

    public void TeleportPlayer(GameObject @object)
    {
        if (TPPlayer)
        {
            Player.transform.position = transform.position;
        }
    }

    public void ToggleMatEmission(Material mat)
    {
        mat.DisableKeyword("_EMISSION");
    }

    public void addDialogue(string Text)
    {
        dialogueSystem.AddDialogueToQueue(new Dialogue(Text));
    }

    public void LoadScene(string load)
    {
        SceneManager.LoadScene(load);
    }
}
