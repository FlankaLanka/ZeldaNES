using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{
    public float movement_speed = 4;
    public float grid_multiplier = 0.5f;

    private Rigidbody rb;
    private Vector3 buffered_pos;
    private Vector2 prev_pos;
    private Vector2 prev_dir;
    private bool snapping;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        buffered_pos = transform.position;
        prev_pos = transform.position;
        prev_dir = Vector2.down;
        snapping = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 current_input = GetInput();
        if (current_input != Vector2.zero && current_input != prev_dir && !snapping)
        {
            snapping = true;
            if (prev_dir.x != current_input.x || prev_dir.y != current_input.y)
                buffered_pos = SnapToGrid();
            else
                buffered_pos = transform.position;
            rb.velocity = Vector3.zero;
            prev_dir = current_input;
        }
        
        
        if (snapping)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, buffered_pos, Time.deltaTime * movement_speed);
            if (transform.position == buffered_pos)
            {
                snapping = false;
            }
        }
        else
        {
            rb.velocity = current_input * movement_speed;
        }
    }

    Vector2 GetInput()
    {
        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        if(Mathf.Abs(horizontal_input) > 0.0f)
        {
            vertical_input = 0.0f;
        }

        return new Vector2(horizontal_input, vertical_input);
    }

    private Vector2 SnapToGrid()
    {
        double x_pos = transform.position.x;
        double x_dec = x_pos - Math.Truncate(x_pos);

        double y_pos = transform.position.y;
        double y_dec = y_pos - Math.Truncate(y_pos);

        if (prev_dir.x > 0)
        {
            if (x_dec < 0.05)
            {
                x_pos = Math.Truncate(x_pos);
            }
            else if (x_dec < .95)
            {
                x_pos = Math.Truncate(x_pos) + 0.5;
            }
            else
            {
                x_pos = Math.Truncate(x_pos) + 1;
            }
        }
        else if (prev_dir.x < 0)
        {
            if (x_dec < 0.45)
            {
                x_pos = Math.Truncate(x_pos);
            }
            else if (x_dec < .95)
            {
                x_pos = Math.Truncate(x_pos) + 0.5;
            }
            else
            {
                x_pos = Math.Truncate(x_pos) + 1;
            }
        }
        else if (prev_dir.y > 0)
        {
            if (y_dec < 0.05)
            {
                y_pos = Math.Truncate(y_pos);
            }
            else if (y_dec < .55)
            {
                y_pos = Math.Truncate(y_pos) + 0.5;
            }
            else
            {
                y_pos = Math.Truncate(y_pos) + 1;
            }
        }
        else if (prev_dir.y < 0)
        {
            if (y_dec < 0.45)
            {
                y_pos = Math.Truncate(y_pos);
            }
            else if (y_dec < .95)
            {
                y_pos = Math.Truncate(y_pos) + 0.5;
            }
            else
            {
                y_pos = Math.Truncate(y_pos) + 1;
            }
        }

        return new Vector2((float)(x_pos), (float)(y_pos));
    }

    private bool SeesWall(RaycastHit[] hits, string tag)
    {
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == tag)
                return true;
        }

        return false;
    }
}
