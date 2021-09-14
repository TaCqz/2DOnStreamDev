using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DamageCollisionCheck : MonoBehaviour
{

    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject prefPlayer;
    [SerializeField] private ParticleSystem anim;
    [SerializeField] private Transform spawnPoint;
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject == playerObject)
        {
            if (player.gameObject != null)
            {
                anim.Play();
                player.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX |
                                                                            RigidbodyConstraints2D.FreezePositionY;
                StartCoroutine("Respawn", 2f);
            }
            Debug.Log("Triggered!");
        }
    }
    IEnumerator Respawn(float delay)
    {
        playerObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        playerObject.transform.position = playerObject.GetComponent<PlayerController>().SpawnPoint;
        playerObject.GetComponent<Renderer>().enabled = true;
        playerObject.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None |
            RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        playerObject.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        playerObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 4f;

    }
    
}
