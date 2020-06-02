using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mapCreater : MonoBehaviour
{
    public GameObject map;
    public GameObject[] maps;
    public int rows;
    public int cols;
    public float width;                     //格子宽度
    private const float rates = 1.73205f;
    public bool is_align_center=true;       //是否居中
    public Vector3 top_left_coordinate;     //左上角坐标，居中的话自己计算不需要提供
    // Start is called before the first frame update
    void Start()
    {
        maps = new GameObject[rows*cols];
        map = Resources.Load("Maps") as GameObject;


        if (is_align_center)
        { 
            top_left_coordinate.x = -width * (cols % 2 == 0 ? (cols / 2-0.5f) : (cols / 2 ));
            top_left_coordinate.y = (rates/2)*width * (rows % 2 == 0 ? (rows / 2 - 0.5f) : (rows / 2 ));
        }
        
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                if(i%2==0)
                    maps[i*j+j]=Instantiate(map, new Vector3(top_left_coordinate.x+j*width, top_left_coordinate.y - i * width * rates / 2, 0), Quaternion.identity);
                else
                    maps[i * j + j] = Instantiate(map, new Vector3(top_left_coordinate.x+ j * width+0.5f , top_left_coordinate.y - i * width * rates / 2, 0), Quaternion.identity);
            }
        }
        maps[6*6].transform.gameObject.GetComponent<red_command>().enabled=true;
        maps[3*3+3].transform.gameObject.GetComponent<green_command>().enabled=true;
        maps[20].transform.gameObject.GetComponent<yellow_command>().enabled = true;
        //maps[0] = Instantiate(map);
        //maps[1] = Instantiate(map, new Vector3( -0.5F, 0, 0), Quaternion.identity);
        //maps[2] = Instantiate(map, new Vector3(0.5F, 0, 0), Quaternion.identity);
        //maps[3] = Instantiate(map, new Vector3(1.0F, 0, 0), Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
