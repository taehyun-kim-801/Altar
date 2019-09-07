using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterInfo
{
    [SerializeField]
    private string name;
    [SerializeField]
    private int health;
    [SerializeField]
    private int damage;
    [SerializeField]
    private string dropItem;

    public string Name => name;
    public int Health => health;
    public int Damage => damage;
    public string DropItem => dropItem;

    public MonsterInfo(string name,int health,int damage,string dropItem)
    {
        this.name = name;
        this.health = health;
        this.damage = damage;
        this.dropItem = dropItem;
    }
}
