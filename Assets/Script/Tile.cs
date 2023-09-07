using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool OnTheTower
    {
        set; get;
    }

    private void Awake()
    {
        OnTheTower = false;
    }
}