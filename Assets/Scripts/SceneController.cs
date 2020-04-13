using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public delegate void LevelLoaded(string sceneName);

    LevelLoaded load;

    private static SceneController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        load = null;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print("LOADED");
        // Check if you loaded a playable level
        if (scene.name.Contains("Level"))
        {
            load(scene.name);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);                
    }

    public void AddLoadCallback(LevelLoaded callback)
    {
        load += callback;
    }
}
