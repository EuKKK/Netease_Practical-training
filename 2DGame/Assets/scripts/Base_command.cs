using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_command : MonoBehaviour
{
    public float HP=50f;

    private const int WHITE=0;
    private const int GREEN=1;
    private const int RED=2;
    private const int BLUE=3;
    private const int YELLOW=4;
    
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
        col=Physics2D.OverlapCircleAll(this.transform.position, 0.5f);
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
        }
    }
}
