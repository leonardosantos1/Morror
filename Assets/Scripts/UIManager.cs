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

    public bool mapIsOpen = false;
    [SerializeField] private Image mapImage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paperSound;

    public CameraFirstPerson cameraFirstPerson;


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

}
