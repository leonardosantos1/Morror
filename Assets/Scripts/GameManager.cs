using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPGTALK;

public class GameManager : MonoBehaviour
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

    // Start is called before the first frame update
    [SerializeField] private RPGTalk rpgTalk;
    public int numberDialogue = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance);
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        rpgTalk = FindObjectOfType<RPGTalk>();

        Invoke("FirstDialogueText", 4.5f);

    }

    // Update is called once per frame
    void Update()
    {
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
        RPGTalk.instance.NewTalk("1", "2");
    }

     void DialogueEnableWhiteDoor()
    {
        _audioSource.PlayOneShot(heavyBreathingAudioClip);
        RPGTalk.instance.NewTalk("3", "4");
    }

    public void DialogueNeedReadLetter()
    {
        RPGTalk.instance.NewTalk("7", "7");
    }

    public void DialogueNeedToFindKey()
    {
        RPGTalk.instance.NewTalk("10", "10");
        _audioSource.PlayOneShot(doorLockedAudioClip);
    }

    public void SetNewComputerScreen()
    {
        for (int i = 0; i < computerScreens.Length; i++)
        {
            computerScreens[i].material = newMaterialScreen;
     
        }
    }

    public void SetAudioClipComputer()
    {
        for(int i = 0; i < computerAudioSources.Length; i++)
        {
            computerAudioSources[i].clip = tvNoSinalAudioClip;
            computerAudioSources[i].Play();
        }
    }

}
