using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : IPersistentSingleton<UIManager>
{
    // Start is called before the first frame update

    public static UIManager instance;
  
    [SerializeField] private int keyAmount = 0;

    public bool mapIsOpen = false;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private AudioClip pickUpKeyAudioClip;
    [SerializeField] private Animator _animatorTextFeedbackMap;


    //public CameraFirstPerson cameraFirstPerson;

    public Animator animatorTextFeedbackMap { get => _animatorTextFeedbackMap; set => _animatorTextFeedbackMap = value; }
    public int KeyAmount { get => keyAmount; set => keyAmount = value; }

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    { 
        if (SceneManager.GetActiveScene().name.Equals("MazeScene")){
            FeedbackGetKey();
        }
    }

    public void SetActiveTextInteract(bool state)
    {
        CanvasManager.instance.textInteract.gameObject.SetActive(state);
    }
    public void SetActiveTextCollectible(bool state)
    {
        CanvasManager.instance.textCollect.gameObject.SetActive(state);
    }

    public void ShowMap()
    {
        mapIsOpen = true;
        CanvasManager.instance.mapImage.gameObject.SetActive(true);
        audioSource.clip = paperSound;
        audioSource.Play();
        PlayerController.instance.CanMove = false;
        CameraFirstPerson.instance.CanMove = false;
    }

    public void CloseMap()
    {
        CanvasManager.instance.mapImage.gameObject.SetActive(false);
        mapIsOpen = false;
        PlayerController.instance.CanMove = true;
        CameraFirstPerson.instance.CanMove = true;

    }

    public void FeedbackMap()
    {
        CanvasManager.instance.textSeeTheMap.gameObject.SetActive(true);
        CanvasManager.instance.animatorFeedBackTookMap.SetTrigger("Feedback");
    }

    public void ShowFeedbacksKey()
    {
        CanvasManager.instance.textAmountGetKey.gameObject.SetActive(true);
        CanvasManager.instance.textAmountKey.gameObject.SetActive(true);
        CanvasManager.instance.keyIcon.gameObject.SetActive(true);
    }

    public void FeedbackGetKey()
    {
        CanvasManager.instance.textAmountGetKey.text = keyAmount.ToString();
    }

    public void PlaySoundPickUpKey()
    {
        audioSource.PlayOneShot(pickUpKeyAudioClip);

    }

 

   public IEnumerator EnableBloodScreen()
    {
        CanvasManager.instance.blooScreenImage.gameObject.SetActive(true);
        CanvasManager.instance.animatorBlood.SetInteger("transition", 1);
        yield return new WaitForSeconds(3f);
        CanvasManager.instance.blooScreenImage.gameObject.SetActive(false);
        CanvasManager.instance.animatorBlood.SetInteger("transition", 0);
    }


    public void Menu()
    {
        CanvasManager.instance.closeGame.gameObject.SetActive(true);
    }
   
    public void CloseGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

}
