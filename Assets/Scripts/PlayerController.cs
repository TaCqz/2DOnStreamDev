using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpPower = 13f;

    [SerializeField] private Vector2 _spawnPoint;

    private int dashes = 1;
    private float dashTime;
    private float startDashTime;
    private float dashSpeed = 1;
    [SerializeField] private float dashLength;
    private float dashCooldown = 1.5f;
    private float timeStamp;

    public Vector2 SpawnPoint
    {
        get => _spawnPoint;
        set => _spawnPoint = value;
    }

    public LayerMask groundLayer;
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    private float horizontalMove;
    
    #endregion

    // Update is called once per frame
    void FixedUpdate()
    {
        // horizontalMove is between -1 and 1 depending on direction
        horizontalMove = Input.GetAxis("Horizontal");

        
        // Check if player wants to dash and start coroutine
        if (dashes > 0 && Input.GetKey(KeyCode.E) && timeStamp <= Time.time)
        {
            StartCoroutine("Dash", dashLength);
        }
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        
        
        

        if (Input.GetAxis("Jump") > 0 && GroundCheck())
        {
            if (rb.gravityScale > 0) rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            else if (rb.gravityScale < 0) rb.velocity = new Vector2(rb.velocity.x, jumpPower * -1f);
        }

        if (GroundCheck()) dashes = 1;
    }

    void Update()
    {
        
        #region particlesystems
        /* ~~~~~~~~~~~ TODO ~~~~~~~~~~
         
         Rework Particle Emission on Movement OR add Particle System on Dash
        
        
        // How particle systems in unity work based on directional movement
        */
        /*var leftEmission = leftTrail.emission;
        var rightEmission = rightTrail.emission;
        if (horizontalMove > 0 && rb.velocity.x > 3 && GroundCheck())
        {
            leftEmission.enabled = true;
        }
        else
        {
            leftEmission.enabled = false;
        }


        if (horizontalMove < 0 && rb.velocity.x < -3 && GroundCheck())
        {
            rightEmission.enabled = true;
        }
        else
        {
            rightEmission.enabled = false;
        }*/
        
        #endregion
    }


    // Checks if the PlayerObject is on the Ground. To avoid jumping on walls it creates a BoxCast with an x slightly smaller than the total size of the BoxCollider
    private bool GroundCheck()
    {
        float extra = 0.1f;

        RaycastHit2D hit;
        // Creation of BoxCast
        if (rb.gravityScale > 0)
        {
            hit = Physics2D.BoxCast(bc.bounds.center,
                new Vector3(bc.bounds.size.x - 0.187f, bc.bounds.size.y, bc.bounds.size.z), 0f, Vector2.down, extra,
                groundLayer);
        } else
        {
            hit = Physics2D.BoxCast(bc.bounds.center,
                new Vector3(bc.bounds.size.x - 0.187f, bc.bounds.size.y, bc.bounds.size.z), 0f, Vector2.up, extra,
                groundLayer);
        }

        // Checks for collision here
        if (hit.collider != null) return true;

        return false;
    }

    IEnumerator Dash(float length)
    {
        // Set speed to 24 for <length> seconds and prepare cooldown calculation
        timeStamp = Time.time + dashCooldown;
        speed = 24;
        Debug.Log("Dashing.. theoretically!");
        yield return new WaitForSeconds(length);
        // After <length> seconds return to normal speed and end coroutine, setting dashes back to 0
        speed = 6;
        dashes = 0;
        StopCoroutine("Dash");
    }
    
    
}

