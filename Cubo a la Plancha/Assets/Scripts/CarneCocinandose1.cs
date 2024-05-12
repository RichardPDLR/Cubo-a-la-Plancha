using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CarneCocinandose1 : MonoBehaviour
{    
    public Material materialAlContacto;
    private Material materialOriginal;
    private Renderer rend;
    public bool cocinado = false;
    public float tiempoDeCoccion = 3f; // Tiempo en segundos para la cocción
    public AudioSource sonidoCocinando; // Referencia al componente AudioSource    
    public AudioSource CarneCocinada; // Referencia al componente AudioSource
    public bool sonidoReproducido = false; // Variable para controlar la reproducción del sonido
    public GameObject EfectoDeHumo;

    void Start()
    {
        rend = GetComponent<Renderer>();
        materialOriginal = rend.material;        
    }    

    public void CambiarMaterial()
    {       
        /// Si la carne no ha sido cocinada todavía
        if (!cocinado)
        {
            /// Programar la restauración del material original después del tiempo de cocción
            Invoke("RestaurarMaterial", tiempoDeCoccion);
            
            // Cambiar el material solo después de que haya pasado el tiempo de cocción
            Invoke("CambiarMaterialInternamente", tiempoDeCoccion);

            // Iniciar la reproducción del sonido
            EfectoDeHumo.SetActive(true);
            sonidoCocinando.Play();
            sonidoReproducido = true;
        }
    }    

    public void RestaurarMaterial()
    {
        rend.material = materialOriginal;        
        cocinado = false; // Reiniciar el estado de cocción        

        // Detener la reproducción del sonido        
        sonidoCocinando.Stop();

        if(sonidoReproducido)
        {
            EfectoDeHumo.SetActive(false);
            CarneCocinada.Play();            
            sonidoReproducido = false;
        }
    }

    private void CambiarMaterialInternamente()
    {
        // Cambiar el material solo si aún no ha sido cocinado
        if (!cocinado)
        {
            rend.material = materialAlContacto;
            cocinado = true;            
        }
    }    
}
