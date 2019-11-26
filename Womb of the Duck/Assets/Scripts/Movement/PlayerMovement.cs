using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public enum Direction
    {
        North, South, East, West
    }
    public float speed;
    Rigidbody2D rb;
    public Direction movingDir;
    [SerializeField] bool movingHorizantally = false, canMove = false;
    [SerializeField] LayerMask obstacleMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        CheckRaycastWithInput(KeyCode.A, Vector2.right);
        CheckRaycastWithInput(KeyCode.W, Vector2.down);
        CheckRaycastWithInput(KeyCode.S, Vector2.up);
        CheckRaycastWithInput(KeyCode.D, Vector2.left);

        
        /*        if (movingHorizantally)
                {
                    if (CheckRayCast(Vector2.left) || CheckRayCast(Vector2.right))
                    {
                        canMove = true;
                    }
                    else
                    {
                        canMove = false;
                    }
                }
                else
                {
                    if (CheckRayCast(Vector2.up) || CheckRayCast(Vector2.down))
                    {
                        canMove = true;
                    }
                    else
                    {
                        canMove = false;
                    }
                }*/

        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
               // rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                movingHorizantally = true;

                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    movingDir = Direction.East;
                }
                else
                {
                    movingDir = Direction.West;
                }
            }
            else if (Input.GetAxisRaw("Vertical") != 0)
            {
              //  rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                movingHorizantally = false;

                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    movingDir = Direction.North;
                }
                else
                {
                    movingDir = Direction.South;
                }
            }
        }
    }

    private void CheckRaycastWithInput(KeyCode key, Vector2 direction)
    {
        if (Input.GetKeyDown(key))
        {
            if (CheckRayCast(direction))
                canMove = true;
            else canMove = false;
        }
    }

    private RaycastHit2D CheckRayCast(Vector2 direction)
    {
        Debug.Log("Shooting Raycst");
        return Physics2D.Raycast(transform.position, transform.TransformDirection(direction), 99f, obstacleMask);
    }

    void FixedUpdate()
    {
        switch(movingDir)
        {
            case Direction.North:
                rb.velocity = new Vector2(0, speed * Time.fixedDeltaTime);
                break;
            case Direction.South:
                rb.velocity = new Vector2(0, -speed * Time.fixedDeltaTime);
                break;
            case Direction.East:
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime, 0);
                break;
            case Direction.West:
                rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, 0);
                break;
        }
    }
}
