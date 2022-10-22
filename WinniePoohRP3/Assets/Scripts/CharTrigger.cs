using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharTrigger : MonoBehaviour
{
    [TextArea(5,10)]
    public string[] dialogues;
    public bool isEvil;
    public GameObject cutscenePanel;
    public DialogueController dialogueScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk()
    {
        DialogueManager.instance.PlayDialogue(this.gameObject.name, this.gameObject.GetComponent<Image>().sprite, dialogues);
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
