using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiniciarCubo : MonoBehaviour
{
    private CuboSpawner cubeSpawner; // Referencia al script CuboSpawner

    void Start()
    {
        // Busca el script CuboSpawner en la escena
        cubeSpawner = FindObjectOfType<CuboSpawner>();

        if (cubeSpawner == null)
        {
            Debug.LogError("CubeSpawner script not found in the scene!");
        }
    }

    // Método que se llama cuando el cubo colisiona con otro collider
    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión ocurrió con un objeto que no sea la sartén
        if (collision.gameObject.CompareTag("Entorno") == true && collision.gameObject.CompareTag("Sarten") == false)
        {
            // Destruye este cubo
            Destroy(gameObject);
            // Spawnear otro cubo diferente
            cubeSpawner.SpawnCube();
        }
    }
}
