using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{

    [SerializeField] private Transform[] vetKeySpawns;
    [SerializeField] GameObject key;

    void Start()
    {
        int positionVet = Random.Range(0, vetKeySpawns.Length);
        Instantiate(key, vetKeySpawns[positionVet]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
