using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestGetComponentScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel;
    public Canvas panel2;
    public Image panel3;
    public Text panel4;

    public GameObject text; 


    private void Awake()
    {

    }
    void Start()
    {
       panel2 = FindObjectOfType<CanvasManager>().GetComponent<Canvas>();
       panel3 = panel2.gameObject.transform.GetChild(2).GetComponentInChildren<Image>();
       panel4 = panel3.gameObject.transform.GetChild(0).GetComponentInChildren<Text>();
       text = panel4.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
