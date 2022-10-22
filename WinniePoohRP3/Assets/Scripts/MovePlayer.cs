using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject target;
    private GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MoveTo()
    {
        float dist = Vector3.Distance(player.transform.position, target.transform.position);
        LeanTween.move(player, new Vector2(target.transform.position.x, target.transform.position.y), dist/500);
    }
}
