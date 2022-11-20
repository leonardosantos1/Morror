using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class LightDoorController : MonoBehaviour
{
    [SerializeField] private GameObject fadeImage;
    [SerializeField] private Animator animatorFade;

    private bool fadeAvaible = false;
    private bool isInFade = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage = FindObjectOfType<CanvasManager>().GetComponent<Canvas>().transform.GetChild(1).GetComponentInChildren<Image>().gameObject;
        animatorFade = FindObjectOfType<CanvasManager>().GetComponent<Canvas>().transform.GetChild(1).GetComponentInChildren<Image>().GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        if (fadeAvaible && !isInFade)
        {
            isInFade = true;
            fadeAvaible = false;
            fadeImage.SetActive(true);
            PlayerController.instance.CanMove = false;
            animatorFade.SetTrigger("FadeIn");
            yield return new WaitForSeconds(3f);
            fadeImage.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                fadeAvaible = true;
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                fadeAvaible = false;
                isInFade = false;
                break;
        }
    }




}
