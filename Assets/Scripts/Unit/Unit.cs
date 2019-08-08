using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    private float _health;

    
    public float health
    {
        get { return _health; }
        set {
            _health = Mathf.Clamp(value, 0, 100);
        }
    }

    protected Vector3 faceDirection;

    public Vector3 FaceDirection
    {
        get { return faceDirection; }
    }

    protected void Die()
    {
        Destroy(this.gameObject);
    }

    public float moveSpeed;

    public void Move(Vector3 direction)
    {
        gameObject.transform.Translate(moveSpeed * direction);
    }

    

    public abstract void Interaction();

    public abstract void GetAttack(GameObject enemy);
}
