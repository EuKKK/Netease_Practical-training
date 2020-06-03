using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_command : MonoBehaviour
{
    MapsBattery battery;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //battery = this.gameObject.GetComponent<MapsBattery>();
    }
    public float getAttackValue(GameObject gameObject)
    {
        battery = this.gameObject.GetComponent<MapsBattery>();
        //Debug.LogWarning(battery.getBatteryData().cureVal);
        return battery.getBatteryData().cureVal;
    }
    public void turnBlueEffects()
    {
        battery = this.gameObject.GetComponent<MapsBattery>();
        CircleCollider2D cc = this.gameObject.GetComponent<CircleCollider2D>();
        cc.radius = battery.getBatteryData().cureDistance;
    }
}
