using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    //char
    [SerializeField] Image CharIcon;
    [SerializeField] Sprite CharIconDefault;
    [SerializeField] TextMeshProUGUI CharName;

    //text
    [SerializeField] TextMeshProUGUI DialogueText;

    [SerializeField] GameObject DialoguePanel;

    bool Playing = false;

    public Queue<Dialogue> dialogues;

    void Start()
    {
        dialogues = new Queue<Dialogue>();
    }

    public void AddDialogueToQueue(Dialogue ToAdd)
    {
        if(ToAdd.Char == null)
        {
            ToAdd.Char = CharIconDefault;
        }
        dialogues.Enqueue(ToAdd);
        CheckQueue();
    }

    int CheckQueue()
    {
        if (!Playing && dialogues.Count > 0)
        {
            StartCoroutine("PlayNext");
        }else
        {
            Debug.LogWarning("Could not start next dialogue, dialogue may already be playing, or there may be no more dialogue in the queue.");
        }
        return dialogues.Count;
    }

    IEnumerator PlayNext()
    {
        //DialoguePanel.SetBool("Open", true);
        DialoguePanel.gameObject.SetActive(true);
        Dialogue DialogueToPlay = dialogues.Dequeue();
        CharName.text = DialogueToPlay.Name;
        CharIcon.sprite = DialogueToPlay.Char;
        Playing = true;

        DialogueText.text = null;

        for (int i = 0; i < DialogueToPlay.Text.Length; i++)
        {
            DialogueText.text += DialogueToPlay.Text[i];
            //DialogueText.gameObject.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.075f);
        }
        yield return new WaitForSeconds(3);
        //Debug.Log(DialoguePanel.GetBool("Open"));
        //DialoguePanel.SetBool("Open", false);
        Playing = false;
        DialoguePanel.SetActive(false);
        Debug.Log("Closing Window");


        CheckQueue();
    }

}

public class Dialogue
{
    public Sprite Char;
    public string Name;
    public string Text;
    public bool AutoSkip;
    public Dialogue (string text, string name = "???", bool autoskip = true, Sprite character = null)
    {
        Text = text;
        Name = name;
        Char = character;
    }
}