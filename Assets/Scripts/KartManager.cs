using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartManager : MonoBehaviour
{

    public GameObject VRKart;

    public GameObject MobileKart;
    bool isVR;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleIsVR()
    {
        isVR = !isVR;
    }
}
