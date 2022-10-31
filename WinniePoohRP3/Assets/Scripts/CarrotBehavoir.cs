using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBehavoir : MonoBehaviour
{
    public bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        float moveTime = Random.Range(1.5f, 4f); 
        LeanTween.moveX(this.gameObject.GetComponent<RectTransform>(), -1000, moveTime).setDestroyOnComplete(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
