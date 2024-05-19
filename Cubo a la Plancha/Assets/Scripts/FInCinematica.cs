using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FInCinematica : StateMachineBehaviour
{
    public string sceneName; // Nombre de la escena a la que quieres cambiar

    // Este método se llama cuando la animación termina de reproducirse
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(sceneName);
    }
}
