using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Runtime.CompilerServices;

public class Buttons : MonoBehaviour
{

    public GameObject comSoonPannel;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
           
        }
        else
        {
            pannelObj.SetActive(true);
        }
    }
    public  void Settings()
    {
        SceneManager.LoadScene($"Settings");

    }
    public void Levels()
    {
        SceneManager.LoadScene($"Levels");

    }
    public void MainMenu()
    {
        SceneManager.LoadScene($"Lobby");

    }
    public void LevelSelect(Button levelBt)
    {
        bool SceneExected = false;
            string buttonName = levelBt.name;
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

        }
        else
        {
            comSoonPannel.SetActive(true);
        }
    }
    public void Back()
    {
        SceneManager.LoadScene($"Lobby");

    }
    public void About()
    {
        SceneManager.LoadScene($"About");

    }
    public void Quit()
    {

    }
}
