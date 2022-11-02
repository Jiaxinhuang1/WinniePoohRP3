using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject target;
    private GameObject player;
    private AudioSource clickSound;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        clickSound = gameObject.GetComponent<AudioSource>();
    }

    public void MoveTo()
    {
        if (!DialogueManager.instance.isDialogueOn)
        {
            clickSound.Play();
            float dist = Vector3.Distance(player.transform.position, target.transform.position);
            LeanTween.move(player, new Vector2(target.transform.position.x, target.transform.position.y), dist / 500);
        }
    }
}
