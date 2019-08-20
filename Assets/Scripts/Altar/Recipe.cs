using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public readonly Dictionary<string, int> sacrifice;
    public readonly string result;

    Recipe(Dictionary<string, int> sacrifice, string result)
    {
        this.sacrifice = sacrifice;
        this.result = result;
    }
}
