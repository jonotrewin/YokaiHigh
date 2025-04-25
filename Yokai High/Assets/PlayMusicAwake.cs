using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicAwake : MonoBehaviour
{
    public string MusicToPlay;
    void Start()
    {
        AudioManager.Instance.Play(MusicToPlay);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
