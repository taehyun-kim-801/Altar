using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadMonstersJson()
    {
        string json = File.ReadAllText($"{Application.dataPath}/Data/{nameof(MapManager)}.json");
    }
}
