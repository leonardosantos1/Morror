using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : MonoBehaviour
{
    [Header("First Jump Scare Required Components")]
    [SerializeField] private GameObject monster;
    [SerializeField] private GameObject light;
    [SerializeField] private Animator animatorMonster;
    [SerializeField] private bool avaibleFirstJumpScare = false;
    [SerializeField] private bool alreadyHappenedFirstJumpScare = false;
    [SerializeField] private AudioClip audioClipChan;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClipMonsterRunBack;

    public bool AlreadyHappenedFirstJumpScare { get => alreadyHappenedFirstJumpScare; set => alreadyHappenedFirstJumpScare = value; }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FirstJumpScare());
    }

    IEnumerator FirstJumpScare()
    {
        if (avaibleFirstJumpScare)
        {
            AlreadyHappenedFirstJumpScare = true;
            avaibleFirstJumpScare = false;
            audioSource.clip = audioClipMonsterRunBack;
            light.SetActive(true);
            monster.SetActive(true);
            audioSource.PlayOneShot(audioClipChan);
            yield return new WaitForSeconds(1.3f);
            audioSource.Play();
            yield return new WaitForSeconds(2f);
            animatorMonster.SetTrigger("RunBack");
            yield return new WaitForSeconds(2f);
            monster.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.tag)
        {
            case "Player":
                if (!AlreadyHappenedFirstJumpScare)
                {
                    avaibleFirstJumpScare = true;
                }
                break;
        }
    }

}
