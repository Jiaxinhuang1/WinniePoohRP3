using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("HoleNumber", 0));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenPanel(GameObject go)
    {
        go.transform.localScale = new Vector3(0, 1, 1);
        go.SetActive(true);
        LeanTween.scaleX(go, 1, 0.15f);
    }

    public void ClosePanel(GameObject go)
    {
        LeanTween.scaleX(go, 0, 0.15f).setOnComplete(() => go.SetActive(false));
    }

    [ContextMenu("ResetPlayerPrefs")]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
