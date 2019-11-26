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
    [SerializeField] bool movingHorizantally = false, canCheck = false;
    [SerializeField] LayerMask obstacleMask;

    public AudioClip QuackSound;
    public AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        if (movingHorizantally)
        {
            if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.6f, obstacleMask) || Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 0.6f, obstacleMask))
            {
                canCheck = true;
            }
            else
            {
                canCheck = false;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 10f, Color.yellow);
        }   
        else
        {
            if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 0.6f, obstacleMask) || Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.6f, obstacleMask))
            {
                canCheck = true;
            }
            else
            {
                canCheck = false;
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 10f, Color.yellow);
        }

        if (canCheck)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                movingHorizantally = true;

                if (Input.GetKeyDown(KeyCode.D))
                {
                    PlayQuack();
                    movingDir = Direction.East;
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    PlayQuack();
                    movingDir = Direction.West;
                }
            }
            else if (Input.GetAxisRaw("Vertical") != 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                movingHorizantally = false;

                if (Input.GetKeyDown(KeyCode.W))
                {
                    PlayQuack();
                    movingDir = Direction.North;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    PlayQuack();
                    movingDir = Direction.South;
                }
            }
        }
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

    void PlayQuack()
    {
        audioSource.Play();
    }
}
