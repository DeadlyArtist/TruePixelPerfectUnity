using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class SerializationHelpers
{
    public static object GetEncapsulatingObject(this SerializedProperty serializedProperty)
    {
        var path = serializedProperty.propertyPath;
        var propertyPathList = path.Split('.');
        object targetObject = serializedProperty.serializedObject.targetObject;
        var removeLast = 1;
        if (propertyPathList[propertyPathList.Length - 1].EndsWith("]"))
        {
            removeLast = 3;
        }
        for (int i = 0; i < propertyPathList.Length - removeLast; i++)
        {
            var propertyName = propertyPathList[i];
            var targetType = targetObject.GetType();
            if (propertyName.EndsWith("]"))
            {
                var index = propertyName.FromToMatch("[", "]").ParseInt();
                targetObject = ((IList)targetObject)[index];
                continue;
            }
            else
            {
                var property = targetType.GetProperty(propertyName, ReflectionHelpers.AllBindingFlags);
                if (property != null)
                {
                    targetObject = property.GetValue(targetObject, null);
                    continue;
                }

                var field = targetType.GetField(propertyName, ReflectionHelpers.AllBindingFlags);
                if (field != null)
                {
                    targetObject = field.GetValue(targetObject);
                    continue;
                }
            }
        }

        return targetObject;
    }
}
