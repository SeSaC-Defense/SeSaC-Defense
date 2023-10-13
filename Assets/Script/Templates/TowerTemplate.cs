using System;
using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public TowerData[] unitData;
    public float cost;

    [Serializable]
    public struct TowerData
    {
        public Sprite sprite;
    }
}
