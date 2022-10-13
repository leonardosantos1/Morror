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
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            Invoke("FirstDialogueText", 5f);
        }
      
       
    }

    // Update is called once per frame
    void Update()
    {
        if (numberDialogue == 1)
        {
            numberDialogue++;
            Invoke("DialogueEnableWhiteDoor", 4.5f);
        }
        if (SceneManager.GetActiveScene().name.Equals("MazeScene"))
        {

        }
    }
    void FirstDialogueText()
    {
        rpgTalk.NewTalk("1", "2");
    }

     void DialogueEnableWhiteDoor()
    {
        rpgTalk.NewTalk("3", "4");
    }

    public void DialogueNeedReadLetter()
    {
        rpgTalk.NewTalk("7", "7");
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
