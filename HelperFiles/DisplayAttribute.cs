using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class DisplayAttribute : PropertyAttribute
{
    public readonly string name;
    public readonly object[] parameters;

    public DisplayAttribute(string name, params object[] parameters)
    {
        this.name = name;
        this.parameters = parameters;
    }
}