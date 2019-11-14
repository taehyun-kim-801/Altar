using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

    public List<GameObject> monsterObject;

    [SerializeField]
    private bool mute;

    [SerializeField]
    private GameObject pinnedRecipeUI;

    private bool isLoaded = false;

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
        Time.timeScale = 0f;
    }

    void Start()
    {
        StartCoroutine(LoadSetting());

        Player.Instance.transform.position = Vector3.zero;

        DontDestroyOnLoad(mainCanvas);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator MainCanvasSetActive(GameObject startUI)
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        Time.timeScale = 1f;
        mainCanvas.SetActive(true);
        Destroy(startUI);
        startTime = Time.time;
    }

    private IEnumerator LoadSetting()
    {
        yield return new WaitUntil(() => Player.Instance.isCreated);

        if (File.Exists($"{Application.dataPath}/Data/{nameof(PlayerInfo)}.dat"))
        {
            var file = new FileStream($"{Application.dataPath}/data/PlayerInfo.dat", FileMode.Open);
            var deserializer = new BinaryFormatter();

            var info = deserializer.Deserialize(file) as PlayerInfo;
            file.Close();
            GetComponent<MapManager>().nextScene = info.activeScene;
            StartCoroutine(GetComponent<MapManager>().ChangeScene(()=>SetStartUI()));
            while (!GetComponent<MapManager>().isLoaded) yield return null;
            Player.Instance.transform.position = new Vector3(info.posX, info.posY);
            Player.Instance.SetInventory(info.inventory);
            Player.Instance.SetInvenQuantity(info.invenQuantity);
            Player.Instance.SetItemCells();
            Player.Instance.SelectItem(0);
            foreach (var recipe in info.pinnedRecipe)
            {
                Player.Instance.AddRecipe(recipe);
            }
            Player.Instance.Health = info.health;
            Player.Instance.Hunger = info.hunger;
        }
        else
        {
            GetComponent<MapManager>().nextScene = "Lobby";
            StartCoroutine(GetComponent<MapManager>().ChangeScene(()=>SetStartUI()));
            while (!GetComponent<MapManager>().isLoaded) yield return null;
            Player.Instance.transform.position = Vector3.zero;
            Player.Instance.SetInventory();
            Player.Instance.SetInvenQuantity();
            Player.Instance.FirstSetting();
            Player.Instance.SetItemCells();
            Player.Instance.PinnedRecipes.Clear();
            Player.Instance.Health = 10;
            Player.Instance.Hunger = 100f;
            Player.Instance.isHungerZero = false;
        }
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
        File.Delete($"{Application.dataPath}/Data/{nameof(PlayerInfo)}.dat");
    }

    public void SetStartUI()
    {
        Time.timeScale = 0f;
        GameObject startUI = Instantiate(startCanvas);
        mainCanvas.SetActive(false);

        StartCoroutine(MainCanvasSetActive(startUI));
    }

    public void GameReset()
    {
        caughtMonsterCount = 0;
        Time.timeScale = 0f;
        var droppedItems = GameObject.FindGameObjectsWithTag("DroppedItem");
        foreach(var item in droppedItems)
        {
            Destroy(item);
        }
        GetComponent<MapManager>().ResetObjectPool();
        Player.Instance.gameObject.SetActive(true);
        LoadSetting();
        StartCoroutine(GetComponent<MapManager>().ChangeScene(() => SetStartUI()));
    }

    public void OpenPinnedRecipeUI()
    {
        Instantiate(pinnedRecipeUI, GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    public void GameExit()
    {
        var file = new FileStream($"{Application.dataPath}/data/PlayerInfo.dat", FileMode.OpenOrCreate);
        var serializer = new BinaryFormatter();

        PlayerInfo info = new PlayerInfo(Player.Instance);
        serializer.Serialize(file, info);
        file.Close();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}