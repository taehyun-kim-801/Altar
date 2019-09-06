using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject.transform;
    }
    void Update()
    {
        if (player != null)
            this.transform.position = player.position + new Vector3(0, 0, -10);
    }
}
