using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public static class DataContainer
{
    public static SpriteAtlas monsterAtlas;
    public static SpriteAtlas itemAtlas;
    public static SpriteAtlas projectileAtlas;

    public static void SetDataContainer()
    {
        monsterAtlas = Resources.Load<SpriteAtlas>("Units");
        itemAtlas = Resources.Load<SpriteAtlas>("Item");
        projectileAtlas = Resources.Load<SpriteAtlas>("Projectile");
    }
}
