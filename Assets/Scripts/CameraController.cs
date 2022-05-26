using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private VehicleControl playerScript;
    public Camera mainCamera;
    public Camera cañonCamera;
    private bool cameraBool = true;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<VehicleControl>();
    }
    void Update()
    {
    
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 3, 0);
        cañonCamera.transform.rotation = playerScript.cañon.transform.rotation;
        if (Input.GetKeyDown("0"))
        {
            ChangeCamera();
        }
        float horizontal = Input.GetAxis("Mouse X");
        if (cameraBool)
        {
            transform.Rotate(Vector3.up * 50 * horizontal * Time.deltaTime);
        }
        
    }

    // No estoy usando la funcion porque decidi poner la camara como child object del player
    private void RotateCam()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * -100 * Time.deltaTime);
        }
    }

    private void CameraMode_1()
    {
        mainCamera.gameObject.SetActive(true);
        cañonCamera.gameObject.SetActive(false);
    }

    private void CameraMode_2()
    {
        mainCamera.gameObject.SetActive(false);
        cañonCamera.gameObject.SetActive(true);
    }

    private void ChangeCamera()
    {
        cameraBool = !cameraBool;
        if (cameraBool)
        {
            CameraMode_1();
        }
        else
        {
            CameraMode_2();
        }
    }
}
