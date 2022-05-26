using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 100;
    public float pushForce = 1000;
    private Rigidbody playerRB;
    public GameObject bus;
    private float yBus;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        playerRB.AddForce(Vector3.forward * speed * vInput * Time.deltaTime, ForceMode.Acceleration); 
        playerRB.AddForce(Vector3.right * speed * hInput * Time.deltaTime, ForceMode.Acceleration);

        OutOfBorders();

        /* Se me ocurrio esto, que aumenta un valor de forma constante
        luego ese valor seria positivo o negativo gracias al axis
        el valor se resetea cuando el axis es cero, cuando se deja de presionar
        un input */
        yBus += 100 * Time.deltaTime;
        float y = yBus * hInput;
        if (Input.GetAxis("Horizontal") == 0)
        {
            yBus = 0;
        }

        Quaternion busRotate = Quaternion.Euler(0, y, 0);
        bus.transform.rotation = busRotate;
        
    }

    void OnCollisionEnter(Collision other)
    {
        
        Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Choque");
            Vector3 enemyVector = other.transform.position;
            // Recordatorio: Vector A - B = vector B to A
            Vector3 awayFromPlayer = (enemyVector - transform.position).normalized;
            // Al chocar contra un objeto, lo empuja
            enemyRB.AddForce(awayFromPlayer * pushForce * Time.deltaTime, ForceMode.Impulse);
        }
    }

    void OutOfBorders()
    {
        if (transform.position.z > 9)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -9); 
        }

        if (transform.position.z < -9)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 9); 
        }

        if (transform.position.x < -15)
        {
            transform.position = new Vector3(15, transform.position.y, transform.position.z); 
        }

        if (transform.position.x > 15)
        {
            transform.position = new Vector3(-15, transform.position.y, transform.position.z); 
        }
    }
}
