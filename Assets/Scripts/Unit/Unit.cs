using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected int health;
    public virtual int Health { get { return health; } set { health = value; } }

    public Vector3 faceDirection;

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public float moveSpeed;

    public void Move(Vector3 direction)
    {
        gameObject.transform.Translate(moveSpeed * direction * Time.deltaTime);
    }

    

    public abstract void Interaction();

    public abstract void GetAttack(GameObject enemy);
}
