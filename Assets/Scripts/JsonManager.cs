using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonManager
{
    private class ObjectList<T>
    {
        public List<T> objects;

        public ObjectList(List<T> ts)
        {
            objects = ts;
        }
    }

    public static List<T> LoadJson<T>()
    {
        string json = File.ReadAllText($"{Application.dataPath}/Data/{typeof(T).Name}.json");
        return JsonUtility.FromJson<ObjectList<T>>(json).objects;
    }

    public static void SaveJson<T>(List<T> objects)
    {
        ObjectList<T> objectList = new ObjectList<T>(objects);
        File.WriteAllText($"{Application.dataPath}/Data/{typeof(T).Name}.json", JsonUtility.ToJson(objectList));
    }
}
