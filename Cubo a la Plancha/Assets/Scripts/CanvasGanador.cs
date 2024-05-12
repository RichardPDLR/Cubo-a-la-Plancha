using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGanador : MonoBehaviour
{
    MusicManager musicManager;

     private void Start()
     {
        musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            Time.timeScale = 1f;
            musicManager.MusicaDeFondo.Stop();
            musicManager.MusicaGanar();
        }
     }

    public void Reiniciar(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }    
}
