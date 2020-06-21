using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_command : MonoBehaviour
{
    public Color color ;
    public float redAttackSpeed = -10f;
    public int attackDistance = 1;
    public GameObject red_effect;
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(1.0f, 0.67843f, 0.72549f, 1.0f);
    }

    public float getAttackValue(GameObject gameObject)
    {
        //Debug.LogWarning(redAttrackSpeed);
        return redAttackSpeed;
    }
    public void turnRedEffects()
    {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = color;
        Vector3 p = red_effect.GetComponent<Transform>().position;
        p.z = -9;
        red_effect.GetComponent<Transform>().position=p;

        //CircleCollider2D cc = this.gameObject.GetComponent<CircleCollider2D>();
        //cc.radius = attackDistance;
    }
    public void ReSetRedEffects()
    {
        Vector3 p = red_effect.GetComponent<Transform>().position;
        p.z = -50;
        red_effect.GetComponent<Transform>().position = p;
    }
    public int GetAttackDistance() => attackDistance;
}
