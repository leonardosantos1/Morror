using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static UIManager instance;
    [SerializeField] private Text textInteract;
    [SerializeField] private Text textCollectible;
    [SerializeField] private Text textFeedbackMap;
    
    [SerializeField] private Text textFeedbackGetKey;
    [SerializeField] private Text textAmoutKey;
    [SerializeField] private Image keyIcon;


    [SerializeField] private int keyAmount = 0;

    public bool mapIsOpen = false;
    [SerializeField] private Image mapImage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paperSound;
    [SerializeField] private Animator _animatorTextFeedbackMap;

    public CameraFirstPerson cameraFirstPerson;

    public Animator animatorTextFeedbackMap { get => _animatorTextFeedbackMap; set => _animatorTextFeedbackMap = value; }
    public int KeyAmount { get => keyAmount; set => keyAmount = value; }

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
        
    }

    // Update is called once per frame
    void Update()
    {
        FeedbackGetKey();
    }

    public void SetActiveTextInteract(bool state)
    {
        textInteract.gameObject.SetActive(state);
    }
    public void SetActiveTextCollectible(bool state)
    {
        textCollectible.gameObject.SetActive(state);
    }

    public void ShowMap()
    {
        mapIsOpen = true;
        mapImage.gameObject.SetActive(true);
        audioSource.clip = paperSound;
        audioSource.Play();
        PlayerController.instance.CanMove = false;
        cameraFirstPerson.CanMove = false;
    }

    public void CloseMap()
    {
        mapImage.gameObject.SetActive(false);
        mapIsOpen = false;
        PlayerController.instance.CanMove = true;
        cameraFirstPerson.CanMove = true;

    }

    public void FeedbackMap()
    {
        textFeedbackMap.gameObject.SetActive(true);
        _animatorTextFeedbackMap.SetTrigger("Feedback");
    }

    public void ShowFeedbacksKey()
    {
        textFeedbackGetKey.gameObject.SetActive(true);
        textAmoutKey.gameObject.SetActive(true);
        keyIcon.gameObject.SetActive(true);

    }

    public void FeedbackGetKey()
    {
        textFeedbackGetKey.text = keyAmount.ToString();
    }
}
