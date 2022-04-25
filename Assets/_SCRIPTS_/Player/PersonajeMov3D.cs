using UnityEngine;

public class PersonajeMov3D : MonoBehaviour
{
    public float velocidad;
    public float velocidadRotacion;
    public float fuerzaSalto;

    Vector3 movimiento;
    
    [Header("Referencia")]
    public Rigidbody rigi;
    
    
    [Header("CheckGround")]
    public Vector3 checkgroundPosition;
    public bool isGround;
    public float checkGroundRatio;
    public LayerMask checkGroundMask;
    

    private void FixedUpdate()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal") * velocidad;
        movimiento.z = Input.GetAxisRaw("Vertical") * velocidad;
        movimiento = transform.TransformDirection(movimiento);

        isGround = Physics.CheckSphere(transform.position + checkgroundPosition, checkGroundRatio, checkGroundMask);
        
        movimiento.y = rigi.velocity.y;
        rigi.velocity = movimiento;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * (-velocidadRotacion * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * (velocidadRotacion * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            rigi.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
