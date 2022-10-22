using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class DialogueController : MonoBehaviour
{
    //public HubDialogueSO hubDialogueSO;
    private Queue<string> sentences;
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    private bool oldInput;
    private bool input;
    public bool isDialogueOn;
    private bool isRunning;
    public float textSpeed;

    [HideInInspector] public string soSceneName;

    // Start is called before the first frame update
    void Start()
    {
        /* Testing substrings
        for (int i = 0; i < hubDialogueSO.firstDialogues.Length; i ++)
        {
            int separatorIndex = hubDialogueSO.firstDialogues[i].IndexOf(":");
            string name = hubDialogueSO.firstDialogues[i].Substring(0, separatorIndex);
            string dialogue = hubDialogueSO.firstDialogues[i].Substring(separatorIndex + 1);
            Debug.Log(name + " says" + dialogue);
        }
        */
        sentences = new Queue<string>();
        //StartDialogue(hubDialogueSO.firstDialogues);
        Debug.Log("YAY I STARTED");
    }

    // Update is called once per frame
    private void Update()
    {
        oldInput = input;
        input = Input.GetAxisRaw("MouseClick") > 0;
        if (isDialogueOn)
        {
            if (input && !oldInput)
            {
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(string name, Sprite image, string[] dialogue)
    {
        if (!isDialogueOn)
        {
            isDialogueOn = true;
            sentences.Clear();
            LeanTween.scaleY(dialoguePanel, 1, 0.2f);
            LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 1f, 0.2f).setDelay(0.1f);
            nameText.text = name;
            characterImage.sprite = image;
            foreach (string sentence in dialogue)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (!isRunning)
        {
            oldInput = input;

            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isRunning = true;

        dialogueText.text = sentence;

        dialogueText.maxVisibleCharacters = 0;

        for (float t = 0; dialogueText.maxVisibleCharacters < sentence.Length; t += Time.deltaTime)
        {
            dialogueText.maxVisibleCharacters = (int)(t * textSpeed);

            if (input && !oldInput)
            {
                // consume input
                oldInput = input;
                dialogueText.maxVisibleCharacters = sentence.Length;
            }
            yield return null;
        }

        isRunning = false;

    }
    public void EndDialogue()
    {
        if (isDialogueOn)
        {
            Debug.Log("Dialogue Ended");
            LeanTween.scaleY(dialoguePanel, 0, 0.2f);
            isDialogueOn = false;
        }
    }
}