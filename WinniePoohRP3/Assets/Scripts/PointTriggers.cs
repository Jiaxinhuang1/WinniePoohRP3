using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Entered Trigger");
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Exited Trigger");
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
