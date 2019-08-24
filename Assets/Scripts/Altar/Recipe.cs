using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public Dictionary<string, int> sacrificeDictionary {get; private set;}
    public string Result => result;

    [SerializeField]
    private List<string> sacrificeNameList;
    [SerializeField]
    private List<int> sacrificeCountList;
    [SerializeField]
    private string result;

    public Recipe(Dictionary<string, int> sacrifice, string result)
    {
        sacrificeDictionary = sacrifice;
        sacrificeNameList = new List<string>();
        sacrificeCountList = new List<int>();
        foreach(var s in sacrifice)
        {
            sacrificeNameList.Add(s.Key);
            sacrificeCountList.Add(s.Value);
        }
        this.result = result;
    }

    public void ListToDictionary()
    {
        sacrificeDictionary = new Dictionary<string, int>();
        for(int i = 0; i<sacrificeNameList.Count; i++)
            sacrificeDictionary.Add(sacrificeNameList[i], sacrificeCountList[i]);
    }
}
