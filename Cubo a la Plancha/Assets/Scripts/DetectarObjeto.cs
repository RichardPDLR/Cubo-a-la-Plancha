using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DetectarObjeto : MonoBehaviour
{
    public MusicManager musicManager;
    public Transform sarten;
    public LayerMask capaSarten;
    public GameObject GanarMenu;
    public AudioClip Silbato;
    private AudioSource audioSource;       
    private bool hasPlayedOnce = false;

    // public Vector3 boxSize = new Vector3(1, 0.1f, 1); // Ajusta este tamaño según tus necesidades
    // public Vector3 boxCenterOffset = new Vector3(0, -0.5f, 0); // Ajusta la posición del centro de la caja

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró en contacto tiene un componente CarneCocinandose1
        CarneCocinandose1 colorChanger = other.GetComponent<CarneCocinandose1>();

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

                // Si el ángulo es menor que un cierto umbral, cambiar el material de la cara del cubo
                if (angulo < 60.0f)
                {
                    
                    colorChanger.CambiarMaterial();
                    Debug.Log("El punto de contacto está cerca de la sartén. Cambiando material." + other.gameObject.name);



                    // Codigo que Carga la Escena "Juego Terminado" Cuando todos los lados del Cubo cambien de material
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

                    // Verifica si todos los lados tienen el material deseado 
                    bool todosTienenMaterialDeseado = true;                   
                    foreach (GameObject lado in TodosLosLados)
                    {
                        Renderer renderer = lado.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            if (renderer.sharedMaterial != colorChanger.materialAlContacto)
                            {
                                todosTienenMaterialDeseado = false;
                                break; // Si un lado no tiene el material deseado, sal del bucle
                            }
                        }
                    }

                    // Si todos los lados tienen el material deseado, carga la escena "Juego Terminado"
                    if (todosTienenMaterialDeseado)
                    {
                       // SceneManager.LoadScene("Juego Terminado");                        
                        Time.timeScale = 0f;                        
                        GanarMenu.SetActive(true);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }      
                }
              /*  else
                {
                    
                } */
            }
          /*  else
            {
               // Debug.Log("El objeto no está en la capa de la sartén. No se cambió el color." + other.gameObject.name);
            } */
        }
    }

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Silbato;
    }

    private void Update()
    {
        // VerificarContactoConSarten();
        PartidaTerminada();
    }

    private void PartidaTerminada()
    {
        if (Time.timeScale == 0f)
        {            
            if (!hasPlayedOnce)
            {                
                musicManager.MusicaDeFondo.Stop();
                audioSource.Play();
                hasPlayedOnce = true;
            }
        }
        else
        {            
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                hasPlayedOnce = false;
            }
        }        
    }

    // private void VerificarContactoConSarten()
    // {
    //     Vector3 centerPosition = new Vector3(sarten.position.x, sarten.position.y + boxCenterOffset.y, sarten.position.z);
    //     Collider[] hits = Physics.OverlapBox(centerPosition, boxSize / 2, sarten.rotation, capaSarten);

    //     foreach (Collider hit in hits)
    //     {
    //         CarneCocinandose1 colorChanger = hit.GetComponent<CarneCocinandose1>();
    //         if (colorChanger != null)
    //         {
    //             // Ejecutar el código que se debe hacer cuando hay contacto con la sartén
    //             Vector3 puntoDeContacto = hit.ClosestPoint(sarten.position);
    //             Vector3 direccionAbajo = -transform.forward; // Vector que apunta hacia abajo
    //             Vector3 direccionHaciaPuntoDeContacto = (puntoDeContacto - transform.position).normalized;
    //             float angulo = Vector3.Angle(direccionAbajo, direccionHaciaPuntoDeContacto);

    //             if (angulo < 60.0f)
    //             {
    //                 colorChanger.CambiarMaterial();
    //                 Debug.Log("El punto de contacto está cerca de la sartén. Cambiando material." + hit.gameObject.name);

    //                 // Aquí puedes agregar el código para verificar si todos los lados están cocinados
    //             }
    //         }
    //     }
    // }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red; // Color del gizmo para el boxSize

    //     // Dibujar el boxSize y boxCenterOffset juntos
    //     Vector3 centerPosition = new Vector3(sarten.position.x, sarten.position.y + boxCenterOffset.y, sarten.position.z);
    //     Gizmos.DrawWireCube(centerPosition, boxSize); // Dibuja el boxSize centrado en la posición calculada

    //     // Dibujar líneas desde los bordes del boxSize hasta el boxCenterOffset
    //     Vector3 halfSize = boxSize * 0.5f; // Calcula la mitad del tamaño del boxSize
    //     Vector3 minPoint = centerPosition - halfSize; // Calcula el punto mínimo del boxSize
    //     Vector3 maxPoint = centerPosition + halfSize; // Calcula el punto máximo del boxSize

    //     Gizmos.color = Color.blue; // Cambia el color para el boxCenterOffset
    //     Gizmos.DrawLine(new Vector3(minPoint.x, sarten.position.y, minPoint.z), new Vector3(centerPosition.x, sarten.position.y, centerPosition.z)); // Dibuja una línea desde el borde mínimo del boxSize hasta el boxCenterOffset en Y
    //     Gizmos.DrawLine(new Vector3(maxPoint.x, sarten.position.y, maxPoint.z), new Vector3(centerPosition.x, sarten.position.y, centerPosition.z)); // Dibuja una línea desde el borde máximo del boxSize hasta el boxCenterOffset en Y
    // }
}
