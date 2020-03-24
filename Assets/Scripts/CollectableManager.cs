using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{

    public GameObject coinPrefab;

    public GameObject cokeBottlePrefab;
    public GameObject drThunderBottlePrefab;

    // This has all the individual tracks as children
    public Transform trackParent;

    public int coinsPerLevel = 25;
    public int bottlesPerLevel;

    List<Transform> tracks;
    GameObject collectParent;
    // Start is called before the first frame update
    void Start()
    {
        // Get all tracks
        tracks = new List<Transform>();
        foreach (Transform child in trackParent)
        {
            if (child.tag == "Track")
            {
                tracks.Add(child);
            }
        }
        collectParent = new GameObject("Collectables");
        PlaceCoins();
    }

    private void PlaceCoins()
    {
        GameObject coinParent = new GameObject();
        Shuffle(tracks);
        for (int i = 0; i < Mathf.Min(coinsPerLevel, tracks.Count); i++)
        {
            Instantiate(coinPrefab, tracks[i].position + Vector3.up, coinPrefab.transform.rotation, collectParent.transform);
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
