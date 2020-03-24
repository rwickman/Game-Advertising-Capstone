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

    public int coinsPerLevel = 25;
    public int capsPerLevel= 5;

    List<Transform> tracks;
    GameObject collectParent;

    public bool useGeneric;
    // Start is called before the first frame update
    void Start()
    {
        if (trackParent == null)
        {
            trackParent = GameObject.Find("ModularTrack").transform;

        }
        // Get all tracks
        tracks = new List<Transform>();
        GetAllTracks(trackParent);

        collectParent = new GameObject("Collectables");
        PlaceCoins();
    }

    private void PlaceCoins()
    {
        Shuffle(tracks);
       
        for (int i = 0; i < Mathf.Min(capsPerLevel, tracks.Count); i++)
        {
            if (useGeneric)
            {
                Instantiate(drThundeCapPrefab, tracks[i].position + Vector3.up, coinPrefab.transform.rotation, collectParent.transform);
            }
            else
            {
                Instantiate(cokeCapPrefab, tracks[i].position + Vector3.up, coinPrefab.transform.rotation, collectParent.transform);
            }
            
        }

        // Place coins up until coinsPerLevel or all the track space has been used up
        for (int i = capsPerLevel; i < Mathf.Min(capsPerLevel + coinsPerLevel, tracks.Count); i++)
        {
            Instantiate(coinPrefab, tracks[i].position + Vector3.up, coinPrefab.transform.rotation, collectParent.transform);
        }
    }


    private void GetAllTracks(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.tag == "Track")
            {
                tracks.Add(child);
            }
            else
            {
                GetAllTracks(child);
            }
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
