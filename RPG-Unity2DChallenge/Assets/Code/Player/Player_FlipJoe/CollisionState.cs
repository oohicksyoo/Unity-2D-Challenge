using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionState : MonoBehaviour
{
    private Collider2D col;
    private bool collided;

    [Header("Ground Checker Settings")]
    [SerializeField]
    private float groundCheckerRadius;
    [SerializeField]
    private Vector3 groundCheckerOffset;
    [SerializeField]
    private LayerMask groundLayer;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckGround()
    {
        var grounds = Physics2D.CircleCastAll(transform.position + groundCheckerOffset, groundCheckerRadius, Vector2.up, float.MaxValue, groundLayer);
        return grounds.Any();
    }

    public bool Collided
    {
        get { return collided; }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        collided = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        collided = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + groundCheckerOffset, groundCheckerRadius);
    }
}
