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

    [SerializeField] private Animator _animator;
    [SerializeField] private bool isOpen;

    public CameraFirstPerson cameraFirstPerson;


    //metodo para mostrar a carta quando for interagida
    public void ShowLetter()
    {
        Cursor.lockState = CursorLockMode.None;// desbloqueia o cursor do mouse para que o usuario consiga fechar a carta depois

        //verifica qual carta é, se seria a da primeira fase ou da segunda
        if (obj.name.Equals("Letter"))
        {
            CanvasManager.instance.letterImage.gameObject.SetActive(true);

        }
        else{
            GameManager.instance.haveReadedLetter = true;
            Debug.Log(GameManager.instance.haveReadedLetter);
            CanvasManager.instance.letterScene2.gameObject.SetActive(true);
        }

        audioSource.clip = paperSound;
        audioSource.Play();
        PlayerController.instance.CanMove = false;
        CameraFirstPerson.instance.CanMove = false;

    }
    //metodo para fechar a carta quando for apertado o botao de close

    public void CloseLetter()
    {

        Cursor.lockState = CursorLockMode.Locked;

        if (obj.name.Equals("Letter"))
        {
            CanvasManager.instance.letterImage.gameObject.SetActive(false);

        }
        else
        {
            CanvasManager.instance.letterScene2.gameObject.SetActive(false);
        }
        //isInLetter = false;
        PlayerController.instance.CanMove = true;
        CameraFirstPerson.instance.CanMove = true;

    }


    //metodo para setar qual será a ação da porta, se ela vai fechar ou abrir
    public void SetDoorActivity()
    {
        if (isOpen.Equals(true))
        {
            _animator.SetTrigger("Close");
            isOpen = !isOpen;
        }
        else if (isOpen.Equals(false))
        {
            _animator.SetTrigger("Open");
            isOpen = !isOpen;

        }
    }

}
