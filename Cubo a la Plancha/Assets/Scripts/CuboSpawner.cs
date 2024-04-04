using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboSpawner : MonoBehaviour
{
     public GameObject cubePrefab; // Prefab del cubo
    public Transform spawnPoint; // Punto de spawn para el cubo
    private Rigidbody meatRigidbody; // Rigidbody del cubo

    // Método que se llama cuando se activa el GameObject
    void Start()
    {
        SpawnCube(); // Llama al método para spawnear el cubo al inicio del juego
    }

    // Método para spawnear un nuevo cubo
    public void SpawnCube()
    {
        GameObject newCube = Instantiate(cubePrefab, transform.position, Quaternion.identity); // Spawnear el nuevo cubo
        meatRigidbody = newCube.GetComponent<Rigidbody>(); // Obtener el Rigidbody del cubo

        // Asignar el Rigidbody del nuevo cubo a la sartén
        SartenMovimiento sartenMovimiento = FindObjectOfType<SartenMovimiento>(); // Encontrar el script de la sartén en la escena
        if (sartenMovimiento != null)
        {
            sartenMovimiento.meatRigidbody = meatRigidbody; // Asignar el Rigidbody del cubo a la sartén
        }
    }
}
