using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static CanvasManager instance;

    public Text textInteract;
    public Text textCollect;
    public Text textSeeTheMap;
    public Text textAmountKey;
    public Text textAmountGetKey;

    public Image letterImage;
    public Image letterScene2;
    public Image mapImage;
    public Image keyIcon;

    public Animator animatorFeedBackTookMap;

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
}
