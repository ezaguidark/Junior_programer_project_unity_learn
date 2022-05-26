using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GameObject player;
    private Rigidbody enemyRB;
    public float speed = 200;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVector = player.transform.position;
        // Recordatorio: vector A - B =  Vector B to A
        Vector3 followPlayer = (playerVector - transform.position).normalized;

        enemyRB.AddForce(followPlayer * speed * Time.deltaTime);

        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }

    }
}
