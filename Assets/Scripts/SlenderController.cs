using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;



public class SlenderController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip jumpscareAudioClip;

    [SerializeField] private Animator _animatorJumpscareImage;

    [SerializeField] private bool jumpscareAlready;

    [SerializeField] private bool seePlayer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animatorEnemy;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float range;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float playerMaxSpeed;

    [SerializeField] private bool restartCoroutine;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float viewAngle;
    [SerializeField] private float viewRadius;

    [SerializeField] private bool rotationOnEnemy;

    [SerializeField] private float raySize;

    [SerializeField] private bool punchedPlayer;

    [SerializeField] private float playerDistance;

    [SerializeField] private float slenderSpeedFollowPlayer;
    [SerializeField] private float slenderAccelerationFollowPlayer;

    private void Awake()
    {
        jumpscareAlready = false;
        punchedPlayer = false;
        rotationOnEnemy = false;
        seePlayer = false;
        restartCoroutine = true;
    }

    void Start()
    {
        playerMaxSpeed = 3;
        animatorEnemy = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        playerTransform = FindObjectOfType<PlayerController>().transform;

        _animatorJumpscareImage = CanvasManager.instance.jumpscareImage.GetComponent<Animator>();
        _audioSource = CanvasManager.instance.jumpscareImage.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        playerDistance = Vector3.Distance(playerTransform.position, transform.position);

        StartCoroutine(EnemyFOV());
        if (restartCoroutine && !seePlayer)
        {
            agent.speed = 4f;
            agent.acceleration = 7.5f;
            restartCoroutine = false;
            StartCoroutine(EnemyPatrol());
        }

        if (PlayerController.instance.Life <= 0 && !jumpscareAlready)
        {
            jumpscareAlready = true;
            StartCoroutine(Jumpscare());
        }

    }

    private void FixedUpdate()
    {
        if (rotationOnEnemy)
        {
            agent.speed = slenderSpeedFollowPlayer;
            agent.acceleration = slenderAccelerationFollowPlayer;
            Quaternion newRotation = Quaternion.LookRotation(agent.velocity.normalized, transform.up);
            Vector3 angles = newRotation.eulerAngles;
            angles.x = 0; angles.z = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(angles), Time.deltaTime * rotationSpeed);
            agent.velocity = animatorEnemy.deltaPosition / Time.deltaTime;
        }

    }
    IEnumerator Jumpscare()
    {
        _animatorJumpscareImage.SetTrigger("jumpscare");
        _audioSource.PlayOneShot(jumpscareAudioClip);
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        CanvasManager.instance.wastedText.gameObject.SetActive(true);
        CameraFirstPerson.instance.CanMove = false;
        PlayerController.instance.CanMove = false;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        CameraFirstPerson.instance.CanMove = true;
        PlayerController.instance.CanMove = true;
        SceneManager.LoadScene("MazeScene");
    }


    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    IEnumerator EnemyFOV()
    {
        Vector3 playerTarget = (playerTransform.transform.position - transform.position).normalized;
        RaycastHit hit;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, playerTransform.transform.position);
            if (distanceToTarget <= viewRadius)
            {

                if (Physics.Raycast(transform.position, playerTarget, out hit, raySize))
                {
                    if (hit.collider.tag.Equals("Player"))
                    {
                        seePlayer = true;
                        Debug.DrawRay(transform.position, hit.point, Color.red);
                        agent.SetDestination(playerTransform.transform.position);
                        rotationOnEnemy = true;
                        Debug.Log("SEGUINDO O PLAYER");
                        if (playerDistance <= 2f && !punchedPlayer)
                        {
                            StartCoroutine(PunchTime());
                        }
                        yield return new WaitForSeconds(4f);
                        Debug.Log("DEU 4 SEGUNDOS");
                        bool verifyFov = PlayerRayTrigger();
                        Debug.Log("Valor verifyFov: " + verifyFov);
                        if (verifyFov)
                        {
                            StartCoroutine(EnemyFOV());
                        }
                        else
                        {
                            rotationOnEnemy = false;
                            seePlayer = false;
                            Debug.Log("É FALSO ESSA MERDA");
                        }
                    }
                }
            }
        }
    }

    IEnumerator PunchTime()
    {
        punchedPlayer = true;
        animatorEnemy.SetTrigger("Punch");
        yield return new WaitForSeconds(2f);
        punchedPlayer = false;

    }

    public bool PlayerRayTrigger()
    {
        Vector3 playerTarget = (playerTransform.transform.position - transform.position).normalized;
        RaycastHit hit;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, playerTransform.transform.position);
            if (distanceToTarget <= viewRadius)
            {

                if (Physics.Raycast(transform.position, playerTarget, out hit, raySize))
                {
                    if (hit.collider.tag.Equals("Player"))
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;

            }
        }
        else
        {
            return false;

        }
    }
    IEnumerator EnemyPatrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centerPoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.red, 1.0f); //so you can see with gizmos
                yield return new WaitForSeconds(4f);
                agent.SetDestination(point);

            }
        }
        restartCoroutine = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            //textLose.gameObject.SetActive(true);
            Debug.Log("RELOU NO PLAYER");
            Time.timeScale = 0f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.maxSpeed > playerMaxSpeed)
            {
                Debug.Log("Escutou o player");

                seePlayer = true;
                agent.SetDestination(playerTransform.transform.position);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.maxSpeed > playerMaxSpeed)
            {
                Debug.Log("Escutou o player");

                seePlayer = true;
                agent.SetDestination(playerTransform.transform.position);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, playerTransform.transform.position);
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.DrawWireSphere(centerPoint.transform.position, range);

    }
}
