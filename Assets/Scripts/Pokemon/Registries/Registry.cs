using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
public static class Registry<T>
{
    public static List<T> FindRegistryChildClasses()
    {
        var types = Assembly.GetExecutingAssembly().ManifestModule.GetTypes().Where(type => type.BaseType == typeof(T)).ToArray();

       return types.Select(abilityType => (T)Activator.CreateInstance(abilityType)).Where(abilityInstance => abilityInstance is T).ToList();
    }
}