using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit/Create Unit", order = 0)]
public class UnitData : ScriptableObject
{
    public string UnitName;
    public float UnitSpeed;
    public float UnitHP;
    public Sprite Sprite;
}