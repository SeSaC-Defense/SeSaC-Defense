using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField] private float      moveSpeed        = 0.0f; //유닛 이동속도
    [SerializeField] private Vector3    moveDirection    = Vector3.zero;

    public float MoveSpeed => moveSpeed;

    private void Update()
    {
        //오브젝트의 포지션을 moveDirection으로 moveSpeed의 속도로 이동하게
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 directtion) 
    {
        //변수값을 이동할 위치에 넣어줌
        moveDirection = directtion;
        print(directtion);
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
