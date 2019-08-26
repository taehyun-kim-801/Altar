using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private float hunger;
    public float Hunger => hunger;

    private bool isAttacked;

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
        base.Die();
    }

    public override void GetAttack(int damage)
    {
        if (!isAttacked)
        {
            Health -= damage;

            if (health <= 0)
            {
                Die();
                return;
            }

            isAttacked = true;
            StartCoroutine(Invincible());

            Debug.Log("Player Health: " + health + " Hunger: " + hunger);
        }
    }

    public IEnumerator Invincible()
    {
        yield return new WaitForSeconds(invincibleTime);

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        isAttacked = false;
    }

}
