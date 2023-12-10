using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;
using System;

public static class AnimatorExtensions
{
    public static void SetFloat(this Animator animator, AnimatorParameter animatorParameter, float value)
    {
        if (animatorParameter.Type != AnimatorControllerParameterType.Float)
        {
            throw new ArgumentException("Given parameter is not of type Float!");
        }

        if (animator.runtimeAnimatorController != animatorParameter.RuntimeAnimatorController)
        {
            Debug.LogWarning("AnimatorControllers do not match!");
        }

        animator.SetFloat(animatorParameter.Hash, value);
    }

    public static void SetInteger(this Animator animator, AnimatorParameter animatorParameter, int value)
    {
        if (animatorParameter.Type != AnimatorControllerParameterType.Int)
        {
            throw new ArgumentException("Given parameter is not of type Int!");
        }

        if (animator.runtimeAnimatorController != animatorParameter.RuntimeAnimatorController)
        {
            Debug.LogWarning("AnimatorControllers do not match!");
        }

        animator.SetInteger(animatorParameter.Hash, value);
    }

    public static void SetBool(this Animator animator, AnimatorParameter animatorParameter, bool value)
    {
        if (animatorParameter.Type != AnimatorControllerParameterType.Bool)
        {
            throw new ArgumentException("Given parameter is not of type Bool!");
        }

        if (animator.runtimeAnimatorController != animatorParameter.RuntimeAnimatorController)
        {
            Debug.LogWarning("AnimatorControllers do not match!");
        }

        animator.SetBool(animatorParameter.Hash, value);
    }

    public static void SetTrigger(this Animator animator, AnimatorParameter animatorParameter)
    {
        if (animatorParameter.Type != AnimatorControllerParameterType.Trigger)
        {
            throw new ArgumentException("Given parameter is not of type Trigger!");
        }

        if (animator.runtimeAnimatorController != animatorParameter.RuntimeAnimatorController)
        {
            Debug.LogWarning("AnimatorControllers do not match!");
        }

        animator.SetTrigger(animatorParameter.Hash);
    }

    public static void ResetTrigger(this Animator animator, AnimatorParameter animatorParameter)
    {
        if (animatorParameter.Type != AnimatorControllerParameterType.Trigger)
        {
            throw new ArgumentException("Given parameter is not of type Trigger!");
        }

        if (animator.runtimeAnimatorController != animatorParameter.RuntimeAnimatorController)
        {
            Debug.LogWarning("AnimatorControllers do not match!");
        }

        animator.ResetTrigger(animatorParameter.Hash);
    }

    public static float GetFloat(this Animator animator, AnimatorParameter animatorParameter)
    {
        if (animatorParameter.Type != AnimatorControllerParameterType.Float)
        {
            throw new ArgumentException("Given parameter is not of type Float!");
        }

        if (animator.runtimeAnimatorController != animatorParameter.RuntimeAnimatorController)
        {
            Debug.LogWarning("AnimatorControllers do not match!");
        }

        return animator.GetFloat(animatorParameter.Hash);
    }

    public static int GetInteger(this Animator animator, AnimatorParameter animatorParameter)
    {
        if (animatorParameter.Type != AnimatorControllerParameterType.Int)
        {
            throw new ArgumentException("Given parameter is not of type Integer!");
        }

        if (animator.runtimeAnimatorController != animatorParameter.RuntimeAnimatorController)
        {
            Debug.LogWarning("AnimatorControllers do not match!");
        }

        return animator.GetInteger(animatorParameter.Hash);
    }

    public static bool GetBool(this Animator animator, AnimatorParameter animatorParameter)
    {
        if (animatorParameter.Type != AnimatorControllerParameterType.Bool)
        {
            throw new ArgumentException("Given parameter is not of type Bool!");
        }

        if (animator.runtimeAnimatorController != animatorParameter.RuntimeAnimatorController)
        {
            Debug.LogWarning("AnimatorControllers do not match!");
        }

        return animator.GetBool(animatorParameter.Hash);
    }
}

[Serializable]
public class AnimatorParameter
{
    [SerializeField]
    private RuntimeAnimatorController animator;

    [SerializeField]
    private AnimatorControllerParameterType type;

    [SerializeField]
    private string name;

    private int? hash;

    public AnimatorControllerParameterType Type => type;
    public string Name => name;
    public RuntimeAnimatorController RuntimeAnimatorController;

    public int Hash
    {
        get
        {
            if (hash == null)
            {
                hash = Animator.StringToHash(name);
            }

            return hash.Value;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(AnimatorParameter))]
    private class AnimatorParameterDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                position = EditorGUI.PrefixLabel(position, label);

                var controllerRect = position;
                controllerRect.height = EditorGUIUtility.singleLineHeight;
                position.y += controllerRect.height;

                var controllerProperty = property.FindPropertyRelative(nameof(animator));

                using (var change = new EditorGUI.ChangeCheckScope())
                {
                    EditorGUI.PropertyField(controllerRect, controllerProperty, GUIContent.none);

                    if (!controllerProperty.objectReferenceValue)
                    {
                        var infoRect = position;
                        infoRect.height = EditorGUIUtility.singleLineHeight;
                        EditorGUI.HelpBox(infoRect, "No animator referenced!", MessageType.Error);
                    }
                    else
                    {
                        var typeRect = position;
                        typeRect.width = 80;
                        typeRect.height = EditorGUIUtility.singleLineHeight;
                        position.x += typeRect.width;

                        var nameRect = position;
                        nameRect.height = EditorGUIUtility.singleLineHeight;
                        nameRect.width -= typeRect.width;

                        var typeProperty = property.FindPropertyRelative(nameof(type));
                        EditorGUI.PropertyField(typeRect, typeProperty, GUIContent.none);
                        var options = ((AnimatorController)controllerProperty.objectReferenceValue).parameters
                            .Where(p => p.type == (AnimatorControllerParameterType)typeProperty.enumValueFlag)
                            .Select(p => p.name)
                            .ToList();

                        var nameProperty = property.FindPropertyRelative(nameof(name));
                        var currentIndex = options.IndexOf(nameProperty.stringValue);

                        var newIndex = EditorGUI.Popup(nameRect, currentIndex, options.ToArray());

                        nameProperty.stringValue = newIndex >= 0 && newIndex < options.Count ? options[newIndex] : "";
                    }

                    if (change.changed)
                    {
                        var target = (AnimatorParameter)fieldInfo.GetValue(property.serializedObject.targetObject);
                        target.hash = null;
                        fieldInfo.SetValue(property.serializedObject.targetObject, target);
                    }
                }
            }
        }
    }
#endif
}
