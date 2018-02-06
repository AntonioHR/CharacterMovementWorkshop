using AntonioHR.Utils;
using System;
using UnityEngine;

public class HorizontalForceSidescrollerMove : MonoBehaviour { 

    #region Inspector Variables
    public float moveForce = 5.0f;
    public KeyCode JumpKey = KeyCode.Space;
    public FrictionSettings frictionSettings;
    public JumpSettings jumpSettings;

    [Serializable]
    public class FrictionSettings
    {
        public static float StoppingThreshold = 1f;
        public float groundBrakeForce = 4.0f;
        public float airBrakeForce = 4.0f;
        public float speedCap = 5f;
        public float speedCapFrictionPower = 5f;
    }

    [Serializable]
    public class JumpSettings
    {
        public static float groundCheckRadius = .1f;
        public Vector2 feetPosition;
        public LayerMask groundLayers;
        public float jumpForce = 2;
    }
    #endregion


    public bool useFakeInput = false;
    [Range(-1, 1)]
    public float fakeInput = 0;

    Rigidbody2D rb;
    Vector2 input;
    Vector2 moveForceVec;
    bool facingLeft = false;


    public Vector2 Velocity { get { return rb.velocity; } }
    public bool FacingLeft { get { return facingLeft; } }
    public bool FeetOnGround
    {
        get
        {
            var layers = jumpSettings.groundLayers.value;
            var radius = JumpSettings.groundCheckRadius;
            var objectsOnFoot = Physics2D.OverlapCircleAll(FeetInWorldSpace, radius, layers);
            foreach (var obj in objectsOnFoot)
            {
                if (obj.gameObject != gameObject)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public Vector3 FeetInWorldSpace { get { return transform.TransformPoint(jumpSettings.feetPosition); } }

    public bool Grounded { get; private set; }
    public float BrakeForce { get { return Grounded ? frictionSettings.groundBrakeForce : frictionSettings.airBrakeForce; } }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(JumpKey) && Grounded)
        {
            DoJump();
        }

    }

    private void DoJump()
    {
        rb.AddForce(Vector2.up * jumpSettings.jumpForce, ForceMode2D.Impulse);
        Grounded = false;
    }

    void FixedUpdate()
    {
        input = InputHelper.DefaultJoystickInputRawCapped;

        if (useFakeInput)
            input.x = fakeInput;

        MoveHorizontal();
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        //if (!Grounded)
        Grounded = rb.velocity.y <= 0 && FeetOnGround;
    }

    private void MoveHorizontal()
    {
        AddHorizontalMoveForce();
        ApplyFriction();

        CheckFacing();


        rb.AddForce(moveForceVec);
    }

    private void AddHorizontalMoveForce()
    {
        moveForceVec = Vector2.right * input.x * moveForce;
    }

    private void CheckFacing()
    {
        if (input.x > 0)
            facingLeft = false;
        else if (input.x < 0)
            facingLeft = true;
    }

    private void ApplyFriction()
    {
        var xMag = Mathf.Abs(rb.velocity.x);

        if (xMag < FrictionSettings.StoppingThreshold && input == Vector2.zero)
        {
            FullStopHorizontal();
        }
        else 
        {
            ApplyHorizontalBrake();
        }

        if (xMag > frictionSettings.speedCap)
        {
            ApplyHorizontalSpeedCap();
        }
    }

    private void ApplyHorizontalSpeedCap()
    {
        float force = -Mathf.Sign(rb.velocity.x) * frictionSettings.speedCapFrictionPower;
        moveForceVec += Vector2.right * force;
    }

    private void FullStopHorizontal()
    {
        rb.velocity = rb.velocity.WithX(0);
    }

    private void ApplyHorizontalBrake()
    {
        if(input.x == 0)
        {
            float force = - Mathf.Sign(rb.velocity.x) * BrakeForce;
            moveForceVec += Vector2.right * force;
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(FeetInWorldSpace, JumpSettings.groundCheckRadius);
    }
}
