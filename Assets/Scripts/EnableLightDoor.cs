using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGTALK;

public class EnableLightDoor : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject doorMonster1;
    [SerializeField] private GameObject doorMonster2;

    private Monster1 monster1;
    private Monster2 monster2;

    [SerializeField] private RPGTalk rpgTalk;

    private string avaibleDoor = "empty";  

    void Start()
    {
        monster1 = FindObjectOfType<Monster1>();
        monster2 = FindObjectOfType<Monster2>();
    }

    // Update is called once per frame
    void Update()
    {
     
        if(avaibleDoor.Equals("empty"))
        {
            if (monster1.AlreadyHappenedFirstJumpScare)
            {
                avaibleDoor = "monster1";
            }
            if (monster2.AlreadyHapennedJumpScare)
            {
                avaibleDoor = "monster2";
            }  
        }

        if( monster1.AlreadyHappenedFirstJumpScare && monster2.AlreadyHapennedJumpScare)
        {
            if (avaibleDoor.Equals("monster1"))
            {
                doorMonster1.SetActive(true);
            }
            else if (avaibleDoor.Equals("monster2"))
            {
                doorMonster2.SetActive(true);
            }

            if (GameManager.instance.numberDialogue == 0)
            {
                //GameManager.instance.SetNewComputerScreen();
                //GameManager.instance.SetAudioClipComputer();
                GameManager.instance.numberDialogue++;
            }
            
        }
    }
}
