using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CarneCocinandose1 : MonoBehaviour
{
    public Color colorAlContacto = Color.red;
    private Color colorOriginal;
    public Renderer rend;
    public bool cocinado = false;
    public float tiempoDeCoccion = 3f; // Tiempo en segundos para la cocción
    public AudioSource sonidoCocinando; // Referencia al componente AudioSource
    public AudioSource CarneCocinada; // Referencia al componente AudioSource
    private bool sonidoReproducido = false; // Variable para controlar la reproducción del sonido

    void Start()
    {
        rend = GetComponent<Renderer>();
        colorOriginal = rend.material.color;        
    }    

    public void CambiarColor()
    {       
        /// Si la carne no ha sido cocinada todavía
        if (!cocinado)
        {
            /// Programar la restauración del color original después del tiempo de cocción
            Invoke("RestaurarColor", tiempoDeCoccion);
            
            // Cambiar el color solo después de que haya pasado el tiempo de cocción
            Invoke("CambiarColorInternamente", tiempoDeCoccion);

            // Iniciar la reproducción del sonido
            sonidoCocinando.Play();
            sonidoReproducido = true;
        }
    }    

    public void RestaurarColor()
    {
        rend.material.color = colorOriginal;        
        cocinado = false; // Reiniciar el estado de cocción

        // Detener la reproducción del sonido
        sonidoCocinando.Stop();

        if(sonidoReproducido)
        {
            CarneCocinada.Play();
            sonidoReproducido = false;
        }
    }

    private void CambiarColorInternamente()
    {
        // Cambiar el color solo si aún no ha sido cocinado
        if (!cocinado)
        {
            rend.material.color = colorAlContacto;
            cocinado = true;  
        }
    }    
}
