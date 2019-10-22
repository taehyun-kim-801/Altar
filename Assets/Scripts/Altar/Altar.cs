using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private GameObject altarUI;

    private bool isOpenUI = false;

    public void OpenAltarUI()
    {
        if(isOpenUI)
        return;
        isOpenUI = true;
        Instantiate(altarUI, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<AltarUI>().close += ()=>isOpenUI = false;
    }
}
