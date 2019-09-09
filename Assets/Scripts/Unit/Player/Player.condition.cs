﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private float hunger;
    public float Hunger { get => hunger; set { hunger = value; gameManager.SendMessage("HungerUI"); } }

    private bool canMove;
    private bool isAttacked;

    private static Color[] blinkColor;

    public override int Health { get => base.Health; set { health = value; gameManager.SendMessage("HealthUI"); } }

    public IEnumerator DecreaseHealth()
    {
        while(health<=0)
        {
            yield return new WaitForSeconds(2f);
            health--;
        }
    }

    protected override void Die()
    {
        Time.timeScale = 0f;
        StopAllCoroutines();
        Destroy(gameObject);
    }

    public IEnumerator Invincible()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(invincibleTime);

        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isAttacked = false;
    }

    public IEnumerator Blink()
    {
        int i = 0;
        while(isAttacked)
        {
            yield return new WaitForSeconds(0.125f);
            GetComponent<SpriteRenderer>().color = blinkColor[i % 2];
            i++;
        }

        GetComponent<SpriteRenderer>().color = blinkColor[1];
    }
}
