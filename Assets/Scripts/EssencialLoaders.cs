using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssencialLoaders : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject UIManagerGameObject;
    public GameObject PlayerGameObject;
    public GameObject CanvasManagerGameObject;
    public GameObject GameManagerGameObject;
    public GameObject CameraGameObject;

    private void Awake()
    {
        if (UIManager.instance == null)
        {
            UIManager.instance = Instantiate(UIManagerGameObject).GetComponent<UIManager>();
        }

        if (PlayerController.instance == null)
        {
            PlayerController.instance = Instantiate(PlayerGameObject).GetComponent<PlayerController>();

        }

        if (CanvasManager.instance == null)
        {
            CanvasManager.instance = Instantiate(CanvasManagerGameObject).GetComponent<CanvasManager>();

        }

        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(GameManagerGameObject).GetComponent<GameManager>();

        }

        if (CameraFirstPerson.instance == null)
        {
            CameraFirstPerson.instance = Instantiate(CameraGameObject).GetComponent<CameraFirstPerson>();

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
