using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityScript.Lang;
using Array = System.Array;

[System.Serializable]
public class ConfigHandler : Dictionary<string,object>
{
    private const string _path = "Assets/Scripts/config.json";
    
    
    
    public static ConfigHandler Load(string path = _path) {
        using (StreamReader r = new StreamReader(path)) {
            string json = r.ReadToEnd();
            return JsonUtility.FromJson<ConfigHandler>(json);
        }
    }
    
    public void Save(string path = _path)
    {
        string json = JsonUtility.ToJson(this);
        Debug.Log(json);
        using (StreamWriter r = new StreamWriter(path)) {
            r.Write(json);
        }
    }
}