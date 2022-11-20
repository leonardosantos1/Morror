using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFirstPerson : IPersistentSingleton<CameraFirstPerson>
{
    // Start is called before the first frame update


    public static CameraFirstPerson instance;

    [SerializeField] [Range(0.01f, 6)] private float _sensibility;
    [SerializeField] private float mouseX = 0.0f, mouseY = 0.0f;

    [SerializeField] private Transform headPlayer;
    [SerializeField] private Transform player;

    public Camera cameraMain;

    private float rotationX, rotationY;

    [SerializeField] private bool canMove;

    public bool CanMove { get => canMove; set => canMove = value; }

    //private void Awake()
    //{
    //    if (instance != null && instance != this)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }

    //}


    private void Start()
    {
        cameraMain = GetComponent<Camera>();
        player = FindObjectOfType<PlayerController>().transform;
        headPlayer = player.transform.GetChild(0).GetComponentInChildren<Transform>();
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!canMove)
        {
           
        }
        else{

            mouseX = Input.GetAxisRaw("Mouse X") * _sensibility;
            mouseY = Input.GetAxisRaw("Mouse Y") * _sensibility;

            rotationX += mouseX;
            rotationY += mouseY;

            rotationY = Mathf.Clamp(rotationY, -90f, 90f);

            transform.rotation = Quaternion.Euler(-rotationY, rotationX, 0);
            player.rotation = Quaternion.Euler(0, rotationX, 0);
        }
      
    }

    private void LateUpdate()
    {

        transform.position = headPlayer.position;

    }

}
