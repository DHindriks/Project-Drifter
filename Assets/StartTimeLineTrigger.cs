using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimeLineTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField]bool TPPlayer;
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
}
