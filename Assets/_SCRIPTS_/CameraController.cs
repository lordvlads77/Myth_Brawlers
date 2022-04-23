using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    
    [Header("Rotation Stuff")]
    private const float _rotationLimitX = 80.0f;
    private float rotationX = default;
    
    [Header("Mouse Sensivity")]
    public float _mouseSenseX;

    [Header("Referencia")] 
    public Transform cuerpoTransform;

    private void Awake()
    {
        Instance = this;
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSenseX * Time.deltaTime;
        // Up and Down Rotation
        rotationX = Mathf.Clamp(rotationX, -_rotationLimitX, _rotationLimitX);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        
        cuerpoTransform.Rotate(Vector3.up * mouseX);
    }
}
