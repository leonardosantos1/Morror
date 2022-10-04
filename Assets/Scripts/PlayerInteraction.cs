using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    public float rayDistance = 2f;
    public InteractiveObject interactiveObj;
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
                    }
                } 
            }
            else
            {
                UIManager.instance.SetActiveTextInteract(false);
            }
        }
        else
        {
            UIManager.instance.SetActiveTextInteract(false);
        }
    }



}
