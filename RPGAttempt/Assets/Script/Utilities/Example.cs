using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public Animator animator;
    public AnimatorParameter animatorParameter;

    [ContextMenu(nameof(Apply))]

    public void Apply()
    {
        Debug.Log(animatorParameter.Type + " - ");
        switch (animatorParameter.Type)
        {
            case AnimatorControllerParameterType.Float:
                animator.SetFloat(animatorParameter, Random.value);
                break;
            case AnimatorControllerParameterType.Int:
                animator.SetInteger(animatorParameter, Random.Range(0, 5));
                break;
            case AnimatorControllerParameterType.Bool:
                animator.SetBool(animatorParameter, !animator.GetBool(animatorParameter));
                break;
            case AnimatorControllerParameterType.Trigger:
                animator.SetTrigger(animatorParameter);
                break;
        }
    }
}
