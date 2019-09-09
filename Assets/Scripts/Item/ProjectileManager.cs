﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    [SerializeField]
    GameObject projectileObject;
    [SerializeField]
    private SpriteAtlas projectileSpriteAtlas;
    private List<GameObject> projectileObjectList;
    private Dictionary<string, ProjectileInfo> projectileDictionary;
    private int projectileNumber = 20;
    private int activatedProjectileNumber = 0;

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
        SaveProjectileJson();
    }

    private void Start()
    {
        projectileObjectList = new List<GameObject>();
        for (int i = 0; i < projectileNumber; i++)
        {
            projectileObjectList.Add(Instantiate(projectileObject));
            projectileObjectList[i].SetActive(false);
        }
    }

    public void ActivateProjectile(string projectileName, Vector3 startPoint, Vector3 direction)
    {
        projectileObjectList[activatedProjectileNumber].SetActive(true);
        ProjectileInfo projectileInfo = projectileDictionary[projectileName];
        projectileObjectList[activatedProjectileNumber].GetComponent<Projectile>().Set(activatedProjectileNumber++, projectileInfo.speed, projectileInfo.damage, projectileInfo.distance,
            startPoint, direction, projectileSpriteAtlas.GetSprite(projectileName));
        activatedProjectileNumber++;
    }

    public void DeactivateProjectile(int index)
    {
        activatedProjectileNumber--;
        GameObject projectile = projectileObjectList[index];
        projectileObjectList[index] = projectileObjectList[activatedProjectileNumber];
        projectileObjectList[activatedProjectileNumber] = projectile;
    }

    public float GetProjectileDamage(string projectileName)
    {
        return projectileDictionary[projectileName].damage;
    }

    [System.Serializable]
    private struct ProjectileInfo
    {
        public ProjectileInfo(string name, float speed, float damage, float distance)
        {
            this.name = name;
            this.speed = speed;
            this.damage = damage;
            this.distance = distance;
        }

        public string name;
        public float speed;
        public float damage;
        public float distance;
    }

    private void SaveProjectileJson()
    {
        List<ProjectileInfo> projectileInfoList = new List<ProjectileInfo>();
        projectileInfoList.Add(new ProjectileInfo("FireBall", 2f, 3, 10));
        projectileInfoList.Add(new ProjectileInfo("MiniSpear", 4f, 3, 7));
        projectileInfoList.Add(new ProjectileInfo("LargeSpear", 2f, 5, 7));

        JsonManager.SaveJson(projectileInfoList);
    }

    private void LoadProjectileJson()
    {
        projectileDictionary = new Dictionary<string, ProjectileInfo>();
        JsonManager.LoadJson<ProjectileInfo>().ForEach((projectileInfo) => { projectileDictionary.Add(projectileInfo.name, projectileInfo); });
    }
}
