using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedDeadFX : StateMachineBehaviour
{
    [SerializeField] int index;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<ZombieController>().ActivateDeadFX(index);
    }
}
