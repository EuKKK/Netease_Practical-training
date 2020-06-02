﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************************
 * 
 * Maps 初始值为50（白块）
 * HP > 100， 转绿
 * HP < 0，转红
 * 
 * 假定阈值
 * 白色 [50,100]
 * 绿色 (100,150]
 * 红色 [-50,0)
 * ***********************************************************/
public class Base_command : MonoBehaviour
{
    public float HP=50f;

    private const int WHITE=0;
    private const int GREEN=1;
    private const int RED=2;
    private const int BLUE=3;
    private const int YELLOW=4;

    private BatteryData batData;
    
    public float speed = 10f;
    public int status;
    // Start is called before the first frame update
    void Start()
    {
        status=0;
    }

    // Update is called once per frame
    public Collider2D[] col;
    void Update()
    {
        //lrs原版
        /*col=Physics2D.OverlapCircleAll(this.transform.position, 0.5f);
        foreach(Collider2D cc in col){
            int ccstatus=cc.gameObject.GetComponent<Base_command>().status;
            if(ccstatus==2&&this.HP>0){
                this.HP-=Time.deltaTime*speed;
            }
            else if(ccstatus==1&&this.HP<100)
            {
                this.HP+=Time.deltaTime*speed;
            }
            if(HP<=0&&this.status!=2)
            {
                this.gameObject.GetComponent<red_command>().enabled=true;
                this.gameObject.GetComponent<green_command>().enabled=false;
            }
            else if(HP>=100&&this.status!=1)
            {
                this.gameObject.GetComponent<red_command>().enabled=false;
                this.gameObject.GetComponent<green_command>().enabled=true;
            }
        }*/

        //主动攻击代替被动接受  仅存在于红、蓝
        float attackdist;
        if(this.status == 2 || this.status == 3)
        {
            if (this.status == 3)
            {
                //获取炮台数据
                MapsBattery battery = this.gameObject.GetComponent<MapsBattery>();
                batData = battery.getBatteryData();
                attackdist = batData.cureDistance;
            }
            else attackdist = 0.5f;

            col = Physics2D.OverlapCircleAll(this.transform.position, attackdist);
            foreach (Collider2D cc in col)
            {
                if (this.status == 2)
                {
                    cc.gameObject.GetComponent<Base_command>().HP -= Time.deltaTime * speed;
                }
                else
                {
                    //暂定炮台治愈能力强于感染
                    cc.gameObject.GetComponent<Base_command>().HP += Time.deltaTime * speed * batData.cureVal;
                }

                float cchp = cc.gameObject.GetComponent<Base_command>().HP;
                int ccstatus = cc.gameObject.GetComponent<Base_command>().status;
                if (cchp <= 0 && ccstatus != 2)
                {
                    //若为蓝转红，摧毁上方炮台
                    if (ccstatus == 3)
                    {
                        GameObject.Destroy(cc.gameObject.GetComponent<MapsBattery>().BatteryOnMaps);
                        GreenNumber.numGreen -= 1;
                    }
                    else if(ccstatus == 1)
                    {
                        GreenNumber.numGreen -= 1;
                    }
                    cc.gameObject.GetComponent<Base_command>().HP = 0;
                    cc.gameObject.GetComponent<red_command>().enabled = true;
                    cc.gameObject.GetComponent<green_command>().enabled = false;
                }
                else if (cchp >= 100 && (ccstatus != 1&& ccstatus != 3))
                {
                    cchp = cc.gameObject.GetComponent<Base_command>().HP = 100;
                    cc.gameObject.GetComponent<red_command>().enabled = false;
                    cc.gameObject.GetComponent<green_command>().enabled = true;
                    GreenNumber.numGreen += 1;
                }
            }
        }
        
    }
}