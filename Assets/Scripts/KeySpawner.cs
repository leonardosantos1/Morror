using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{

    [SerializeField] private Transform[] vetKeySpawns;
    [SerializeField] GameObject key;

    private void Awake()
    {

        int positionVet1 = Random.Range(0, vetKeySpawns.Length);
        int positionVet2 = Random.Range(0, vetKeySpawns.Length);
        Instantiate(key, vetKeySpawns[positionVet1]);
        while (positionVet2 == positionVet1)
        {
            positionVet2 = Random.Range(0, vetKeySpawns.Length);
            if (positionVet2 != positionVet1) break;

        }
        Instantiate(key, vetKeySpawns[positionVet2]);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
