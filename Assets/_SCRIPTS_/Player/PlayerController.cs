using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Mov Velocidad y Fuerza")]
    public float _speed;
    public float _speedRotation;
    public float _jumpForce;
    Vector3 _movement;
    
    [Header("Referencia")]
    public Rigidbody _rigi;
    
    
    [Header("CheckGround")]
    public Vector3 _checkgroundPosition;
    public bool _isGround;
    public float _checkGroundRatio;
    public LayerMask _checkGroundMask;

    [SerializeField] private Camera _playerCamera = default;

    [Header("Focus Stuff")] [SerializeField]
    private Interactable _focusingThis = default;

    private void Start()
    {
        _playerCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        _movement.x = Input.GetAxisRaw("Horizontal") * _speed;
        _movement.z = Input.GetAxisRaw("Vertical") * _speed;
        _movement = transform.TransformDirection(_movement);

        _isGround = Physics.CheckSphere(transform.position + _checkgroundPosition, _checkGroundRatio, _checkGroundMask);
        
        _movement.y = _rigi.velocity.y;
        _rigi.velocity = _movement;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * (-_speedRotation * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * (_speedRotation * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Space) && _isGround)
        {
            _rigi.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
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
        if (newFocusingThis != _focusingThis)
        {
            if (_focusingThis != null)
            {
                _focusingThis.OnDefocusing();
            }
            _focusingThis = newFocusingThis;
        } 
        newFocusingThis.OnFocusing(transform);
    }

    void NotFocusing()
    {
        if (_focusingThis != null)
        {
            _focusingThis.OnDefocusing();
        }
        _focusingThis.OnDefocusing();
        _focusingThis = null;
    }
}

