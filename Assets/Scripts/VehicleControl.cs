using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VehicleControl : MonoBehaviour
{
    private Rigidbody playerRB;
    public GameObject cañon;
    private float vertical;
    private float horizontal;
    public float speed = 20;
    public float rotate = 50;
    public float rotateCañon = 100;
    public GameObject puntaCañon;
    public GameObject[] wheels;
    public GameObject ball;
    public bool reloading = false;
    private bool volcado = false;
    public int livePoints;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI reloadingText;
    public ParticleSystem explosion;
    public ParticleSystem fire;
    private AudioSource playerAudio;
    public AudioClip shooting;
    public AudioClip crashing;
    public AudioClip explode;

   
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        livesText.text = livePoints.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if (volcado == false)
        {
            transform.Translate(Vector3.forward * vertical * speed* Time.deltaTime);
            //playerRB.AddRelativeForce(Vector3.forward * vertical * fSpeed * Time.deltaTime);
        }
        

        // Esto evita que el vehiculo gire sobre su propio eje mientras esta parado.
        if (vertical != 0 )
        {
            transform.Rotate(Vector3.up * horizontal * rotate * Time.deltaTime);
            foreach (GameObject wheel in wheels)
            {
                wheel.transform.Rotate(Vector3.right * vertical * 100 * Time.deltaTime);
            }
        }
        
        RotateCañon();

        if (Input.GetKeyDown(KeyCode.Space) && reloading == false)
        {
            reloading= true;
            Disparar();

            StartCoroutine(ReloadingAnmo());

        }
        // AAAH IMPORTANTE: En unity el valor del euler es positivo siempre, es decir, va de 0 a 359 en sentido contra horario al parecer.
        // Es por eso que no podia poner que si z < -90 grados o algo asi porque nunca se cumplia.
        float zRot = transform.eulerAngles.z;

        // el indicador volcado debe estar falso para que no se ejecute la corutina millones de veces xD
        if(zRot > 60 & zRot <= 180 & volcado == false | zRot < 300 & zRot >= 180 & volcado == false)
         {
            StartCoroutine(ReturnRotation());  
         }

         OffOutBounds();
    }

    private void RotateCañon()
    {
        if (Input.GetKey(KeyCode.E))
        {
            cañon.transform.Rotate(Vector3.up * rotateCañon * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            cañon.transform.Rotate(Vector3.up * -rotateCañon * Time.deltaTime);
        }
    }

    private void Disparar()
    {
        // PENDIENTE: encontrar la posicion ideal para disparar la bala xD
        Vector3 disparoPos = puntaCañon.transform.position;
        Instantiate(ball, disparoPos, cañon.transform.rotation);
        Debug.Log("Recargando...");
        reloadingText.text = "Recargando...";
        reloadingText.color = Color.red;
        playerAudio.PlayOneShot(shooting, 2);
        
    }

    // No se me ocurrio otra cosa por ahora, cuando el enemigo choca con el player
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 direccionChoque = (other.transform.position - transform.position).normalized;
            Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();
            otherRb.AddForce(direccionChoque * 25, ForceMode.Impulse);
            lessLives();
            playerAudio.PlayOneShot(crashing);
        }
    }


    // Funcion que despues de 4 segundos si esta el tanque volcado, lo regresa a su posicion.
    // Apenas inicia la rutina, el indicador volcado se activa, cuando se termina, se desactiva.
    IEnumerator ReturnRotation()
    {
        
        volcado = true;
        yield return new WaitForSeconds(4);
        Debug.Log("Volteando...");
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        volcado = false;
         
    }

    // Funcion para cooldown al disparar. PENDIENTE: poner un indicador en pantalla que avise cuando este listo.
    IEnumerator ReloadingAnmo()
    {
        yield return new WaitForSeconds(6);
        reloading = false;
        Debug.Log("Listo...");
        reloadingText.text = "Listo...!";
        reloadingText.color = Color.green;
    
    }

    public void lessLives()
    {
        livePoints -= 1;
        livesText.text = livePoints.ToString();
        if (livePoints == 0)
        {
           VehicleDestroyed();
        }
    }

    void VehicleDestroyed()
    {
        // Desactiva el script, no se si sea la mejor practica xd pero ya no se puede controlar al player.
        playerRB.AddForce(Vector3.up * 100, ForceMode.Impulse);
        playerRB.AddTorque(new Vector3(Random.Range(1,10),Random.Range(1,10),Random.Range(1,10)) * 500);
        // Esto hace que las funciones del script no se ejecuten despues de perder.
        GetComponent<VehicleControl>().enabled = false;
        // llamar GameOver desde el GameManager...
        FindObjectOfType<GameManager>().GameOver();
        // To do... Añadir una explosion y ek fuego.
        explosion.Play();
        fire.Play();
        playerAudio.PlayOneShot(explode, 5);
    }

    public void OffOutBounds()
    {
        if (transform.position.x > 450)
        {
            transform.position = new Vector3(450, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -450)
        {
            transform.position = new Vector3(-450, transform.position.y, transform.position.z);
        }

        if (transform.position.z > 450)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 450);
        }

        if ( transform.position.z < -450)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -450);
        }
    }

}
