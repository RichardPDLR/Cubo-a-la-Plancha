using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarneCocinandose2 : MonoBehaviour
{
    public Color colorAlContacto2 = Color.red; // Color al contacto con otro objeto
    private Color colorOriginal2; // Color original del objeto
    private Renderer rend2; // Referencia al componente Renderer
    private bool enContacto2 = false; // Estado de contacto
    private bool cambioColorActivado2 = false; // Estado de cambio de color activado
    public float tiempoAntesDeCambio2 = 1.0f; // Tiempo antes de cambiar de color

    void Start()
    {
        // Obtener el componente Renderer del objeto
        rend2 = GetComponent<Renderer>();

        // Guardar el color original del objeto
        colorOriginal2 = rend2.material.color;
    }

    void Update()
    {
        // Verificar si el objeto está en contacto y el tiempo de espera ha pasado
        if (enContacto2 && tiempoAntesDeCambio2 > 0)
        {
            tiempoAntesDeCambio2 -= Time.deltaTime;
        }
        else if (enContacto2 && !cambioColorActivado2)
        {
            // Cambiar el color del objeto al color de contacto
            rend2.material.color = colorAlContacto2;
            cambioColorActivado2 = true;
        }
    }

    // Método que se ejecuta cuando otro objeto entra en contacto con el objeto actual
    void OnTriggerEnter(Collider other2)
    {
        // Verificar si el objeto que entró en contacto tiene un BoxCollider
        if (other2.GetComponent<BoxCollider>() != null)
        {
            // Activar el estado de contacto
            enContacto2 = true;
        }        
    }

    // Método que se ejecuta cuando otro objeto deja de estar en contacto con el objeto actual
    void OnTriggerExit(Collider other2)
    {
        // Verificar si el objeto que dejó de estar en contacto tiene un BoxCollider
        if (other2.GetComponent<BoxCollider>() != null)
        {
            // Desactivar el estado de contacto
            enContacto2 = false;

            // Reiniciar el tiempo antes del cambio
            tiempoAntesDeCambio2 = 1.0f;
        }
    }
}
