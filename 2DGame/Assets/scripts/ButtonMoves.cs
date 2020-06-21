using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMoves : MonoBehaviour
{
    public static bool moveUp=false;

    private float upper;
    private float lower;
    public float moveDistance;
    public float speed;

    private void Start()
    {
        moveDistance = 100.0f;
        speed = 80.0f;
        Vector3 p = gameObject.GetComponent<Transform>().position;
        lower = p.y;
        upper = moveDistance + lower;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 p = gameObject.GetComponent<Transform>().position;
        if (moveUp)
        {
            if (p.y < upper)
            {
                p.y += Time.deltaTime * speed;
                p.y = min(p.y, upper);
                gameObject.GetComponent<Transform>().position = p;
            }
        }
        else
        {
            if (p.y > lower)
            {
                p.y -= Time.deltaTime * speed;
                p.y = max(p.y, lower);
                gameObject.GetComponent<Transform>().position = p;
            }
        }
        //新增 --by lee
    }
    float max(float a,float b)
    {
        return a > b ? a : b;
    }
    float min(float a,float b)
    {
        return a < b ? a : b;
    }
}
