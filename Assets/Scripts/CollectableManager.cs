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
    public int coinsPerLap = 25;
    public int capsPerLap= 5;

    // Enforce that collectables cannot be placed too close to the finish line
    public float minDistanceFromFinishLine = 20;
    public bool useGeneric;

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
        print("PLACING COLLECTABLE");
        Shuffle(tracks);
       
        for (int i = 0; i < Mathf.Min(capsPerLap, tracks.Count); i++)
        {
            if (useGeneric)
            {
                collectables.Add(
                    Instantiate(drThundeCapPrefab,
                        tracks[i].GetComponent<Renderer>().bounds.center + Vector3.up * 0.5f,
                        coinPrefab.transform.rotation,
                        collectParent.transform));
            }
            else
            {
                collectables.Add(
                    Instantiate(cokeCapPrefab,
                        tracks[i].GetComponent<Renderer>().bounds.center + Vector3.up * 0.5f,
                        coinPrefab.transform.rotation,
                        collectParent.transform));
            }
        }

        // Place coins up until coinsPerLap or all the track space has been used up
        for (int i = capsPerLap; i < Mathf.Min(capsPerLap + coinsPerLap, tracks.Count); i++)
        {
            collectables.Add(
                Instantiate(coinPrefab, 
                    tracks[i].GetComponent<Renderer>().bounds.center + Vector3.up * 0.5f,
                    coinPrefab.transform.rotation,
                    collectParent.transform));
        }
    }

    public void RemoveCollectables()
    {
        print("REMOVING COLLECTABLE");
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
