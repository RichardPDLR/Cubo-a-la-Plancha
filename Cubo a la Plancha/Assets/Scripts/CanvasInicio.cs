using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasInicio : MonoBehaviour
{
    MusicManager musicManager;

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();

        musicManager.MusicaMenu();
    }

    public void seleccionarescena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void salir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
