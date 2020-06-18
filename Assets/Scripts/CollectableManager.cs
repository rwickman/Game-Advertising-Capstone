﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   The CollectableManager compenent spawns collectable GameObjects every lap.
/// </summary>
public class CollectableManager : MonoBehaviour
{

    public GameObject coinPrefab;

    public GameObject cokeCapPrefab;
    public GameObject drThundeCapPrefab;

    // This has all the individual tracks as children
    public Transform trackParent;

    public Transform finishLine;
    public int coinClusterPerLap = 25;
    public int capClustersPerLap = 5;

    // Enforce that collectables cannot be placed too close to the finish line
    public float minDistanceFromFinishLine = 20;
    public bool useGeneric;
    public int clusterSize = 5;
    public float clusterOffset = 1f;
    public float curveOffset = 2.5f;
    public LayerMask coinLayer;

    private List<Transform> tracks;
    private GameObject collectParent;
    private List<GameObject> collectables;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            useGeneric = gameManager.GetComponent<AdManager>().GetUseGeneric();
        }
        
        collectables = new List<GameObject>();
        if (finishLine == null)
        {
            finishLine = GameObject.Find("StartFinishLine").transform;
        }

        if (trackParent == null)
        {
            trackParent = GameObject.Find("ModularTrack").transform;

        }
        // Get all tracks
        tracks = new List<Transform>();
        GetAllTracks(trackParent);
        FilterTracks();

        collectParent = new GameObject("Collectables");
        // Set to collectable layer
        collectParent.layer = 10;
        PlaceCollectables();
    }

    public void PlaceCollectables()
    {
        Shuffle(tracks);

        Vector3 capPos;
        for (int i = 0; i < Mathf.Min(capClustersPerLap, tracks.Count); i++)
        {
            if (tracks[i].name.Contains("Curve"))
            {
                capPos = tracks[i].position + Vector3.up * 2f;
            }
            else
            {
                capPos = tracks[i].GetComponent<Renderer>().bounds.center + Vector3.up * 2f;
            }

            if (tracks[i].rotation.eulerAngles.y == 0 || Mathf.Abs(tracks[i].rotation.eulerAngles.y) == 180)
            {
                for (int j = 0; j < clusterSize; j++)
                {
                    if (useGeneric)
                    {
                        Quaternion rot;
                        if (tracks[i].GetComponent<TrackDirection>().flipped)
                        {
                            rot = Quaternion.Euler(
                                tracks[i].rotation.eulerAngles.x,
                                tracks[i].rotation.eulerAngles.y + 180f,
                                tracks[i].rotation.eulerAngles.z);
                        }
                        else
                        {
                            rot = Quaternion.Euler(tracks[i].rotation.eulerAngles);
                        }
                        collectables.Add(
                            Instantiate(drThundeCapPrefab,
                                capPos,
                                rot,
                                collectParent.transform));
                    }
                    else
                    {

                        Quaternion rot;
                        if (tracks[i].GetComponent<TrackDirection>().flipped)
                        {
                            rot = Quaternion.Euler(
                                tracks[i].rotation.eulerAngles.x,
                                tracks[i].rotation.eulerAngles.y + 180f,
                                tracks[i].rotation.eulerAngles.z);
                        }
                        else
                        {
                            rot = Quaternion.Euler(tracks[i].rotation.eulerAngles);
                        }

                        collectables.Add(
                            Instantiate(cokeCapPrefab,
                                capPos,
                                rot,
                                collectParent.transform));
                    }
                    capPos.z += clusterOffset;
                }
            }
            else
            {
                for (int j = 0; j < clusterSize; j++)
                {
                    if (useGeneric)
                    {
                        Quaternion rot;
                        if (tracks[i].GetComponent<TrackDirection>().flipped)
                        {
                            rot = Quaternion.Euler(
                                tracks[i].rotation.eulerAngles.x,
                                tracks[i].rotation.eulerAngles.y + 180f,
                                tracks[i].rotation.eulerAngles.z);
                        }
                        else
                        {
                            rot = Quaternion.Euler(tracks[i].rotation.eulerAngles);
                        }
                        collectables.Add(
                            Instantiate(drThundeCapPrefab,
                                capPos,
                                rot,
                                collectParent.transform));
                    }
                    else
                    {
                        Quaternion rot;
                        if (tracks[i].GetComponent<TrackDirection>().flipped)
                        {
                            rot = Quaternion.Euler(
                                tracks[i].rotation.eulerAngles.x,
                                tracks[i].rotation.eulerAngles.y + 180f,
                                tracks[i].rotation.eulerAngles.z);
                        }
                        else
                        {
                            rot = Quaternion.Euler(tracks[i].rotation.eulerAngles);
                        }
                        collectables.Add(
                            Instantiate(cokeCapPrefab,
                                capPos,
                                rot,
                                collectParent.transform));
                    }
                    capPos.x += clusterOffset;
                }
            }

        }

        Vector3 coinPos;
        // Place coins up until coinClusterPerLap or all the track space has been used up
        for (int i = capClustersPerLap; i < Mathf.Min(capClustersPerLap + coinClusterPerLap, tracks.Count); i++)
        {
            if (tracks[i].name.Contains("Curve"))
            {
                coinPos = tracks[i].position + Vector3.up * 2f;
            }
            else 
            {
                coinPos = tracks[i].GetComponent<Renderer>().bounds.center + Vector3.up * 2f;
            }

            Quaternion rot = Quaternion.Euler(
                coinPrefab.transform.rotation.eulerAngles.x,
                tracks[i].rotation.eulerAngles.y + 90f,
                coinPrefab.transform.rotation.eulerAngles.z);

            if (tracks[i].rotation.eulerAngles.y == 0 || Mathf.Abs(tracks[i].rotation.eulerAngles.y) == 180)
            {
                for (int j = 0; j < clusterSize; j++)
                {
                    collectables.Add(
                        Instantiate(coinPrefab,
                            coinPos,
                            rot,
                            collectParent.transform));
                    coinPos.z += clusterOffset;
                }

            }
            else
            {
                for (int j = 0; j < clusterSize; j++)
                {
                    collectables.Add(
                        Instantiate(coinPrefab,
                            coinPos,
                            rot,
                            collectParent.transform));
                    coinPos.x += clusterOffset;
                }
            }
        }
        AdjustCollectables();
    }


    public void RemoveCollectables()
    {
        foreach (GameObject collectable in collectables)
        {
            if (collectable != null)
            {
                Destroy(collectable);    
            }
        }
        collectables.Clear();
    }

    private void GetAllTracks(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.tag == "Track" || child.name.Contains("Track"))
            {
                tracks.Add(child);
            }
            else if (child.childCount > 0)
            {
                GetAllTracks(child);
            }
        }
    }

    private void FilterTracks()
    {
        List<Transform> tempTracks = new List<Transform>();
        for (int i = 0; i < tracks.Count; i++)
        {
            if (Vector3.Distance(finishLine.position, tracks[i].position) > minDistanceFromFinishLine)
            {
                tempTracks.Add(tracks[i]);
            }
        }
        tracks = tempTracks;

    }

    private void AdjustCollectables()
    {
        StartCoroutine("Adjust");
    }

    IEnumerator Adjust()
    {
        yield return new WaitForSeconds(0.1f);
        RaycastHit hit;
        for (int i = 0; i < collectables.Count; i++)
        {
            if (Physics.Raycast(collectables[i].transform.position, -Vector3.up, out hit, coinLayer))
            {
                print(hit.transform.name);
                collectables[i].transform.position = new Vector3(collectables[i].transform.position.x, hit.point.y + 1, collectables[i].transform.position.z);
            }
            collectables[i].GetComponent<CoinFloat>().enabled = true;
        }
    }

    // Fisher-Yates shuffle
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
