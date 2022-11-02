using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;
public class DialogueController : MonoBehaviour
{
    //public HubDialogueSO hubDialogueSO;
    [Header("Regular Box")]
    private Queue<string> sentences;
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    [Header("Thought Bubble")]
    public GameObject dialogueBubble;
    public TextMeshProUGUI poohText;

    private bool oldInput;
    private bool isPooh;
    private bool input;
    public bool isDialogueOn;
    private bool isRunning;
    public float textSpeed;
    private int clipNum;
    private AudioClip[] VOclips;
    public AudioSource VOsource;

    public UnityEvent EndDialogueFunction;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
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

    public void StartDialogue(string name, Sprite image, string[] dialogue, bool isPoohTalking, AudioClip[] clips)
    {
        VOclips = clips;
        clipNum = 0;
        if (isPoohTalking)
        {
            isPooh = true;
        }
        else
        {
            isPooh = false;
        }
        if (!isDialogueOn)
        {
            isDialogueOn = true;
            sentences.Clear();
            if (!isPooh)
            {
                LeanTween.scaleY(dialoguePanel, 1, 0.2f);
                LeanTween.alpha(characterImage.GetComponent<RectTransform>(), 1f, 0.2f).setDelay(0.1f);
                nameText.text = name;
                characterImage.sprite = image;
            }
            else
            {
                LeanTween.scaleY(dialogueBubble, 1, 0.2f);
            }
            foreach (string sentence in dialogue)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence()
    {
        if (!isRunning && !VOsource.isPlaying)
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

            //Play the voice over clips
            VOsource.clip = VOclips[clipNum];
            VOsource.Play();
            if (clipNum < VOclips.Length - 1)
            {
                clipNum++;
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        isRunning = true;
        TextMeshProUGUI talkText;
        if (isPooh)
        {
            talkText = poohText;
        }
        else
        {
            talkText = dialogueText;
        }

        talkText.text = sentence;
        talkText.maxVisibleCharacters = 0;
        for (float t = 0; talkText.maxVisibleCharacters < sentence.Length; t += Time.deltaTime)
        {
            talkText.maxVisibleCharacters = (int)(t * textSpeed);
            /*
            if (input && !oldInput)
            {
                oldInput = input;
                talkText.maxVisibleCharacters = sentence.Length;
            }
            */
            yield return null;
        }
        //VOsource.Stop();  Stop once all the text is finished ()
        isRunning = false;

    }
    public void EndDialogue()
    {
        if (isDialogueOn)
        {
            Debug.Log("Dialogue Ended");
            if (isPooh)
            {
                LeanTween.scaleY(dialogueBubble, 0, 0.2f);
            }
            else
            {
                LeanTween.scaleY(dialoguePanel, 0, 0.2f);
            }
            EndDialogueFunction.Invoke();
            isDialogueOn = false;
        }
    }
}
