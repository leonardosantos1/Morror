using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject target;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        transform.LookAt(targetPosition);
    }
}
