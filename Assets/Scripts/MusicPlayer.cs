using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private bool IsMuted;
    private AudioSource MusicOne;
    private AudioSource MusicTwo;
    // Start is called before the first frame update
    void Awake()
    {
        MusicOne = transform.GetChild(0).GetComponent<AudioSource>();
        MusicTwo = transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Call this to toggle music between playing and muted
    /// </summary>
    public void ToggleMusic()
    {
        IsMuted = !IsMuted;
        MusicOne.mute = IsMuted;
        MusicTwo.mute = IsMuted;
    }

    /// <summary>
    /// Feed in an audio clip to change the currently playing song. If have time will fade music out / in
    /// </summary>
    /// <param name="newclip"></param>
    public void ChangeMusic(AudioClip newclip)
    {
        if (MusicOne.isPlaying)
        {
            MusicTwo.clip = newclip;
            MusicTwo.Play();
            MusicOne.Stop();
        }
        else
        {
            MusicOne.clip = newclip;
            MusicOne.Play();
            MusicTwo.Stop();
        }
    }
}
