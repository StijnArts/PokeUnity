using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
public static class SubTypeReflector<T>
{
    public static List<T> FindSubTypeClasses()
    {
        var types = Assembly.GetExecutingAssembly().ManifestModule.GetTypes().Where(type => type.BaseType == typeof(T) && !type.IsAbstract).ToArray();

       return types.Select(abilityType => (T)Activator.CreateInstance(abilityType)).Where(instance => instance is T).ToList();
    }
}