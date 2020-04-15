﻿using System.Collections;
using System.Collections.Generic;
using KartGame.Track;
using KartGame.KartSystems;
using UnityEngine;

public class KartManager : MonoBehaviour
{

    public GameObject VRKart;
    public GameObject mobileKart;
    public SceneData sceneData;

    SceneController sceneController;
    EndController endController;

    bool isVR = false;
    
    void Start()
    {
        sceneController = GetComponent<SceneController>();
        sceneController.AddLoadCallback(InitLevel);
    }


    public void InitLevel(string sceneName)
    {
        //print("InitLevel");
        GameObject trackManagerGO = GameObject.Find("TrackManager");
        GameObject displayGO = GameObject.Find("TimeDisplayCanvas");


        GameObject kartGO = null;
        Vector3 kartPos = Vector3.zero;
        if (sceneName == "Level1")
        {
            kartPos = sceneData.level1KartPos;
        }
        else if (sceneName == "Level2")
        {
            kartPos = sceneData.level2KartPos;
        }
        
        if (isVR)
        {
            kartGO = Instantiate(VRKart, kartPos, Quaternion.identity);
        }
        else
        {
            GameObject kartParent = Instantiate(mobileKart, Vector3.zero, Quaternion.identity);
            // Get the kart gameobject
            kartGO = kartParent.transform.GetChild(1).gameObject;
            kartGO.transform.position = kartPos;
        }
        
        
        // Add kart references to classes that require it
        KartMovement movement = kartGO.GetComponent<KartMovement>();
        Racer racer = kartGO.GetComponent<Racer>();
        KartRepositionTrigger kartRepoTrig = trackManagerGO.GetComponent<KartRepositionTrigger>();
        TimeDisplay display = displayGO.GetComponent<TimeDisplay>();
        AudioSource audio = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        audio.enabled = true;
        audio.Play();

        kartRepoTrig.SetMovable(movement);
        display.SetRacer(racer);
        

        display.enabled = true;
        kartRepoTrig.enabled = true;

        trackManagerGO.GetComponent<TrackManager>().StartTrackManager();
        display.StartDisplay();
        // Set the Racer for EndController
        endController = GameObject.Find("EndGameController").GetComponent<EndController>();
        endController.playerRacer = racer;
    }

 
    public void SetIsVR(bool value)
    {
        isVR = value;
    }
}
