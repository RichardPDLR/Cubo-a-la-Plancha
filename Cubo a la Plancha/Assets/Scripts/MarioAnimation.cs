using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarionAnimation : MonoBehaviour
{
    public AnimationClip IntroduccionMario; // Arrastra tu Animation Clip aquí en el Inspector
    private Animator animator;
    public AudioSource marioCaminando;
    private AnimatorOverrideController animatorOverrideController;

    public string sceneName;

    void Start()
    {
        // Obtener la referencia al componente Animator
        animator = GetComponent<Animator>();
        marioCaminando.Play();

        if (animator != null && IntroduccionMario != null)
        {
            // Crear un AnimatorOverrideController basado en el Animator Controller original
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

            // Reemplazar la primera animación en el Animator Override Controller con el Animation Clip especificado
            animatorOverrideController["DefaultAnimation"] = IntroduccionMario;

            // Asignar el AnimatorOverrideController al Animator
            animator.runtimeAnimatorController = animatorOverrideController;

            // Reproducir la animación especificada
            animator.Play("DefaultAnimation");            

            StartCoroutine(WaitForAnimationToEnd(IntroduccionMario.length));
        }
        else
        {
            if (animator == null)
                Debug.LogError("No se encontró el componente Animator en el GameObject.");
            if (IntroduccionMario == null)
                Debug.LogError("No se ha asignado ningún Animation Clip.");
        }
    }

    private IEnumerator WaitForAnimationToEnd(float duration)
    {
        // Esperar el tiempo que dura la animación
        yield return new WaitForSeconds(duration);

        // Cambiar de escena
        SceneManager.LoadScene(sceneName);
    }

    
    // public float runSpeed = 7;
    // public float rotationSpeed = 250;

    // public Animator animator;

    // private float x, y;

    // // Update is called once per frame
    // void Update()
    // {
    //     x = Input.GetAxis("Horizontal");
    //     y = Input.GetAxis("Vertical");

    //     transform.Rotate(0,x * Time.deltaTime * rotationSpeed, 0);
    //     transform.Translate(0,0,y * Time.deltaTime * runSpeed);

    //     animator.SetFloat("VelX", x);
    //     animator.SetFloat("VelY", y);

    // }
}
