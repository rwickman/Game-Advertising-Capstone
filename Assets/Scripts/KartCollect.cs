using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartCollect : MonoBehaviour
{
    public int score;

    private int coinPoints = 5;
    private int bottleCapPoints = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            score += coinPoints;
            Destroy(other.gameObject);
        }
        if (other.tag == "Cap")
        {
            score += bottleCapPoints;
            Destroy(other.gameObject);
        }
    }
}
