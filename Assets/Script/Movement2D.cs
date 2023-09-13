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
        if(moveDirection.x < transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void MoveTo(Vector3 directtion) 
    {
        //�������� �̵��� ��ġ�� �־���
        moveDirection = directtion;
    }
}
