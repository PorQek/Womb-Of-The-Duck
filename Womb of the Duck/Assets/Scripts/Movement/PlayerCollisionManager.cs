using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    PlayerMovement player;
    RaycastHit2D hit;
    [SerializeField] LayerMask obstacleMask;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    void OnCollisionEnter2D(Collision2D info)
    {
        if(info.collider.tag == "Spikes")
        {
            switch (player.movingDir)
            {
                case PlayerMovement.Direction.North:
                    Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 0.6f, obstacleMask);
                    break;
                case PlayerMovement.Direction.South:
                    Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.6f, obstacleMask);
                    break;
                case PlayerMovement.Direction.East:
                    Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 0.6f, obstacleMask);
                    break;
                case PlayerMovement.Direction.West:
                    Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.6f, obstacleMask);
                    break;               
            }
        }
    }
}
