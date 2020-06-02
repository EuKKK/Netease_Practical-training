using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class green_command : MonoBehaviour
{
    public Color color;
    public static int numbersGreen; //绿格数量--决定资源增长速度
    //MapsBattery batteryOnMaps;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        color = new Color(0.0f, 1.0f, 0.0f, 1.0f);

        sprite.color = color;
        this.gameObject.GetComponent<Base_command>().status = 1;
        //this.gameObject.GetComponent<Base_command>().HP = 100;

        //判断炮台是否存在
        MapsBattery battery = this.gameObject.GetComponent<MapsBattery>();
        if(battery.BatteryOnMaps != null)
        {
            this.gameObject.GetComponent<Base_command>().status = 3;
            //this.gameObject.GetComponent<Base_command>().HP = 200;
        }

    }

}
