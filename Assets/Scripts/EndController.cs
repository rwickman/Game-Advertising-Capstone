using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.Track;

/// <summary>
/// This class controls the actions that should be taken at the end of the game.
/// Handles creating the portal at level 2
/// Handles swapping the advertisement on the billboard.
/// </summary>
public class EndController : MonoBehaviour
{

    public TrackManager trackManager;
    public Transform finishLine;
    public GameObject startPortal;
    public GameObject billboardContents;
    public Material cokeMat;
    public Material drThunderMat;
    public CollectableManager collectManager;

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
            if (finishLine != null)
            {
                finishLine.position = endFinishLinePos.position;
            }
            if (startPortal != null)
            {
                startPortal.SetActive(true);
            }
            
            if (collectManager.useGeneric)
            {
                billboardContents.GetComponent<MeshRenderer>().material = drThunderMat;
            }
            else
            {
                billboardContents.GetComponent<MeshRenderer>().material = cokeMat;
            }
            
            setupEnd = true;
        }
    }
}
