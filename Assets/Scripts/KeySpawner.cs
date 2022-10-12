using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{

    [SerializeField] private Transform[] vetKeySpawns;
    [SerializeField] GameObject key;

    private void Awake()
    {
        int positionVet = Random.Range(0, vetKeySpawns.Length);
        Instantiate(key, vetKeySpawns[positionVet]);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
