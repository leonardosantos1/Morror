using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFirstPerson : IPersistentSingleton<CameraFirstPerson>
{
    // Start is called before the first frame update


    public static CameraFirstPerson instance;

    [SerializeField] [Range(0.01f, 6)] private float _sensibility;// range da sensibilidade do mouse
    [SerializeField] private float mouseX, mouseY;

    [SerializeField] private Transform headPlayer;
    [SerializeField] private Transform player;

    public Camera cameraMain;

    private float rotationX, rotationY;

    [SerializeField] private bool canMove;

    public bool CanMove { get => canMove; set => canMove = value; }
    private void Start()
    {
        cameraMain = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;// trava o cursor
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

            mouseX = Input.GetAxisRaw("Mouse X") * _sensibility;// pega o input do eixo X  do mouse
            mouseY = Input.GetAxisRaw("Mouse Y") * _sensibility;// pega o input do eixo Y  do mouse

            rotationX += mouseX;
            rotationY += mouseY;

            rotationY = Mathf.Clamp(rotationY, -90, 90);//Limita grau no eixo y para que o usuario nao consiga dar uma 360 com o mouse durante o jogo

            transform.rotation = Quaternion.Euler(-rotationY, rotationX, 0);// rotação da camera de acordo com o movimento do mouse
            player.rotation = Quaternion.Euler(0, rotationX, 0);// rotação do player de acordo com o movimento do mouse
        }
      
    }

    private void LateUpdate()
    {
        transform.position = headPlayer.position;//position na cabeça do player
    }

}
