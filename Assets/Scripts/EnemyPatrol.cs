using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1;
    public BoxCollider2D bc;
    public float distanceDetection = 0.5f;
    public LayerMask obstaclesLayer;

    public bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }


    private void Update()
    {
        isFacingRight = transform.right.normalized.x > 0;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(
            speed * transform.right.normalized.x,
            rb.velocity.y
        );

        if (HasCollisionWithObstacle())
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    public bool HasNotTouchedGround()
    {
        Vector2 centerPosition = new Vector2(
            bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x / 2)),
            bc.bounds.min.y
        );
        Collider2D hit = Physics2D.OverlapCircle(
            centerPosition,
            0.1f,
            obstaclesLayer
        );
        return hit.transform == null;
    }

    public bool HasCollisionWithObstacle()
    {
        RaycastHit2D hit = Physics2D.Linecast(
            bc.bounds.center,
            bc.bounds.center + new Vector3(distanceDetection * transform.right.normalized.x, 0, 0),
            obstaclesLayer
        );

        if(hit) {
            return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        if (bc != null)
        {
            // transform.right.normalized.x
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                bc.bounds.center,
                bc.bounds.center + new Vector3(distanceDetection * transform.right.normalized.x, 0, 0)
            );

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(
                new Vector2(
                    bc.bounds.center.x + (transform.right.normalized.x * (bc.bounds.size.x / 2)),
                    bc.bounds.min.y
                ),
                0.1f
            );
        }
    }

}
