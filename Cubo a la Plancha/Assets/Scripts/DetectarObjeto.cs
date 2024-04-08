using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarObjeto : MonoBehaviour
{
    public Transform sarten;
    public LayerMask capaSarten;
        

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

                // Si el ángulo es menor que un cierto umbral, cambiar el color de la cara del cubo
                if (angulo < 60.0f)
                {
                    
                    colorChanger.CambiarColor();
                    Debug.Log("El punto de contacto está cerca de la sartén. Cambiando color." + other.gameObject.name);
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
