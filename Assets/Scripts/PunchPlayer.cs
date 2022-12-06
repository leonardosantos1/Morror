using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Image sangue;
    [SerializeField] private Animator animatorSangue;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")){
            Debug.Log("Acertou o SOCO NO PLAYer");
            PlayerController.instance.Life--;
            StartCoroutine(UIManager.instance.EnableBloodScreen());
        }
    }
}
