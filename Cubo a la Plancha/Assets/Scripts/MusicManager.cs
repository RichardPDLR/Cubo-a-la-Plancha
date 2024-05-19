using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource MusicaDeMenu;
    public AudioSource MusicaDeFondo;
    public AudioSource MusicaDeGanar;
    public AudioSource MusicaDeEsperaEmpezar;
    public AudioSource MusicaDeEsperaLoop;

    internal void MusicaMenu()
    {
        MusicaDeMenu.Play();
    }

    internal void MusicaFondo()
    {        
        MusicaDeFondo.Play();
    }

    internal void MusicaGanar()
    {
        MusicaDeGanar.Play();
    }

    internal void MusicaEsperaEmpezar()
    {
        MusicaDeEsperaEmpezar.Play();
    }

    internal void MusicaEsperaLoop()
    {
        MusicaDeEsperaLoop.Play();
    }

    internal IEnumerator PlayAudiosInSequence()
    {
        // Reproduce el primer audio
        MusicaDeGanar.Play();

        // Espera hasta que el primer audio haya terminado
        while (MusicaDeGanar.isPlaying)
        {
            yield return null; // Espera un frame
        }

        // Reproduce el segundo audio
        MusicaDeEsperaEmpezar.Play();

        while (MusicaDeEsperaEmpezar.isPlaying)
        {
            yield return null; // Espera un frame
        }

        MusicaDeEsperaLoop.Play();
    }
}
