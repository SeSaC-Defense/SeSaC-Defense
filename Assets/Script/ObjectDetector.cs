using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : Singleton<ObjectDetector>
{
    [SerializeField]
    private ButtonGroupToggle buttonGroupToggle;
    
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;

        if (CanHit(out hit))
        {
            buttonGroupToggle.HitTransform = hit.transform;

            switch ((hit.transform.tag, UIStateEventHandler.Instance.CurrentState))
            {
                case ("Tile", UIStateType.ConstructionChecking):
                    UIStateEventHandler.Instance.ChangeState(UIStateType.ConstructionConfirming);
                    break;
                case ("Tower", UIStateType.None):
                    UIStateEventHandler.Instance.ChangeState(UIStateType.TowerPressed);
                    break;
                case ("Barrack", UIStateType.None):
                    UIStateEventHandler.Instance.ChangeState(UIStateType.BarrackPressedOnWaiting);
                    /*이부분에 hit한 오브젝트 정보를 가져가서 ui에게 정보를 보내줘야합니다
                     *저는 BarrackDataviewer.cs를 가진 오브젝트를 따로 만들어서 정보 처리를 하였으나
                     *제가 함부로 건드리면 UI 설계가 복잡해질것 같아 남겨드리겠습니다.
                     *
                     *private Transform hit;                         1. 정보를 저장한 변수
                     *
                     *public void SelectedBuilding(Transform hit)    2. hit한 배럭의 정보를 받아오는 함수 
                     *{
                     *  this.hit = hit;
                     *}
                     *  public void ChoiceUnit(int n)                3. 버튼에서 int값을 불러오는 함수
                     *{
                     *  hit.GetComponent<UnitSpawner>().UnitChoice(n);  4. 받은 int값을 hit한 배럭에 넣어주는 형식
                    */
                    break;
                default:
                    UIStateEventHandler.Instance.ChangeState(UIStateType.None);
                    break;
            }
        }
    }

    private bool CanHit(out RaycastHit hit)
    {
        hit = new RaycastHit();

        if (!Input.GetMouseButtonDown(0))
            return false;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity);
    }
}