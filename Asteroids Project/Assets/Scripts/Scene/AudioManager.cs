using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager aM;

    [HideInInspector]
    public static AudioSource playerAS, saucerAS, asteroidAS;

    #region SFX Audioclips
    public AudioClip playerFireSound;
    public AudioClip bigSaucerFireSound;
    public AudioClip smallSaucerFireSound;
    public AudioClip destroySound;
    public AudioClip teleportSound;
    public AudioClip teleportArriveSound;
    public AudioClip gameOverSound;
    #endregion

    void Awake()
    {
        aM = this;
        playerAS = gameObject.AddComponent<AudioSource>();
        saucerAS = gameObject.AddComponent<AudioSource>();
        asteroidAS = gameObject.AddComponent<AudioSource>();
    }
}
