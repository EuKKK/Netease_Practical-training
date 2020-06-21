using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BatteryData
{
    public GameObject batteryPrefab;
    public int cost;
    public int costWater;
    public int costElectric;
    public BatteryType type;
    public int cureDistance;
    public float cureVal;
    public float totalHP;
    public float HP; //当前血量
    public Text textMoney;
    public Text textWater;
    public Text textElectric;
}

public enum BatteryType
{
    Hospital,
    PowerStation,
    MaskFactory
}