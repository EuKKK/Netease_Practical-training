using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_command : MonoBehaviour
{
    public Color color ;
    public float redAttackSpeed = -10f;
    public int attackDistance = 1;

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
        //CircleCollider2D cc = this.gameObject.GetComponent<CircleCollider2D>();
        //cc.radius = attackDistance;
    }
    public int GetAttackDistance() => attackDistance;
}
