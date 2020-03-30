using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int numCoinClusterSize = 5;
    public float coinOffset = 1f;

    private List<Transform> tracks;
    private GameObject collectParent;
    private List<GameObject> collectables;
    
    // Start is called before the first frame update
    void Start()
    {
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
        PlaceCollectables();
    }



    public void PlaceCollectables()
    {
        Shuffle(tracks);

        Vector3 capPos;
        for (int i = 0; i < Mathf.Min(capClustersPerLap, tracks.Count); i++)
        {
            capPos = tracks[i].GetComponent<Renderer>().bounds.center + Vector3.up * 0.5f;
            if (tracks[i].name.Contains("Curve"))
            {
                for (int j = 0; j < numCoinClusterSize; j++)
                {
                    if (useGeneric)
                    {
                        collectables.Add(
                            Instantiate(drThundeCapPrefab,
                                capPos,
                                coinPrefab.transform.rotation,
                                collectParent.transform));
                    }
                    else
                    {
                        collectables.Add(
                            Instantiate(cokeCapPrefab,
                                capPos,
                                coinPrefab.transform.rotation,
                                collectParent.transform));
                    }
                    if (tracks[i].eulerAngles.y == 180 || tracks[i].eulerAngles.y == 0)
                    {
                        capPos.x += coinOffset;
                        capPos.z -= coinOffset;
                    }
                    else
                    {
                        capPos.x += coinOffset;
                        capPos.z += coinOffset;
                    }
                    
                }
            }
            else if (tracks[i].rotation.eulerAngles.y == 0 || Mathf.Abs(tracks[i].rotation.eulerAngles.y) == 180)
            {
                for (int j = 0; j < numCoinClusterSize; j++)
                {
                    if (useGeneric)
                    {
                        collectables.Add(
                            Instantiate(drThundeCapPrefab,
                                capPos,
                                coinPrefab.transform.rotation,
                                collectParent.transform));
                    }
                    else
                    {
                        collectables.Add(
                            Instantiate(cokeCapPrefab,
                                capPos,
                                coinPrefab.transform.rotation,
                                collectParent.transform));
                    }
                    capPos.z += coinOffset;
                }
            }
            else
            {
                for (int j = 0; j < numCoinClusterSize; j++)
                {
                    if (useGeneric)
                    {
                        collectables.Add(
                            Instantiate(drThundeCapPrefab,
                                capPos,
                                coinPrefab.transform.rotation,
                                collectParent.transform));
                    }
                    else
                    {
                        collectables.Add(
                            Instantiate(cokeCapPrefab,
                                capPos,
                                coinPrefab.transform.rotation,
                                collectParent.transform));
                    }
                    capPos.x += coinOffset;
                }
            }          

        }

        Vector3 coinPos;
        // Place coins up until coinClusterPerLap or all the track space has been used up
        for (int i = capClustersPerLap; i < Mathf.Min(capClustersPerLap + coinClusterPerLap, tracks.Count); i++)
        {
            coinPos = tracks[i].GetComponent<Renderer>().bounds.center + Vector3.up * 0.5f;
            if (tracks[i].name.Contains("Curve"))
            {
                for (int j = 0; j < numCoinClusterSize; j++)
                {
                    collectables.Add(
                        Instantiate(coinPrefab,
                            coinPos,
                            coinPrefab.transform.rotation,
                            collectParent.transform));
                    if (tracks[i].eulerAngles.y == 180 || tracks[i].eulerAngles.y == 0)
                    {
                        coinPos.x += coinOffset;
                        coinPos.z -= coinOffset;
                    }
                    else
                    {
                        coinPos.x += coinOffset;
                        coinPos.z += coinOffset;
                    }
                }
            }
            else if (tracks[i].rotation.eulerAngles.y == 0 || Mathf.Abs(tracks[i].rotation.eulerAngles.y) == 180)
            {
                for (int j = 0; j < numCoinClusterSize; j++)
                {
                    collectables.Add(
                        Instantiate(coinPrefab,
                            coinPos,
                            coinPrefab.transform.rotation,
                            collectParent.transform));
                    coinPos.z += coinOffset;
                }
                    
            }
            else
            {
                for (int j = 0; j < numCoinClusterSize; j++)
                {
                    collectables.Add(
                        Instantiate(coinPrefab,
                            coinPos,
                            coinPrefab.transform.rotation,
                            collectParent.transform));
                    coinPos.x += coinOffset;
                }
            }
        }
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
            if (child.tag == "Track")
            {
                tracks.Add(child);
            }
            else if(child.childCount > 0)
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
