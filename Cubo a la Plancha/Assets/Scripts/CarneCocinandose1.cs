using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarneCocinandose1 : MonoBehaviour
{
    public Color colorAlContacto = Color.red; // Color al contacto con otro objeto
    private Color colorOriginal; // Color original del objeto
    private Renderer rend; // Referencia al componente Renderer
    private bool enContacto = false; // Estado de contacto
    private bool cambioColorActivado = false; // Estado de cambio de color activado
    public float tiempoAntesDeCambio = 1.0f; // Tiempo antes de cambiar de color

    void Start()
    {
        // Obtener el componente Renderer del objeto
        rend = GetComponent<Renderer>();

        // Guardar el color original del objeto
        colorOriginal = rend.material.color;
    }

    void Update()
    {
        // Verificar si el objeto está en contacto y el tiempo de espera ha pasado
        if (enContacto && tiempoAntesDeCambio > 0)
        {
            tiempoAntesDeCambio -= Time.deltaTime;
        }
        else if (enContacto && !cambioColorActivado)
        {
            // Cambiar el color del objeto al color de contacto
            rend.material.color = colorAlContacto;
            cambioColorActivado = true;
        }
    }

    // Método que se ejecuta cuando otro objeto entra en contacto con el objeto actual
    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entró en contacto tiene un BoxCollider
        if (other.GetComponent<BoxCollider>() != null)
        {
            // Activar el estado de contacto
            enContacto = true;
        }
    }

    // Método que se ejecuta cuando otro objeto deja de estar en contacto con el objeto actual
    void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que dejó de estar en contacto tiene un BoxCollider
        if (other.GetComponent<BoxCollider>() != null)
        {
            // Desactivar el estado de contacto
            enContacto = false;

            // Reiniciar el tiempo antes del cambio
            tiempoAntesDeCambio = 1.0f;
        }
    }
}
