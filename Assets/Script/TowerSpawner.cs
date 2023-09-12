using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject magition1;
    [SerializeField] private GameObject magition2;
    [SerializeField] private GameObject magition3;
    [SerializeField] private GameObject magition4;
    [SerializeField] private GameObject barrack;

    private bool magition1IsBuild = false;
    private bool magition2IsBuild = false;
    private bool magition3IsBuild = false;
    private bool magition4IsBuild = false;
    private bool barrackIsBuild   = false;
   
    public void ReadyToMagition1()
    {
        print("magition1 true");
        magition1IsBuild = true; //�Ǽ� ��ư on/off �Լ�
    }

    public void ReadyToMagition2()
    {
        print("magition2 true");
        magition2IsBuild = true;
    }

    public void ReadyToMagition3()
    {
        print("magition3 true");
        magition3IsBuild = true;
    }

    public void ReadyToMagition4()
    {
        print("magition4 true");
        magition4IsBuild = true;
    }

    public void ReadyToBarrack()
    {
        print("barrack true");
        barrackIsBuild = true;
    }

    public void SpawnTower(Transform tileTransform) //Ŭ���� tile
    {
        Tile tile = tileTransform.GetComponent<Tile>(); //����tile�� tile��ũ��Ʈ�� ����
        //�̹� ������ Ÿ���� �ִٸ� ��ư�Լ� false��
        //�׸��� ������ Ÿ���� ���ٸ� ���� ��ư�� bool������ Ȯ���ؼ� Ÿ�� �Ǽ�
        if (tile.OnTheTower == true) //�̹� ������ Ÿ���� �ִٸ� ��ư�Լ� false��
        {
            magition1IsBuild = false;
            magition1IsBuild = false;
            magition3IsBuild = false;
            magition3IsBuild = false;
            magition4IsBuild = false;
            barrackIsBuild   = false;
        }
        else if(magition1IsBuild == true)
        {
            tile.OnTheTower = true;
            Instantiate(magition1, tileTransform);
            magition1IsBuild = false;
        }
        else if(magition2IsBuild == true)
        {
            tile.OnTheTower = true;
            Instantiate(magition2, tileTransform);
            magition2IsBuild = false;
        }
        else if(magition3IsBuild == true)
        {
            tile.OnTheTower = true;
            Instantiate(magition3, tileTransform);
            magition3IsBuild = false;
        }
        else if(magition4IsBuild == true) 
        {
            tile.OnTheTower = true;
            Instantiate(magition4, tileTransform);
            magition4IsBuild = false;
        }
        else if(barrackIsBuild == true)
        {
            tile.OnTheTower = true;
            Instantiate(barrack, tileTransform);
            barrackIsBuild = false;
        }
    }

}
