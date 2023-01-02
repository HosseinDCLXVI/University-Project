using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsCrouch", false);
        ProgressManager CC= FindObjectOfType<ProgressManager>();
        CC.IsCrouch = false;
    }
}
