using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField] private float      moveSpeed        = 0.0f; //���� �̵��ӵ�
    [SerializeField] private Vector3    moveDirection    = Vector3.zero;

    public float MoveSpeed => moveSpeed;

    private void Update()
    {
        //������Ʈ�� �������� moveDirection���� moveSpeed�� �ӵ��� �̵��ϰ�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 directtion) 
    {
        //�������� �̵��� ��ġ�� �־���
        moveDirection = directtion;
        print(directtion);
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
