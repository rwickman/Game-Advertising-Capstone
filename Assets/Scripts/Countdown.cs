using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    float currentTime;
    [SerializeField] float startingTime;

    [SerializeField] TextMeshProUGUI countdownText;

    [SerializeField] GameObject gameManager;

    bool canCount = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        currentTime = startingTime;
    }

    private void Update()
    {
        if (canCount)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 0)
            {
                currentTime = 0;
                SceneController sn = gameManager.GetComponent<SceneController>();
                sn.LoadScene("Level1");
            }

            if (currentTime <= 5.5)
            {
                countdownText.color = Color.red;
            }
        }
    }

    public void CountBtn()
    {
        canCount = true;
    }
}
