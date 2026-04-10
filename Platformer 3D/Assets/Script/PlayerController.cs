using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float airControl = 0.6f;
    [SerializeField] private float rotationSpeed = 12f;
    [Header("Jump")]
    [SerializeField] private float jumpImpulse = 6f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;

    private Rigidbody _rb;
    private bool _isGrounded;
    private bool IsGrounded
    {
        get { return _isGrounded; }
        set 
        {
            if (value != _isGrounded && value)
            {
                AudioManager.Instance.PlayPlayerSfx(landSound);
            }
            _isGrounded = value;
        }
    }

    private Vector3 _moveInput;
    private bool _jumpPressed;

    private bool _isPaused;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_isPaused)
        {
            CheckMoveInput();
            CheckJumpInput();
            Rotate();
        }
    }

    private void Rotate()
    {
        if (_moveInput.sqrMagnitude > 0)
        {
            Quaternion target = Quaternion.LookRotation(_moveInput.normalized);
            float t = 1f - Mathf.Exp(-rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, t);
        }
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpPressed = true;
        }
    }

    private void CheckMoveInput() =>
        _moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

    private void FixedUpdate()
    {
        if (!_isPaused)
        {
            CheckGround();
            Move();
            Jump();
        }
    }

    private void Move()
    {
        //desired horizontal speed
        Vector3 desiredSpeed = _moveInput;
        if (desiredSpeed.sqrMagnitude > 1f) desiredSpeed.Normalize();
        desiredSpeed *= maxSpeed;

        //curr horizontal speed
        Vector3 v = _rb.linearVelocity;
        Vector3 currentPlanar = new Vector3(v.x, 0f, v.z);

        //curr -> desired
        float accel = IsGrounded ? acceleration : acceleration * airControl;

        Vector3 deltaV = desiredSpeed - currentPlanar;
        float maxDelta = accel * Time.fixedDeltaTime;

        if (deltaV.magnitude > maxDelta)
        {
            deltaV = deltaV.normalized * maxDelta;
        }

        _rb.AddForce(deltaV, ForceMode.VelocityChange);
    }

    private void Jump()
    {
        if (_jumpPressed)
        {
            _jumpPressed = false;
            AudioManager.Instance.PlayPlayerSfx(jumpSound);
            if (IsGrounded)
            {
                _rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            }
        }
    }

    public void AddKnockbackDeltaV(Vector3 deltaV)
    {
        deltaV.y = 0f;
        _rb.AddForce(deltaV, ForceMode.VelocityChange);
    }

    private void CheckGround() =>
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

    public void SetPauseState(bool state)
    {
        _isPaused = state;
    }

    private void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }






    //[SerializeField] private float speed;
    //[SerializeField] private float jumpForce;
    //[SerializeField] private float rotationSpeed;
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float groundCheckRedius;
    //[SerializeField] private LayerMask groundLayer;
    //private Rigidbody _rb;
    //private bool _isGrounded;

    //private void Awake()
    //{
    //    _rb = GetComponent<Rigidbody>();
    //}

    //private void Update()
    //{
    //    Move();
    //    CheckGround();
    //    Jump();
    //}

    //private void Move()
    //{
    //    float inputX = Input.GetAxis("Horizontal");
    //    float inputY = Input.GetAxis("Vertical");

    //    Vector3 movement = new Vector3 (inputX, 0, inputY);

    //    #region Rotation

    //    if(movement.magnitude > 0)
    //    {
    //        Quaternion target = Quaternion.LookRotation(movement);
    //        float t = 1 - Mathf.Exp(-rotationSpeed * Time.deltaTime);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, target, t);
    //    }

    //    #endregion

    //    Vector3 direction = _rb.position + movement * speed * Time.deltaTime; //TODO move to FixedUpdate()
    //    _rb.MovePosition(direction);
    //}

    //private void Jump()
    //{
    //    if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //    }
    //}

    //private void CheckGround()
    //{
    //    var colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRedius, groundLayer);

    //    _isGrounded = (colliders.Length > 0);

    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.burlywood;
    //    Gizmos.DrawSphere(groundCheck.position, groundCheckRedius);
    //}
}
