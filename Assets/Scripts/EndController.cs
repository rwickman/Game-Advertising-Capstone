using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.Track;

public class EndController : MonoBehaviour
{

    public TrackManager trackManager;
    public Transform finishLine;
    public GameObject startPortal;

    public Transform endFinishLinePos;

    [HideInInspector] public Racer playerRacer;

    private bool setupEnd;
    
    // Update is called once per frame
    void Update()
    {
        if (!setupEnd &&
            trackManager.hitFirstCheckpoint
            && playerRacer.GetCurrentLap() == trackManager.raceLapTotal)
        {
            // This needs to be delayed until maybe after a checkpoint is reached.
            // It will cause the player to teleport before wanting to
            finishLine.position = endFinishLinePos.position;
            startPortal.SetActive(true);
            setupEnd = true;   
        }
    }
}
