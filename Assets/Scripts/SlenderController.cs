using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlenderController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip jumpscareAudioClip;

    [SerializeField] private Animator _animatorJumpscareImage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Jumpscare();
        }
    }

    void Jumpscare()
    {
        _animatorJumpscareImage.SetTrigger("jumpscare");
        _audioSource.PlayOneShot(jumpscareAudioClip);

    }

}
