using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.Track;

public class EndController : MonoBehaviour
{

    public TrackManager trackManager;
    public Transform finishLine;
    public GameObject startPortal;
    public GameObject billboardContents;
    public Material billboardMat;

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
            finishLine.position = endFinishLinePos.position;
            startPortal.SetActive(true);
            billboardContents.GetComponent<MeshRenderer>().material = billboardMat;
            setupEnd = true;
        }
    }
}
