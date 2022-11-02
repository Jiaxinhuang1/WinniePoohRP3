using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    private DialogueController dialogueScript;
    public bool isDialogueOn;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        dialogueScript = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueController>();
    }

    private void Update()
    {
        isDialogueOn = dialogueScript.isDialogueOn;
    }

    public void PlayDialogue(string name, Sprite image, string[] dialogue, bool isPooh, AudioClip[] clips)
    {
        dialogueScript.StartDialogue(name, image, dialogue, isPooh, clips);
    }
}
