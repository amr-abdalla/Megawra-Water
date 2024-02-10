using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientSpriteWithBodyVelocity : MonoBehaviour
{
    [SerializeField]
    private PhysicsBody2D body = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private bool flipX = false;

    void Start()
    {
        if (body == null)
            body = GetComponent<PhysicsBody2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body == null || spriteRenderer == null)
            return;

        if (body.VelocityX > Physics2DConstants.EPSILON_VELOCITY)
        {
            spriteRenderer.flipX = flipX;
        }
        else if (body.VelocityX < -Physics2DConstants.EPSILON_VELOCITY)
        {
            spriteRenderer.flipX = !flipX;
        }
    }
}
