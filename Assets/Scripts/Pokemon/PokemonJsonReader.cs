using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PokemonJsonReader : ScriptableObject
{
    
    public static T getPokemonData<T>(string jsonPath)
    {
        if (File.Exists(jsonPath))
        {
            return JsonUtility.FromJson<T>(ReadFile(jsonPath));
        }
        else
        {
            Debug.Log("Did not find file at: " + jsonPath);
            return default(T);
        }
    }

    

    public static Pokemon loadFromSaveData()
    {
        //Should read the save data and load the pokemon accordingly
        return null;
    }


    private static string ReadFile(string path)
    {
            using (StreamReader reader = new StreamReader(path))
            {
                string content = reader.ReadToEnd();
                return content;
            }
    }
}
