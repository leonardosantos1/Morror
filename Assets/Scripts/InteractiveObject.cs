using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{

    public ScriptableObject obj;
    public bool isInLetter = false;
    [SerializeField] private Image letterImage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paperSound;

    public CameraFirstPerson cameraFirstPerson;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLetter()
    {
        letterImage.gameObject.SetActive(true);
        audioSource.clip = paperSound;
        audioSource.Play();
        PlayerController.instance.CanMove = false;
        cameraFirstPerson.CanMove = false;
    }

    public void CloseLetter()
    {
        letterImage.gameObject.SetActive(false);
        isInLetter = false;
        PlayerController.instance.CanMove = true;
        cameraFirstPerson.CanMove = true;
    }

}
