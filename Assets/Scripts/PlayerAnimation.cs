using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator _animator;
    
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.IsWalk)
           
        {
            _animator.SetInteger("transition", 1);
        }
        else
        {
            _animator.SetInteger("transition", 0);
        }
    }
}
