using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public bool IsMuted;
    private AudioSource[] SfxPlayers;
    // Start is called before the first frame update
    void Awake()
    {
        SfxPlayers = transform.GetComponentsInChildren<AudioSource>();
    }

    // Update is called once per frame

    /// <summary>
    /// Feed in an audio clip to be played from SfxPlayer
    /// </summary>
    /// <param name="Sfx"></param>
    public void PlaySfx(AudioClip Sfx)
    {
        for (int i = 0; i < SfxPlayers.Length; i++)
        {
            if (!SfxPlayers[i].isPlaying)
            {
                SfxPlayers[i].clip = Sfx;
                SfxPlayers[i].Play();
                return;
            }
        }
    }

    /// <summary>
    /// toggle Sfx on and off
    /// </summary>
    public void ToggleSfx()
    {
        IsMuted = !IsMuted;
        foreach (AudioSource sfx in SfxPlayers) sfx.mute = IsMuted;
    }

}
