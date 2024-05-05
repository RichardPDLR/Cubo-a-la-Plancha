using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Timers;

public class DetectarObjeto : MonoBehaviour
{
    public Transform sarten;
    public LayerMask capaSarten;

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró en contacto tiene un componente CarneCocinandose1
        CarneCocinandose1 colorChanger = other.GetComponent<CarneCocinandose1>();               
        Cronometro cronometro = other.GetComponent<Cronometro>();

        if (colorChanger != null)
        {
            // Verificar si el objeto está en la capa de la sartén
            if ((capaSarten.value & (1 << other.gameObject.layer)) != 0)
            {
                // Obtener el punto de contacto más cercano entre el collider del cubo y la sartén
                Vector3 puntoDeContacto = other.ClosestPoint(sarten.position);

                // Calcular el ángulo entre la dirección hacia abajo y la dirección hacia el punto de contacto
                Vector3 direccionAbajo = -transform.forward; // Vector que apunta hacia abajo
                Vector3 direccionHaciaPuntoDeContacto = (puntoDeContacto - transform.position).normalized;
                float angulo = Vector3.Angle(direccionAbajo, direccionHaciaPuntoDeContacto);

                // Si el ángulo es menor que un cierto umbral, cambiar el color de la cara del cubo
                if (angulo < 60.0f)
                {
                    
                    colorChanger.CambiarColor();
                    Debug.Log("El punto de contacto está cerca de la sartén. Cambiando color." + other.gameObject.name);



                    // Codigo que Carga la Escena "Juego Terminado" Cuando todos los lados del Cubo cambien de color
                    // Encuentra todos los GameObjects con los tags deseados
                    GameObject[] Lados1 = GameObject.FindGameObjectsWithTag("CarneFrente");
                    GameObject[] Lados2 = GameObject.FindGameObjectsWithTag("CarneAtras");
                    GameObject[] Lados3 = GameObject.FindGameObjectsWithTag("CarneArriba");
                    GameObject[] Lados4 = GameObject.FindGameObjectsWithTag("CarneAbajo");
                    GameObject[] Lados5 = GameObject.FindGameObjectsWithTag("CarneIzquierda");
                    GameObject[] Lados6 = GameObject.FindGameObjectsWithTag("CarneDerecha");
                    
                    // Combina los arrays de lados
                    GameObject[] TodosLosLados = new GameObject[Lados1.Length + Lados2.Length + Lados3.Length + Lados4.Length + Lados5.Length + Lados6.Length];
                    Lados1.CopyTo(TodosLosLados, 0);
                    Lados2.CopyTo(TodosLosLados, Lados1.Length);
                    Lados3.CopyTo(TodosLosLados, Lados1.Length + Lados2.Length);
                    Lados4.CopyTo(TodosLosLados, Lados1.Length + Lados2.Length + Lados3.Length);
                    Lados5.CopyTo(TodosLosLados, Lados1.Length + Lados2.Length + Lados3.Length + Lados4.Length);
                    Lados6.CopyTo(TodosLosLados, Lados1.Length + Lados2.Length + Lados3.Length + Lados4.Length + Lados5.Length);

                    // Verifica si todos los lados tienen el color deseado
                    bool todosTienenColorDeseado = true;
                    foreach (GameObject lado in TodosLosLados)
                    {
                        Renderer renderer = lado.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            if (renderer.material.color != colorChanger.colorAlContacto)
                            {
                                todosTienenColorDeseado = false;
                                break; // Si un lado no tiene el color deseado, sal del bucle
                            }
                        }
                    }

                    // Si todos los lados tienen el color deseado, carga la escena "Juego Terminado"
                    if (todosTienenColorDeseado)
                    {                                               
                        Time.timeScale = 0f;
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        SceneManager.LoadScene("Juego Terminado");
                    }      
                }
               /* else
                {
                    Debug.Log("El punto de contacto no está suficientemente cerca de la sartén. No se cambió el color." + other.gameObject.name);
                } */
            }
           /* else
            {
                Debug.Log("El objeto no está en la capa de la sartén. No se cambió el color." + other.gameObject.name);
            } */
        }
    }  
}
