using Unity.Netcode;
using UnityEngine;

public class Movement2D : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed => moveSpeed;

    public void Setup(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 directtion) 
    {
        //�������� �̵��� ��ġ�� �־���
        moveDirection = directtion;
        if (moveDirection.x < 0) //�̵��� ������ �����̶�� ������ �ٶ󺸰�
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (moveDirection.x > 0) //�̵��� ������ �����̶�� ������ �ٶ󺸰�
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
