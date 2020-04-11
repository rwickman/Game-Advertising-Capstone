using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeShip : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyShip;
    public GameObject explosion;

    public GameObject friendLaser;
    
    private bool activatedExplosion;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activatedExplosion)
        {
            activatedExplosion = true;
            StartCoroutine("Explode");
        }
    }

    IEnumerator Explode()
    {
        Destroy(enemyShip);
        explosion.SetActive(true);
        yield return new WaitForSeconds(1f);
        Destroy(friendLaser);
        yield return new WaitForSeconds(10f);
        Destroy(explosion);
    }

}
