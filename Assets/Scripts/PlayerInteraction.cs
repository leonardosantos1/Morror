using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    public float rayDistance = 2f;
    public InteractiveObject interactiveObj;
    public CollectibleObject collectibleObj;

    private bool tookMap = false;
   // [SerializeField] private Camera camera; 
    private string obj;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       Interaction();

      if(tookMap && Input.GetKeyDown(KeyCode.Tab) && !UIManager.instance.mapIsOpen)
        {
            UIManager.instance.ShowMap();

        }else if (tookMap && Input.GetKeyDown(KeyCode.Tab) && UIManager.instance.mapIsOpen)
        {
            UIManager.instance.CloseMap();
        }
    }

    public void Interaction()
    {
        RaycastHit hit;
        Vector3 rayOrigin = CameraFirstPerson.instance.cameraMain.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        if(Physics.Raycast(rayOrigin, CameraFirstPerson.instance.cameraMain.transform.forward, out hit, rayDistance))
        {
            obj = hit.collider.GetComponent<Collider>().tag;

            if (obj.Equals("Interactive Object"))
            {      
                UIManager.instance.SetActiveTextInteract(true);
               
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //hit.collider.GetComponent<InteractiveObject>();
                    interactiveObj = hit.collider.GetComponent<InteractiveObject>();
                    if (interactiveObj.obj.name.Equals("Letter") || interactiveObj.obj.name.Equals("Letter2"))
                    {
                        interactiveObj.ShowLetter();

                    }else if (interactiveObj.obj.name.Equals("FinalDoor")) { 
                        if (UIManager.instance.KeyAmount == 2)
                        {
                            interactiveObj.SetDoorActivity();
                        }
                        else
                        {
                            GameManager.instance.DialogueNeedToFindKey();
                        }

                    }else if(interactiveObj.obj.name.Equals("Door")){
                        if (GameManager.instance.haveReadedLetter)
                        {
                            interactiveObj.SetDoorActivity();

                        }
                        else
                        {
                            GameManager.instance.DialogueNeedReadLetter();
                        }
                    }
                   
                }
                 
            }else
            {
                UIManager.instance.SetActiveTextInteract(false);
            }
            //intera��o com objetos coletaveis
            if (obj.Equals("Collectible Object"))
            {
                UIManager.instance.SetActiveTextCollectible(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //hit.collider.GetComponent<CollectibleObject>();
                    collectibleObj = hit.collider.GetComponent<CollectibleObject>();
                    if (collectibleObj.obj.name.Equals("Map"))
                    {
                        tookMap = true;
                        Destroy(collectibleObj.gameObject);
                        UIManager.instance.FeedbackMap();
                    }
                    else if (collectibleObj.obj.name.Equals("Key"))
                    {
                        UIManager.instance.KeyAmount++;
                        UIManager.instance.PlaySoundPickUpKey();
                        Destroy(collectibleObj.gameObject);

                    }
                }
            }
            else
            {
                if (SceneManager.GetActiveScene().name.Equals("MazeScene"))
                {
                    UIManager.instance.SetActiveTextCollectible(false);
                }
            }
        }
        else
        {
            UIManager.instance.SetActiveTextInteract(false);

            if (SceneManager.GetActiveScene().name.Equals("MazeScene"))
            {
                UIManager.instance.SetActiveTextCollectible(false);
            }

        }
    }



}
