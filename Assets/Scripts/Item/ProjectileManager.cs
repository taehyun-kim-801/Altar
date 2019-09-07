using System.Collections;
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
        public string name;
        public float speed;
        public float damage;
        public float distance;
    }
}
