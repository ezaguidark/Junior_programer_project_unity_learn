using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody ballRB;
    private VehicleControl playerScript;
    private GameManager gmScript;
    private Rigidbody playerRB;
    private AudioSource fxSounds;
    public AudioClip explosion;
    public float force = 50;
    public float retrocesoForce;
    // Start is called before the first frame update
    void Start()
    {
        fxSounds = GetComponent<AudioSource>();
        // PENDIENTE
        /* Toma como referencia el cañon del tanquesito, su rotacion actual,
        es por eso que debo agarrar la informacion del script. falta modificar las propiedades
        fisicas de la bala, mas peso, velocidad, etc*/
        playerScript = GameObject.FindObjectOfType<VehicleControl>();
        gmScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRB = playerScript.GetComponent<Rigidbody>();

        // Añade como retroceso al tanque cuando dispara.
        Vector3 direccionForce = (playerScript.transform.position - transform.position).normalized;
        playerRB.AddForce(direccionForce * retrocesoForce, ForceMode.Impulse);

        // Añade fuerza al proyectil, hacia adelante.
        ballRB = GetComponent<Rigidbody>();
        ballRB.AddForce(playerScript.cañon.transform.forward *  force, ForceMode.Impulse);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Nota: El collider de la bala es mas grande de lo normal en unity para que golpee los carros mejor.
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Obtiene el rigigbody y la variable del script para cambiarla.
            Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();
            other.gameObject.GetComponent<CarEnemy>().alive = false;
            // Lanza por los aires al vehiculo y se destruye despues.
            otherRb.AddForce(Vector3.up * 20, ForceMode.Impulse);
            Destroy(other.gameObject, 15);
            fxSounds.PlayOneShot(explosion, 2);

            gmScript.AddScore();
        }
    }
}
