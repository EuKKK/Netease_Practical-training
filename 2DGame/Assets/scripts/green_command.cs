using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class green_command : MonoBehaviour
{
    public Color color ;
    public static int numbersGreen; //绿格数量--决定资源增长速度
    //MapsBattery batteryOnMaps;
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(0.46275f, 0.87059f, 0.50589f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void turnGreenEffects()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

}
