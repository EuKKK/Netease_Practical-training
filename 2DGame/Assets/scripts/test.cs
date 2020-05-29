using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Color color;
    private float r;
    private float b;
    private float g;
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        r = 1.0f;
        b = 0.5f;
        g = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        SpriteRenderer sprite = collider.gameObject.GetComponent<SpriteRenderer>();
        color.r = Mathf.Sin(r * Time.time);
        color.g = Mathf.Sin(2*g * Time.time);
        color.b = Mathf.Sin(b * Time.time);
        sprite.color = color;
    }
}
