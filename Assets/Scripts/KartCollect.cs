using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
    }
}
