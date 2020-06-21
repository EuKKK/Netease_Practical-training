using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_command : MonoBehaviour
{
    MapsBattery battery;

    public float getAttackValue(GameObject gameObject)
    {
        battery = this.gameObject.GetComponent<MapsBattery>();
        //Debug.LogWarning(battery.getBatteryData().cureVal);
        return battery.getBatteryData().cureVal;
    }
    public void turnBlueEffects()
    {
        battery = this.gameObject.GetComponent<MapsBattery>();
        //改变碰撞体范围
        //CircleCollider2D cc = this.gameObject.GetComponent<CircleCollider2D>();
        //cc.radius = battery.getBatteryData().cureDistance;
    }
    public int GetAttackDistance() => battery.getBatteryData().cureDistance;
}
