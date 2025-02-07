#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Runtime.CompilerServices;
using System.Linq;

public class Buttons : MonoBehaviour
{
    public static Buttons buttonsInstance;
    public GameObject comSoonPannel;
    public AudioSource sfxManager;
    public AudioClip errorClip;
    public AudioClip SelectClip;
    public AudioClip winClip;
    public AudioClip levelClip;
    public AudioClip tochClip;

    private string buttonName;
    private void Awake()
    {
        if (buttonsInstance != null && buttonsInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            buttonsInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void PannelOpenCloss(GameObject pannelObj)
    {
        if (pannelObj.activeSelf == true)
        {
            pannelObj.SetActive(false);
            sfxManager.clip = SelectClip;
            sfxManager.Play();
        }
        else
        {
            pannelObj.SetActive(true);
            sfxManager.clip = SelectClip;
            sfxManager.Play();
        }
    }
    public  void Settings()
    {
        SceneManager.LoadScene($"Settings");
        sfxManager.clip = SelectClip;
        sfxManager.Play();
    }
    public void Levels()
    {
        SceneManager.LoadScene($"Levels");
        sfxManager.clip = SelectClip;
        sfxManager.Play();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene($"Lobby");
        sfxManager.clip = SelectClip;
        sfxManager.Play();
    }
    public void LevelSelect(Button levelBt)
    {
        bool SceneExected = false;
            buttonName = levelBt.name;
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.path.Contains(buttonName))
                {
                    SceneExected = true;
                }
            }
        if (SceneExected)
        {
            SceneManager.LoadScene($"{buttonName}");
            sfxManager.clip = levelClip;
            sfxManager.Play();
        }
        else
        {
            comSoonPannel.SetActive(true);
            sfxManager.clip = errorClip;
            sfxManager.Play();
        }
    }
    public void NextLevel()
    {
       
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            sfxManager.clip = SelectClip;
            sfxManager.Play();
        }
        else
        {
            comSoonPannel.SetActive(true);
            sfxManager.clip = errorClip;
            sfxManager.Play();
        }
    }
    public void RePlay()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        sfxManager.clip = SelectClip;
        sfxManager.Play();
    }
    public void Back()
    {
        SceneManager.LoadScene($"Lobby");
        sfxManager.clip = SelectClip;
        sfxManager.Play();
    }
    public void About()
    {
        SceneManager.LoadScene($"About");
        sfxManager.clip = SelectClip;
        sfxManager.Play();
    }
    public void Quit()
    {
        Application.Quit();
        sfxManager.clip = SelectClip;
        sfxManager.Play();
    }
}
#endif