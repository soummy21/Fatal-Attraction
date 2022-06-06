using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSMB : StateMachineBehaviour
{

    int randomAnimations = 4;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int randomStates = Random.Range(1, randomAnimations);
        animator.SetInteger(TagManager.RANDOM_PARAMETER, randomStates);
    }

}
