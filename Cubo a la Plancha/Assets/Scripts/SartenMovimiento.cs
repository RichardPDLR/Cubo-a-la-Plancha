using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SartenMovimiento : MonoBehaviour
{
    // Variable para controlar la sensibilidad del movimiento de la sartén, se puede modificar en Unity
    public float sensitivity = 2.0f;

    // Límites de rotación para los ejes Z y X, se puede modificar en Unity
    public float minRotationZ = -10.997f;
    public float maxRotationZ = 10.997f;
    public float minRotationX = -14.194f;
    public float maxRotationX = 14.194f;
    
    private Vector3 currentRotation; // Rotación actual de la sartén
    
    private Vector3 originalPosition; // Posición original de la sartén    
    private bool isLifting = false; // Indica si la sartén está realizando un movimiento brusco hacia arriba

    public float panForce = 2f; // Fuerza hacia arriba que se aplicará a la carne
    
    public Rigidbody meatRigidbody; // Referencias a los Rigidbody de la carne

    

    // Inicialización
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Guarda la rotación inicial de la sartén
        currentRotation = transform.localEulerAngles;

        // Guarda la posición original de la sartén
        originalPosition = transform.localPosition;
        
        if (meatRigidbody == null)        
             meatRigidbody = GameObject.FindGameObjectWithTag("Carne").GetComponent<Rigidbody>();
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1f)
        {
            // Obtiene el movimiento del mouse en los ejes horizontal (izquierda-derecha) y vertical (arriba-abajo)
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            float rotationZ = mouseX * sensitivity; // Calcula la rotación en el eje Z basada en el movimiento horizontal del mouse
            
            float rotationX = -mouseY * sensitivity; // Calcula la rotación en el eje X basada en el movimiento vertical del mouse
            
            currentRotation += new Vector3(rotationX, 0f, rotationZ); // Actualiza la rotación actual de la sartén

            // Limita la rotación en los ejes Z y X dentro de los rangos especificados
            currentRotation.x = Mathf.Clamp(currentRotation.x, minRotationX, maxRotationX);
            currentRotation.z = Mathf.Clamp(currentRotation.z, minRotationZ, maxRotationZ);
            
            transform.localEulerAngles = currentRotation; // Aplica la rotación a la sartén

            // Maneja el movimiento brusco hacia arriba al hacer clic izquierdo del mouse
            if (Input.GetMouseButtonDown(0) && !isLifting)
            {
                isLifting = true;            
                StartCoroutine(LiftUp()); // Llama a la función para iniciar el movimiento brusco hacia arriba
            }
        }        
    }    

    // Método para iniciar el movimiento brusco hacia arriba de forma suave
    IEnumerator LiftUp()
    {
        // Calcula la nueva posición de la sartén hacia arriba
        Vector3 targetPosition = originalPosition + Vector3.up * 0.3f;

        // Interpola suavemente la posición actual hacia la nueva posición durante un período de tiempo
        float duration = 0.1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }        
        
        // Aplica una fuerza hacia arriba a la carne
        if (meatRigidbody != null)
        {
            meatRigidbody.AddForce(Vector3.up * panForce, ForceMode.Impulse);
        }        

        transform.localPosition = targetPosition; // Establece la posición final
        
        yield return new WaitForSeconds(0.2f); // Espera un breve período de tiempo antes de restablecer la posición original
        
        ResetPosition(); // Llama a la función para restablecer la posición original        
    }

    // Método para restablecer la posición original de la sartén
    void ResetPosition()
    {        
        isLifting = false;        
        transform.localPosition = originalPosition;
    }
}
