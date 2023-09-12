using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;
    private int damage;

    public void SetUp(Transform target, int damage)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        if(target != null)
        {
            Vector3 diretion = (target.position - transform.position).normalized;
            movement2D.MoveTo(diretion);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("EUnit")) return;
        if (collider.transform != target) return;

        //데미지주는 함수 넣기
    }
}
