using System;
using UnityEngine;

[CreateAssetMenu]
public class UnitTemplate : ScriptableObject
{
    public UnitPlayerSideData[] playerSideData;
    public float spawnInterval;
    public int maxHealth;
    public int guard;
    public int cost1;
    public int reward1;
    public int reward2;
    public int reward3;
    public int reward4;

    [Serializable]
    public struct UnitPlayerSideData
    {
        public Sprite sprite;
        public int direction;
    }
}
