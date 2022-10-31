using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public bool isGrounded;
    private float originalY;

    public void Start()
    {
        isGrounded = true;
        originalY = gameObject.GetComponent<RectTransform>().position.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGrounded = false;
                var seq = LeanTween.sequence();
                seq.append(LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 100, 0.25f));
                seq.append(LeanTween.moveY(gameObject.GetComponent<RectTransform>(), originalY, 0.25f).setOnComplete(() => isGrounded = true));
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Carrot")
       {
            Debug.Log("Collecting");
            Destroy(collision.gameObject);
            GameManager.instance.CollectCarrot();

       }
    }
}
