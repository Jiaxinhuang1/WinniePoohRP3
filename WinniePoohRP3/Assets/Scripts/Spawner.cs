using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform firstPoint;
    private Transform secondPoint;
    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        firstPoint = gameObject.transform.GetChild(0).transform;
        secondPoint = gameObject.transform.GetChild(1).transform;
        StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnObjects()
    {
        while (true)
        {
            float randNum = Random.Range(0, 10);
            float xPos, yPos;
            if (randNum < 4)
            {
                xPos = firstPoint.position.x;
                yPos = firstPoint.position.y;
            }
            else
            {
                xPos = secondPoint.position.x;
                yPos = secondPoint.position.y;
            }
            Vector3 position = new Vector3(xPos, yPos, 0);
            GameObject item = Instantiate(Resources.Load("Carrot"), position, Quaternion.identity) as GameObject;
            item.transform.SetParent(canvas.transform);
            item.transform.SetAsFirstSibling();
            yield return new WaitForSeconds(Random.Range(GameManager.instance.minSpawnSpeed, 1.5f));
        }
    }
}
