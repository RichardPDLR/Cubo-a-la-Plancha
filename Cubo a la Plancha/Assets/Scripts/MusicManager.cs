using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource MusicaDeFondo;
    public AudioSource MusicaDeGanar;

    void Start()
    {        
        MusicaDeFondo.Play();
    }

    internal void MusicaGanar()
    {
        MusicaDeGanar.Play();
    }
}
