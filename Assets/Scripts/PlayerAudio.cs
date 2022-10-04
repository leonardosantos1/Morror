using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipSteps;
    [SerializeField] private AudioClip audioClipStepsTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySteps()
    {
        audioSource.clip = audioClipSteps;
        audioSource.Play();
    }
    public void PlayStepsTwo()
    {
        audioSource.clip = audioClipStepsTwo;
        audioSource.Play();
    }
}
