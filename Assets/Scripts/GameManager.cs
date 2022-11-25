using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPGTALK;

public class GameManager : IPersistentSingleton<GameManager>
{

    public static GameManager instance;
    [SerializeField] private Renderer[] computerScreens;
    [SerializeField] private AudioSource[] computerAudioSources;
    [SerializeField] private AudioClip tvNoSinalAudioClip;
    [SerializeField] private Material newMaterialScreen;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip heavyBreathingAudioClip;
    [SerializeField] private AudioClip doorLockedAudioClip;

    private bool loadDoneMazeScene = false;

    public bool haveReadedLetter = false;

    // Start is called before the first frame update
    [SerializeField] private RPGTalk rpgTalk;
    public int numberDialogue = 0;

    void Start()
    {
        haveReadedLetter = false;
        rpgTalk = FindObjectOfType<RPGTalk>();
        Invoke("FirstDialogueText", 4f);

    }

    // Update is called once per frame
    void Update()
    {
        //seta a logica do menu de pause durante o jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CanvasManager.instance.menuImage.gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                AudioListener.pause = false;
                CanvasManager.instance.menuImage.gameObject.SetActive(false);
                PlayerController.instance.CanMove = true;
                CameraFirstPerson.instance.CanMove = true;
            }
            else
            {

                Time.timeScale = 0f;
                AudioListener.pause = true;
                PlayerController.instance.CanMove = false;
                CameraFirstPerson.instance.CanMove = false;
                CanvasManager.instance.menuImage.gameObject.SetActive(true);
            }
        }

        if (numberDialogue == 1)
        {
            numberDialogue++;
            Invoke("DialogueEnableWhiteDoor", 4.5f);
        }
        if (SceneManager.GetActiveScene().name.Equals("MazeScene") && !loadDoneMazeScene)
        {
            loadDoneMazeScene = true;
            gameObject.GetComponent<EnableLightDoor>().enabled = false;
            rpgTalk = FindObjectOfType<RPGTalk>();

        }
    }
    void FirstDialogueText()
    {
        rpgTalk.NewTalk("1", "2");
    }

     void DialogueEnableWhiteDoor()
    {
        _audioSource.PlayOneShot(heavyBreathingAudioClip);
        rpgTalk.NewTalk("3", "4");
    }

    public void DialogueNeedReadLetter()
    {
        rpgTalk.NewTalk("7", "7");
    }

    public void DialogueNeedToFindKey()
    {
        rpgTalk.NewTalk("10", "10");
        _audioSource.PlayOneShot(doorLockedAudioClip);
    }

    //public void SetNewComputerScreen()
    //{
    //    for (int i = 0; i < computerScreens.Length; i++)
    //    {
    //        computerScreens[i].material = newMaterialScreen;
     
    //    }
    //}

    //public void SetAudioClipComputer()
    //{
    //    for(int i = 0; i < computerAudioSources.Length; i++)
    //    {
    //        computerAudioSources[i].clip = tvNoSinalAudioClip;
    //        computerAudioSources[i].Play();
    //    }
    //}

}
