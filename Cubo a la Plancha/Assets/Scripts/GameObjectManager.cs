using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public GameObject Sarten;
    public GameObject CarneSpawn;
    public GameObject Animacion;

    // Update is called once per frame
    void Update()
    {
        if (!Animacion.activeSelf && Sarten.activeSelf == false)
        {
            // Activa Sarten y CarneSpawn
            Sarten.SetActive(true);
            CarneSpawn.SetActive(true);            
        }
    }
}
