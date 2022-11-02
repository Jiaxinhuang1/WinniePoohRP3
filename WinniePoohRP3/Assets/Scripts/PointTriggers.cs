using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointTriggers : MonoBehaviour
{
    [Header("Timer")]
    public bool needsTimer;
    public Slider timeSlider;
    private float timeRemaining;
    private const float timerMax = 5f;
    public bool isTimerOn;
    // Start is called before the first frame update
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            timeSlider.gameObject.SetActive(true);
            timeSlider.value = CalculateSliderValue();
            if (timeRemaining <= 0)
            {
                GameObject firstActiveArrow = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject;
                for (int i = 0; i < gameObject.transform.GetChild(0).transform.childCount; i++)
                {
                    if (gameObject.transform.GetChild(0).transform.GetChild(i).gameObject.activeSelf == true)
                    {
                        firstActiveArrow = gameObject.transform.GetChild(0).transform.GetChild(i).gameObject;
                    }
                }
                firstActiveArrow.GetComponent<Button>().onClick.Invoke();
                isTimerOn = false;
            }
            else if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
        }

    }

    public void StartTimer()
    {
        timeRemaining = timerMax;
        isTimerOn = true;
    }

    public void StopTimer()
    {
        timeRemaining = timerMax;
        isTimerOn = false;
    }

    float CalculateSliderValue()
    {
        return (timeRemaining / timerMax);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if (needsTimer)
            {
                if (!DialogueManager.instance.isDialogueOn)
                {
                    timeSlider.gameObject.SetActive(true);
                    StartTimer();
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            if (needsTimer)
            {
                timeSlider.gameObject.SetActive(false);
                StopTimer();
            }
        }
    }
}
