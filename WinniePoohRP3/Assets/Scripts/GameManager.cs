using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum Difficulty { Easy, Medium, Hard }

    [Header("Difficulty Tracker")]
    public float minSpawnSpeed;
    public int difficultyLevel;
    public GameManager.Difficulty difficultyState;
    public GameObject losePanel;
    public GameObject winPanel;

    [Header("Lives")]
    public int lifeCount;
    public GameObject[] dead;

    [Header("Timer")]
    public Slider timeSlider;
    private float timeRemaining;
    private const float timerMax = 20f;
    public bool isTimerOn;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        int holeNum = PlayerPrefs.GetInt("HoleNumber", 0);
        PlayerPrefs.SetInt("DifficultyLevel", holeNum);
        difficultyLevel = PlayerPrefs.GetInt("DifficultyLevel", 1);
        ChangeDifficulty();
        isTimerOn = false;
        lifeCount = dead.Length;
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            timeSlider.value = CalculateSliderValue();
            if (timeRemaining <= 0)
            {
                if (lifeCount <= 0)
                {
                    losePanel.SetActive(false);
                
                }
                else
                {
                    winPanel.SetActive(true);
                }
                isTimerOn = false;
            }
            else if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
        }
    }

    float CalculateSliderValue()
    {
        return (timeRemaining / timerMax);
    }

    public void ChangeDifficulty()
    {
        if (difficultyLevel <= 1)
        {
            difficultyState = Difficulty.Easy;
        }
        else if (difficultyLevel == 2)
        {
            difficultyState = Difficulty.Medium;
        }
        else if (difficultyLevel == 3)
        {
            difficultyState = Difficulty.Hard;
        }
        switch (difficultyState)
        {
            case Difficulty.Easy:
                minSpawnSpeed = 2f;
                Debug.Log("Easy Level");
                Debug.Log("Spawn Speed: " + minSpawnSpeed);
                break;
            case Difficulty.Medium:
                minSpawnSpeed = 1f;
                Debug.Log("Medium Level");
                Debug.Log("Spawn Speed: " + minSpawnSpeed);
                break;
            case Difficulty.Hard:
                minSpawnSpeed = 0.5f;
                Debug.Log("Hard Level");
                Debug.Log("Spawn Speed: " + minSpawnSpeed);
                break;
        }
    }

    public void DecreaseLive()
    {
        if (lifeCount > 0)
        {
            lifeCount--;
            for (int i = lifeCount; i < dead.Length; i++)
            {
                dead[i].SetActive(false);
            }
            if (lifeCount == 0)
            {
                losePanel.SetActive(true);
            }
        }
        else
        {
            lifeCount = 0;
        }
    }

    public void StartTimer()
    {
        timeRemaining = timerMax;
        isTimerOn = true;
        //bubbleMaker.makeBubbles = true;
    }
}
