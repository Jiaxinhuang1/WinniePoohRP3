using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBehavoir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float moveTime = Random.Range(1.5f, 4f); 
        LeanTween.moveX(this.gameObject.GetComponent<RectTransform>(), -1000, moveTime).setDestroyOnComplete(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
