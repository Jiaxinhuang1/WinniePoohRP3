using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHole : MonoBehaviour
{
    public GameObject holeCutscene;
    public bool NeedsFight;
    public int holeNum;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("HoleNumber", 0) == holeNum)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = this.gameObject.transform.position;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (NeedsFight)
            {
                PlayerPrefs.SetInt("HoleNumber", holeNum);
                SceneManagerScript.instance.ChangeScene("Combat");
            }
            else
            {
                PlayerPrefs.SetInt("HoleNumber", holeNum);
                this.gameObject.SetActive(false);
                holeCutscene.SetActive(true);
                //SceneManagerScript.instance.ChangeScene("Cave");
            }
        }
    }




}
