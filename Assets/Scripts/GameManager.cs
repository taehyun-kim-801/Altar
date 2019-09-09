using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public GameObject eventSystem;
    // Start is called before the first frame update
    private void Awake()
    {
        DataContainer.SetDataContainer();
    }
    void Start()
    {
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(eventSystem);
        DontDestroyOnLoad(gameObject);
    }

    public void GameExit()
    {
        List<PlayerInfo> playerInfo = new List<PlayerInfo>();
        playerInfo.Add(new PlayerInfo(Player.Instance));

        JsonManager.SaveJson<PlayerInfo>(playerInfo);
    }
}
