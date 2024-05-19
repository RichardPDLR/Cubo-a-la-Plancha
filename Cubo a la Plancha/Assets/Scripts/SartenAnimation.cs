using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SartenAnimation : MonoBehaviour
{
    public AnimationClip IntroduccionSarten; // Arrastra tu Animation Clip aquí en el Inspector
    private Animator animator;
    public AudioSource sartenColocada;
    private AnimatorOverrideController animatorOverrideController;

    MusicManager musicManager;

    void Start()
    {        
        // Obtener la referencia al componente Animator
        animator = GetComponent<Animator>();
        musicManager = FindObjectOfType<MusicManager>();

        musicManager.MusicaDeMenu.Stop();     

        if (animator != null && IntroduccionSarten != null)
        {
            // Crear un AnimatorOverrideController basado en el Animator Controller original
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

            // Reemplazar la primera animación en el Animator Override Controller con el Animation Clip especificado
            animatorOverrideController["DefaultAnimation"] = IntroduccionSarten;

            // Asignar el AnimatorOverrideController al Animator
            animator.runtimeAnimatorController = animatorOverrideController;            

            // Reproducir la animación especificada
            sartenColocada.Play();
            animator.Play("DefaultAnimation");            

            StartCoroutine(WaitForAnimationToEnd(IntroduccionSarten.length));
        }
        else
        {
            if (animator == null)
                Debug.LogError("No se encontró el componente Animator en el GameObject.");
            if (IntroduccionSarten == null)
                Debug.LogError("No se ha asignado ningún Animation Clip.");
        }
    }

    private IEnumerator WaitForAnimationToEnd(float duration)
    {
        // Esperar el tiempo que dura la animación
        yield return new WaitForSeconds(duration);
        
        gameObject.SetActive(false);
        musicManager.MusicaFondo();
    }
}
