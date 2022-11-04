using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed;

    private bool loadDoneMazeScene = false;

    Vector3 velocity;
    Vector3 direction = Vector3.zero;
    Vector3 currentVelocity;

    public float acceleration = 50;
    public float maxSpeed = 10;

    [SerializeField] private bool canMove;

    [SerializeField] private bool isWalk = false;

    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public bool IsWalk { get => isWalk; set => isWalk = value; }
    public bool CanMove { get => canMove; set => canMove = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(instance != this)
        {
            Destroy(instance);
            DontDestroyOnLoad(gameObject);
        }
        canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("MazeScene") && !loadDoneMazeScene)
        {
            loadDoneMazeScene = true;
            RespawnPlayerMazeScene();

        }
    }
    private void FixedUpdate()
    {
        OnMove();
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
    }

    public void OnMove()
    {
        //_rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * _speed;

        //float moveH = Input.GetAxis("Horizontal");
        //float moveV = Input.GetAxis("Vertical");

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        direction = (transform.forward * verticalInput + transform.right * horizontalInput);
        // _rb.velocity = (direction * _speed);
        //direction = new Vector3(horizontalInput, 0, verticalInput).normalized;


        velocity = Vector3.SmoothDamp(velocity, direction * maxSpeed, ref currentVelocity, maxSpeed / acceleration);

        if (canMove)
        {
            _rb.velocity = velocity;

        }
        else
        {
           _rb.velocity = Vector3.zero;
        }

        //transform.position += velocity * Time.deltaTime;


    }

    void RespawnPlayerMazeScene()
    {
        canMove = true;
        transform.position = new Vector3(-20.7560005f, 1.40100002f, -40f);
    }


}
