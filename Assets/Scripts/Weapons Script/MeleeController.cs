using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : WeaponController
{
    [SerializeField] AudioClip meleeClip;
    public override void ProcessAttack()
    {
        AudioSource.PlayClipAtPoint(meleeClip,Camera.main.transform.position, 1f);
    }
}
