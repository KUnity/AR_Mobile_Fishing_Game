using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip btnClick;
    public AudioClip bgm;
    public AudioClip rodThrowing;
    public AudioClip fishCatch;

    AudioSource audioSource;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void ThrowRod(){
        audioSource.clip = rodThrowing;
        audioSource.Play();
    }

    public void CatchFish(){
        audioSource.clip = fishCatch;
        audioSource.Play();
    }

    public void ClickBtn(){
        audioSource.clip = btnClick;
        audioSource.Play();
    }
}
