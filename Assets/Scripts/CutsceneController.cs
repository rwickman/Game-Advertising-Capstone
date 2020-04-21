using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// This class controls when the cutscenes at the end of the levels should be played.
/// </summary>
public class CutsceneController : MonoBehaviour
{

    bool played = false;
    PlayableDirector timeline;
    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCutscene()
    {
        if (!isPlaying() && !played)
        {
            timeline.Play();
            played = true;
        }
    }

    public bool isPlaying()
    {
        if (timeline.state == PlayState.Playing)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
