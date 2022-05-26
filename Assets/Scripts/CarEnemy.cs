using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody enemyRb;
    private Rigidbody playerRb;
    public float enemySpeed;
    public bool alive;
    public ParticleSystem fireParticle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRb = GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();
        alive = true;
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Mientras este "alive" seguira siempre al player.
        if (alive)
        {
            transform.LookAt(player.transform.position);
            // transform.rotation = Quaternion.LookRotation(player.transform.position);
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(playerDirection * enemySpeed * Time.deltaTime);
        }
        else
        {
            fireParticle.Play();
        }
        
        if (transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }

    
}
