using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int xpValue = 1;

    //Logic
    public float triggerLength = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer = false;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Is the player in range?
        //if its in chase distance (initially for 5 meters)
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            // if its <= 1 meter then start to chase.
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;


            if (chasing)
            {
                // if it can chase and not colliding with player yet, then move to position.
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            // else go back to where we were
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for ovelaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }
    }

    protected override void Death()
    {     
        Destroy(gameObject);
        GameManager.instance.exp += xpValue;
        GameManager.instance.ShowText("+" + xpValue.ToString() + " xp", 22, Color.magenta, transform.position, Vector3.up * 40, 1.0f);

    }

}
