using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    public System.Action<Collider2D> triggerFunc;
    public System.Action UseItem;
    public Player player { get; private set; }
    public Item selectedItem { get; private set; }

    private bool isSwinging = false;
    private SpriteRenderer itemSpriteRenderer;
    private WaitForSeconds swingWaitSeconds = new WaitForSeconds(0.02f);
    private WaitForSeconds itemDelaySeconds;

    private void Awake()
    {
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Bind(Player player) => this.player = player;

    public void Equip(Item item = null)
    {
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
    }

    public void UseEquippedItem() => UseItem?.Invoke();

    private void OnTriggerEnter2D(Collider2D collision) => triggerFunc?.Invoke(collision);
}
