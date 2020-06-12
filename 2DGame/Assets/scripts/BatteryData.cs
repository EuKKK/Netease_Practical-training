using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BatteryData
{
    public GameObject batteryPrefab;
    public int cost;
    public BatteryType type;
    public float cureDistance;
    public float cureVal;
    public float totalHP;
    public float HP; //当前血量
}

public enum BatteryType
{
    Hospital,
    PowerStation,
    MaskFactory
}