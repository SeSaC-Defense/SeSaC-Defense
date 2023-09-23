using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    private UnitData unitData;
    private int playerNumber = 1;
    public int PlayerNumber => playerNumber;

    private float moveSpeed = 10; //���� �̵��ӵ�
    public float MoveSpeed => moveSpeed;
    private void Start()
    {
        moveSpeed = unitData.UnitSpeed;
    }
    private void Update()
    {
        //������Ʈ�� �������� moveDirection���� moveSpeed�� �ӵ��� �̵��ϰ�
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void DataSetting(UnitData unitData)
    {
        this.unitData = unitData;
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
