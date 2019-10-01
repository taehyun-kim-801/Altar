using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool Mute => mute;

    public GameObject startCanvas;
    public GameObject pauseUI;
    public GameObject mainCanvas;
    public GameObject player;
    public GameObject gameOverCanvas;

    public int caughtMonsterCount { get; private set; }
    public float startTime { get; private set; }

    [SerializeField]
    private bool mute;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DataContainer.SetDataContainer();

        LoadSetting();
        Time.timeScale = 0f;
    }

    IEnumerator Start()
    {
        StartCoroutine(GetComponent<MapManager>().ChangeScene());
        Player.Instance.transform.position = Vector3.zero;

        DontDestroyOnLoad(mainCanvas);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);

        while (!GetComponent<MapManager>().isLoaded) yield return null;

        SetStartUI();
    }

    private IEnumerator MainCanvasSetActive(GameObject startUI)
    {
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        Time.timeScale = 1f;
        mainCanvas.SetActive(true);
        Destroy(startUI);
        startTime = Time.time;
    }

    private void LoadSetting()
    {
        if(File.Exists($"{Application.dataPath}/Data/{nameof(PlayerInfo)}.json"))
        {
            PlayerInfo info = JsonManager.LoadJson<PlayerInfo>()[0];
            GetComponent<MapManager>().nextScene = info.activeScene;
            Player.Instance.transform.position = info.position;
            Player.Instance.SetInventory(info.inventory);
            Player.Instance.SetInvenQuantity(info.invenQuantity);
            foreach(var recipe in info.pinnedRecipe)
            {
                Player.Instance.AddRecipe(recipe);
            }
            Player.Instance.Health = info.health;
            Player.Instance.Hunger = info.hunger;
        }
        else
        {
            GetComponent<MapManager>().nextScene = "Grave";
        }
    }

    public void GameExit()
    {
        List<PlayerInfo> playerInfo = new List<PlayerInfo>();
        playerInfo.Add(new PlayerInfo(Player.Instance));

        JsonManager.SaveJson(playerInfo);
    }

    public void GamePause()
    {
        Instantiate(pauseUI, FindObjectOfType<Canvas>().transform);
    }

    public void CountCaughtMonster()
    {
        caughtMonsterCount++;
    }

    public void GameOver()
    {
        Instantiate(gameOverCanvas);
    }

    public void SetStartUI()
    {
        GameObject startUI = Instantiate(startCanvas);
        mainCanvas.SetActive(false);

        StartCoroutine(MainCanvasSetActive(startUI));
    }
}
