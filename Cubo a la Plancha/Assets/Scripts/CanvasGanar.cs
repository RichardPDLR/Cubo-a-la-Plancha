using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGanar : MonoBehaviour
{
    public void Continuar(string nombre)
    {
        SceneManager.LoadScene(nombre);
        
    }
}
