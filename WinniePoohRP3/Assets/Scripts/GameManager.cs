using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum Difficulty { Easy, Medium, Hard }

    [Header("Difficulty Tracker")]
    public float minSpawnSpeed;
    public int difficultyLevel;
    public GameManager.Difficulty difficultyState;
    public GameObject winPanel;

    [Header("Carrots")]
    public int carrotCount;
    public GameObject[] carrots;

    //[Header("Timer")]
    //public Slider timeSlider;
    //private float timeRemaining;
    //private const float timerMax = 20f;
    //public bool isTimerOn;
    public GameObject notificationText;
    public GameObject spawner;
    private AudioSource collectSound;

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
        collectSound = gameObject.GetComponent<AudioSource>();
        int holeNum = PlayerPrefs.GetInt("HoleNumber", 0);
        PlayerPrefs.SetInt("DifficultyLevel", holeNum);
        difficultyLevel = PlayerPrefs.GetInt("DifficultyLevel", 1);
        ChangeDifficulty();
        //isTimerOn = false;
        spawner.SetActive(false);
        carrotCount = 0;
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        /* This is for timer functionality
        if (isTimerOn)
        {
            timeSlider.value = CalculateSliderValue();
            if (timeRemaining <= 0)
            {
                if (carrotCount <= 0)
                {
                    losePanel.SetActive(false);
                }
                else
                {
                    winPanel.SetActive(true);
                }
                spawner.SetActive(false);
                isTimerOn = false;
            }
            else if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
        }*/
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
                minSpawnSpeed = 1.25f;
                Debug.Log("Easy Level");
                Debug.Log("Spawn Speed: " + minSpawnSpeed);
                break;
            case Difficulty.Medium:
                minSpawnSpeed = 0.5f;
                Debug.Log("Medium Level");
                Debug.Log("Spawn Speed: " + minSpawnSpeed);
                break;
            case Difficulty.Hard:
                minSpawnSpeed = 0.25f;
                Debug.Log("Hard Level");
                Debug.Log("Spawn Speed: " + minSpawnSpeed);
                break;
        }
    }

    public void CollectCarrot()
    {
        collectSound.Play();
        carrotCount++;
        for (int i = 0; i < carrotCount; i++)
        {
            if (i < carrots.Length)
            {
                carrots[i].SetActive(true);
            }
        }
        if (carrotCount >= 10)
        {
            winPanel.SetActive(true);
            spawner.SetActive(false);
            //isTimerOn = false;
        }
    }

    /*
    public void StartTimer()
    {
        timeRemaining = timerMax;
        spawner.SetActive(true);
        isTimerOn = true;
    }*/

    IEnumerator Countdown()
    {
        TextMeshProUGUI notiText;
        notiText = notificationText.GetComponent<TextMeshProUGUI>();
        notificationText.SetActive(true);
        notiText.text = "Click to Jump";
        yield return new WaitForSeconds(1);
        notiText.text = "3";
        yield return new WaitForSeconds(1);
        notiText.text = "2";
        yield return new WaitForSeconds(1);
        notiText.text = "1";
        yield return new WaitForSeconds(1);
        notiText.text = "Go!";
        yield return new WaitForSeconds(1);
        notificationText.SetActive(false);
        spawner.SetActive(true);
        //StartTimer();
    }
}
