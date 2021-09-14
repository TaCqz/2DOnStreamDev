using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwap : MonoBehaviour
{

    [SerializeField] private GameObject playerObject;
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject == playerObject)
        {
            playerObject.GetComponent<Rigidbody2D>().gravityScale *= -1f;
            Debug.Log("Triggered Gravity!");
        }
    }
}
