using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Registries
{
    public class ScriptableObjectInstanceFinder
    {
        public static List<T> GetAllInstances<T>() where T : ScriptableObject
        {
            return AssetDatabase.FindAssets($"t: { typeof(T).Name}").ToList()
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<T>)
            .ToList();
        }
    }
}
