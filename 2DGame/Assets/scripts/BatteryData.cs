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
}

public enum BatteryType
{
    Hospital,
    PowerStation,
    MaskFactory
}