using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public GameObject eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(eventSystem);
        DontDestroyOnLoad(gameObject);
    }
}
