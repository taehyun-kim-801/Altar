using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private GameObject altarUI;

    public void OpenAltarUI()
    {
        Instantiate(altarUI, GameObject.FindGameObjectWithTag("Canvas").transform);
    }
}
