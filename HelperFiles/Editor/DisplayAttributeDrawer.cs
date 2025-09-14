using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DisplayAttribute))]
public class DisplayAttributeDrawer : PropertyDrawer
{
    // Necessary since some properties tend to collapse smaller than their content
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var displayAttribute = (DisplayAttribute)attribute;
        var declaringType = this.fieldInfo.DeclaringType;
        var encapsulatingObject = property.GetEncapsulatingObject();
        object value = declaringType.GetField(displayAttribute.name, ReflectionHelpers.AllBindingFlags)?.GetValue(encapsulatingObject);
        value ??= declaringType.GetProperty(displayAttribute.name, ReflectionHelpers.AllBindingFlags)?.GetValue(encapsulatingObject);
        value ??= declaringType.GetMethod(displayAttribute.name, ReflectionHelpers.AllBindingFlags).Invoke(encapsulatingObject, displayAttribute.parameters);
        declaringType.SetValue(encapsulatingObject, fieldInfo.Name, value);

        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
