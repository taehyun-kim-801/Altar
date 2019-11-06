using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class MonsterManager
{
    public static Dictionary<string, List<string>> monstersByMap { get; private set; }
    public static Dictionary<string, GameObject> monsterObjects { get; private set; }

    static MonsterManager()
    {
        monstersByMap = new Dictionary<string, List<string>>();
        monsterObjects = new Dictionary<string, GameObject>();
        
        if(!File.Exists($"{Application.dataPath}/Data/MonsterInfo.json"))
        {
            FileStream fs = File.Create($"{Application.dataPath}/Data/MonsterInfo.json");
            fs.Close();
            JsonManager.SaveJson(SaveMonsters());
        }

        if (!File.Exists($"{Application.dataPath}/Data/MonsterList.json"))
        {
            FileStream fs = File.Create($"{Application.dataPath}/Data/MonsterList.json");
            fs.Close();
            JsonManager.SaveJson(SaveLists());
        }

        LoadMonstersJson();
    }

    private static void LoadMonstersJson()
    {
        foreach(var monster in GameManager.Instance.monsterObject)
        {
            monsterObjects.Add(monster.name, monster);
        }

        JsonManager.LoadJson<MonsterList>().ForEach((list) => { monstersByMap.Add(list.name, list.monsters); });
    }

    [System.Serializable]
    private struct MonsterList
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public List<string> monsters;

        public MonsterList(string name, List<string> monsters)
        {
            this.name = name;
            this.monsters = monsters;
        }
    }

    private static List<MonsterInfo> SaveMonsters()
    {
        List<MonsterInfo> result = new List<MonsterInfo>();

        result.Add(new MonsterInfo("Skeleton", 10, 1, "Bone"));
        result.Add(new MonsterInfo("MiniOrk", 10, 1, "Mask"));
        result.Add(new MonsterInfo("RipDevil", 10, 1, "Devil's Rip"));
        result.Add(new MonsterInfo("MagicianOrk", 20, 2, "Staff"));
        result.Add(new MonsterInfo("Ghost", 20, 2, "Hoot"));
        result.Add(new MonsterInfo("Slime", 20, 2, "Jelly"));
        result.Add(new MonsterInfo("StoneGolem", 30, 3, "Stone"));
        result.Add(new MonsterInfo("Satan", 30, 3, "Tooth"));
        result.Add(new MonsterInfo("CaptainOrk", 30, 3, "Leather"));

        return result;
    }

    private static List<MonsterList> SaveLists()
    {
        List<MonsterList> result = new List<MonsterList>();

        result.Add(new MonsterList("Grave", new List<string> { "Skeleton", "Slime", "StoneGolem" }));
        result.Add(new MonsterList("Hell", new List<string> { "RipDevil", "Ghost", "Satan" }));
        result.Add(new MonsterList("Forest", new List<string> { "MiniOrk", "MagicianOrk", "CaptainOrk" }));
        return result;
    }
}
