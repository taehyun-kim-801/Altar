﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    public System.Action<Collider2D> triggerFunc;
    public System.Action UseItem;
    public Player PlayerInstance => player;

    private Player player;
    private Item selectedItem;
    private bool isSwinging = false;
    private SpriteRenderer itemSpriteRenderer;
    private WaitForSeconds swingWaitSeconds = new WaitForSeconds(0.02f);

    private void Awake()
    {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Bind(Player player) => this.player = player;

    public void Equip(Item item)
    {
        selectedItem = item;
        item.Equip(this);
        itemSpriteRenderer.sprite = item.ItemSprite;
    }

    private IEnumerator Swing()
    {
        Quaternion rotation = transform.rotation;
        while (rotation.z < transform.rotation.z + 120)
        {
            transform.Rotate(20, 0, 0);
            yield return swingWaitSeconds;
        }

        transform.Rotate(-120, 0, 0);

        yield return selectedItem.itemDelaySeconds;
        isSwinging = false;

        yield return null;
    }

    public void UseEquippedItem()
    {
        if (isSwinging)
            return;
        StartCoroutine("Swing");
        UseItem?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision) => triggerFunc?.Invoke(collision);
}