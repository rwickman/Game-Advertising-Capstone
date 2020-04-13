using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeShip : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyShip;
    public GameObject explosion;
    public List<GameObject> friendShip;
    
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
        for (int i = 0; i < friendShip.Count; i++)
        {
            friendShip[i].transform.GetChild(0).gameObject.SetActive(false);
            ShipMovment shipMovement = friendShip[i].GetComponent<ShipMovment>();
            if (shipMovement != null)
            {
                friendShip[i].GetComponent<ShipMovment>().enabled = true;
                friendShip[i].GetComponent<Ship>().enabled = false;
            }
            
        }

        yield return new WaitForSeconds(10f);
        Destroy(explosion);
    }

}
