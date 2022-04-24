using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Mov Velocidad y Fuerza")]
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

    [SerializeField] private Camera _playerCamera = default;

    [Header("Focus Stuff")] [SerializeField]
    private Interactable _focusingThis = default;

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal") * velocidad;
        movimiento.z = Input.GetAxisRaw("Vertical") * velocidad;
        movimiento = transform.TransformDirection(movimiento); // Transforma una direccion local en direccion del mundo.

        isGround = Physics.CheckSphere(transform.position + checkgroundPosition, checkGroundRatio, checkGroundMask);
        
        movimiento.y = rigi.velocity.y; // Permite que la gravedad siga funcionando
        rigi.velocity = movimiento; // Aplicamos
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
        if (Input.GetKey(KeyCode.Space) && isGround) // KeyDown y KeyUp no funcionan correctamente en el FixedUpdate
        {
            rigi.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    Focusing(interactable);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            NotFocusing();
        }
    }

    void Focusing(Interactable newFocusingThis)
    {
        _focusingThis = newFocusingThis;
    }

    void NotFocusing()
    {
        _focusingThis = null;
    }
}
