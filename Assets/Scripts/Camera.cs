using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
            this.transform.position = player.position + new Vector3(0, 0, -10);
    }
}
