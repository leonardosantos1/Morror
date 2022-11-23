using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : IPersistentSingleton<CanvasManager>
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

    public Image jumpscareImage;

    public Animator animatorBlood;
    public Image blooScreenImage;

    public Text wastedText;
    public Button restartGame;
    public Button closeGame;

    public Image menuImage;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
