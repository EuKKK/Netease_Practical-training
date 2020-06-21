using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    6.15
    维护游戏速度的脚本，所有与时间相关的操作都需要乘上这个系数
    目前受控的是攻击模块和资源生成模块
 */
public class SpeedControllor : MonoBehaviour
{
    public static float gameSpeed;
    public float nomalSpeed;
    public float fastForwardSpeed;

    public void Start()
    {
        gameSpeed = nomalSpeed;
    }
    public void OnPauseSelected(bool isOn)
    {
        if (isOn)
        {
            gameSpeed = 0.0f;
        }
    }
    public void OnPlaySelected(bool isOn)
    {
        if (isOn)
        {
            gameSpeed = nomalSpeed;
        }
    }
    public void OnFastForwardSelected(bool isOn)
    {
        if (isOn)
        {
            gameSpeed = fastForwardSpeed;
        }
    }
}
