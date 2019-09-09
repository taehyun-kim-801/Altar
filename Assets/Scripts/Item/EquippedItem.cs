﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    public enum State
    {
        Swing,
        Use,
        None
    }

    public System.Action<Collider2D> triggerFunc;
    public System.Action UseItem;
    public Item selectedItem { get; private set; }
    public State state { get; private set; }

    private SpriteRenderer itemSpriteRenderer;
    private WaitForSeconds swingWaitSeconds = new WaitForSeconds(0.02f);
    private WaitForSeconds itemDelaySeconds;

    private void Awake()
    {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        state = State.None;
    }

    public void Equip(Item item = null)
    {
        if (state != State.None)
            return;

        triggerFunc = null;
        UseItem = null;
        selectedItem = null;
        itemSpriteRenderer.sprite = null;

        if (item == null)
            return;

        selectedItem = item;
        item.Equip(this);
        itemSpriteRenderer.sprite = item.sprite;
        itemDelaySeconds = new WaitForSeconds(item.Delay);


        if (gameObject.GetComponent<PolygonCollider2D>() != null)
            DestroyImmediate(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }

    private IEnumerator Swing()
    {
        state = State.Swing;

        gameObject.GetComponent<PolygonCollider2D>().enabled = true;

        for (int i = 0; i < 6; i++)
        {
            transform.RotateAround(Player.Instance.transform.position, Vector3.back, 20);
            yield return swingWaitSeconds;
        }

        gameObject.GetComponent<PolygonCollider2D>().enabled = false;

        for (int i = 0; i < 6; i++)
        {
            transform.RotateAround(Player.Instance.transform.position, Vector3.forward, 20);
            yield return swingWaitSeconds;
        }

        state = State.Use;
        yield return itemDelaySeconds;
        state = State.None;

        yield break;
    }

    public void UseEquippedItem()
    {
        if (state != State.None)
            return;

        StartCoroutine("Swing");
        UseItem?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision) => triggerFunc?.Invoke(collision);
}