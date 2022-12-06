using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class EpilepsiaScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoInitialMenu()
    {
        SceneManager.LoadScene("InitialScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
