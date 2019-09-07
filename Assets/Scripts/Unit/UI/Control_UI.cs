using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Control_UI : MonoBehaviour
{
    public GameObject stickBackground;
    public Transform stick;
    public GameObject playerObj;

    private Player player;
    private Vector3 stickFirstPos;
    private Vector3 stickDirection;
    private float radius;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Player>();
        radius = stickBackground.GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        stickFirstPos = stick.transform.position;

        float canvas = stickBackground.transform.parent.GetComponent<RectTransform>().localScale.x;
        radius *= canvas;
    }

    //Joystick
    public void JoystickDrag(BaseEventData _data)
    {
        PointerEventData data = _data as PointerEventData;
        Vector3 pos = data.position;

        stickDirection = (pos - stickFirstPos).normalized;
        float distance = Vector3.Distance(pos, stickFirstPos);
        if(distance<radius)
        {
            stick.position = pos;
        }
        else
        {
            stick.position = stickFirstPos + stickDirection * radius;
        }

        player.faceDirection = stickDirection;
    }

    public void JoystickDragEnd()
    {
        stick.position = stickFirstPos;
        stickDirection = Vector3.zero;
        player.faceDirection = stickDirection;
    }
}
