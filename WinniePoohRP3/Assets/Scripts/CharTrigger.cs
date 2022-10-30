using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharTrigger : MonoBehaviour
{
    public string nameDia;
    public Sprite imageDia;
    [TextArea(5, 10)]
    public string[] dialogues;
    public bool isEvil;
    public GameObject cutscenePanel;
    private DialogueController dialogueScript;
    // Start is called before the first frame update
    void Start()
    {
        dialogueScript = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk()
    {
        DialogueManager.instance.PlayDialogue(nameDia, imageDia, dialogues);
        if (isEvil)
        {
            dialogueScript.EndDialogueFunction.AddListener(() => cutscenePanel.SetActive(true));
        }
        else
        {
            dialogueScript.EndDialogueFunction.AddListener(() => Debug.Log("Ending Dialogue function call"));
        }
    }
}
