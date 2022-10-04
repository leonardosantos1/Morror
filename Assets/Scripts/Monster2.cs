using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject monster;
    [SerializeField] private bool avaibleToJumpScare = false;
    [SerializeField] private bool alreadyHapennedJumpScare = false;
    [SerializeField] private Animator animatorMonster;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipLaughter;
    [SerializeField] private AudioClip audioClipChan;

    public bool AlreadyHapennedJumpScare { get => alreadyHapennedJumpScare; set => alreadyHapennedJumpScare = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(JumpScare());
    }
    

    IEnumerator JumpScare()
    {
        if (avaibleToJumpScare)
        {
            AlreadyHapennedJumpScare = true;
            avaibleToJumpScare = false;
            audioSource.clip = audioClipLaughter;
            monster.SetActive(true);
            audioSource.PlayOneShot(audioClipChan);
            yield return new WaitForSeconds(0.8f);
            animatorMonster.SetTrigger("Laughing");
            audioSource.Play();
            yield return new WaitForSeconds(2f);
            monster.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                if (!AlreadyHapennedJumpScare)
                {
                    avaibleToJumpScare = true;
                }
                break;
        }
    }
}
