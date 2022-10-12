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
    [SerializeField] private Camera camera; 
    private string obj;

    void Start()
    {
        camera = Camera.main;
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
        Vector3 rayOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

        if(Physics.Raycast(rayOrigin, camera.transform.forward, out hit, rayDistance))
        {
            obj = hit.collider.GetComponent<Collider>().tag;

            if (obj.Equals("Interactive Object"))
            {      
                UIManager.instance.SetActiveTextInteract(true);
               
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<InteractiveObject>();
                    interactiveObj = hit.collider.GetComponent<InteractiveObject>();
                    if (interactiveObj.obj.name.Equals("Letter") && !interactiveObj.isInLetter)
                    {
                        interactiveObj.ShowLetter();
                        interactiveObj.isInLetter = true;

                        if (SceneManager.GetActiveScene().name.Equals("MazeScene"))
                        {
                            UIManager.instance.ShowFeedbacksKey();
                        }

                    }
                   
                } 
            }else
            {
                UIManager.instance.SetActiveTextInteract(false);
            }
            if (obj.Equals("Collectible Object"))
            {
                UIManager.instance.SetActiveTextCollectible(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<CollectibleObject>();
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
                        Destroy(collectibleObj.gameObject);

                    }
                }
            }
            else
            {
                UIManager.instance.SetActiveTextCollectible(false);

            }

        }
        else
        {
            UIManager.instance.SetActiveTextInteract(false);
            UIManager.instance.SetActiveTextCollectible(false);

        }
    }



}
