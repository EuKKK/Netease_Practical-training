using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moves : MonoBehaviour
{
    //新增 -- by lee
    public static bool moveLeft = false;
    private float left;
    private float left_higher;

    public static bool first = false;

    public static bool flag = false;
    public static int count = 0;
    public float moveDistance;
    public float speed;

    private void Start()
    {
        moveDistance = 170.0f;
        //moveDistance = 0.0f;
        speed = 400.0f;
        Vector3 p = gameObject.GetComponent<Transform>().position;
        //新增 -- by lee
        left_higher = p.x;
        left = left_higher - moveDistance;
    }
    // Update is called once per frame
    void Update()
    {   
        Vector3 p = gameObject.GetComponent<Transform>().position;
        if (moveLeft)
        {
            moveDistance = 170.0f * count;
            left = left_higher - moveDistance;
            if (p.x > left)
            {
                p.x -= Time.deltaTime * speed;
                p.x = max(p.x, left);
                gameObject.GetComponent<Transform>().position = p;
            }
        }
        else
        {
            speed = 600.0f;
            left = left_higher;
            if (p.x > left)
            {
                p.x -= Time.deltaTime * speed;
                p.x = max(p.x, left);
                gameObject.GetComponent<Transform>().position = p;
            }
        }

        //新增 --by lee
    }
    float max(float a, float b)
    {
        return a > b ? a : b;
    }
    float min(float a, float b)
    {
        return a < b ? a : b;
    }
}
