using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KartCollect : MonoBehaviour
{
    public int score;
   
    private int coinPoints = 5;
    private int bottleCapPoints = 10;

    private TextMeshProUGUI scoreText;
    private bool scoreUpdated;
    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (scoreUpdated)
        {
            scoreText.text = "Score: " + score;
            scoreUpdated = false;
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            score += coinPoints;
            StartCoroutine(CollectAndDestory(other.gameObject));
            scoreUpdated = true;
        }
        if (other.tag == "Cap")
        {
            score += bottleCapPoints;
            StartCoroutine(CollectAndDestory(other.gameObject));
            scoreUpdated = true;
        }
    }

    IEnumerator CollectAndDestory(GameObject collectable)
    {
        print("Playing coin sound!");
        collectable.GetComponent<MeshRenderer>().enabled = false;
        collectable.GetComponent<CapsuleCollider>().enabled = false;
        AudioSource audio = collectable.GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(collectable);
    }
}
