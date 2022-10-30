using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHole : MonoBehaviour
{
    public GameObject holeCutscene;
    public int holeNum;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("HoleNumber", 0) == holeNum)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = this.gameObject.transform.position;
            this.gameObject.SetActive(false);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("HoleNumber", holeNum);
            this.gameObject.SetActive(false);
            holeCutscene.SetActive(true);
        }
    }




}
