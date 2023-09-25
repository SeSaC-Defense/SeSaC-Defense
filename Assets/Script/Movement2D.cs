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
        //변수값을 이동할 위치에 넣어줌
        moveDirection = directtion;
        if (moveDirection.x < 0) //이동할 방향이 좌측이라면 좌측을 바라보고
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (moveDirection.x > 0) //이동할 방향이 우측이라면 우측을 바라보게
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
