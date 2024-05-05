using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasInicio : MonoBehaviour
{
    public void seleccionarescena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
