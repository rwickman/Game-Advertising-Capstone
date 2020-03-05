using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.Track;

public class LevelManager : MonoBehaviour
{
    public int numberOfLevels = 2;

    public int curLevel = 1;

    TrackManager trackManager;

    SceneController sceneController;

    void Start()
    {
        sceneController = GetComponent<SceneController>();
        sceneController.AddLoadCallback(SetTrackManager);
    }

    void Update()
    {
        if (curLevel != 0 && trackManager != null && trackManager.IsRaceStopped)
        {
            print("LOADING SCENE");
            trackManager = null;
            sceneController.LoadScene(GetNextLevel());
        }
    }

    void SetTrackManager(string sceneName)
    {
        if (curLevel != 0)
        {
            trackManager = GameObject.Find("TrackManager").GetComponent<TrackManager>();
        }
    }

    string GetNextLevel()
    {
        ++curLevel;
        if (curLevel > numberOfLevels)
        {
            curLevel = 1;
            return "Title";
        }
        else 
        {
            return "Level" + curLevel;
        }
    }
}
