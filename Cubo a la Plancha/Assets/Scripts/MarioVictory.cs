using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioVictory : MonoBehaviour
{
    public AnimationClip VictoriaMario;
    public AnimationClip IdleMario;
    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null && VictoriaMario != null)
        {            
            animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            
            animatorOverrideController["VictoryAnimation"] = VictoriaMario;
            
            animator.runtimeAnimatorController = animatorOverrideController;
            
            animator.Play("VictoryAnimation");

            StartCoroutine(WaitForAnimationToEnd(VictoriaMario.length));
        }
    }

    private IEnumerator WaitForAnimationToEnd(float duration)
    {        
        yield return new WaitForSeconds(duration);


        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            
        animatorOverrideController["DefaultAnimation"] = IdleMario;
            
        animator.runtimeAnimatorController = animatorOverrideController;
            
        animator.Play("DefaultAnimation");
    }
}
