using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private bool isOpen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetDoorActivity(bool status)
    {
        if (status.Equals(true))
        {
            _animator.SetTrigger("Close");
            isOpen = !status;
        }else if (status.Equals(false))
        {
            _animator.SetTrigger("Open");
            isOpen = !status;

        }
    }
}
