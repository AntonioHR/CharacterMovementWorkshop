using AntonioHR.Utils;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleSidescrollerMove : MonoBehaviour {
    

	private static float groundCheckRadius = .1f;

    #region Inspector Fields
    [SerializeField]
	private Vector2 feet;
	[SerializeField]
	private LayerMask groundLayers;
	[SerializeField]
	private float jumpForce = 2;
	[SerializeField]
	private float horizontalSpeed = 4;

    [SerializeField]
    private  KeyCode jumpKey = KeyCode.Space;
    #endregion

    private Rigidbody2D body;



    public bool AreFeetOnGround
	{
		get
		{
			Collider2D[] objectsOnFoot = Physics2D.OverlapCircleAll(FeetPositionInWorldSpace, groundCheckRadius, groundLayers.value);
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
	public Vector3 FeetPositionInWorldSpace
    {
        get
        {
            return transform.TransformPoint(feet);
        }
    }
	public bool Grounded
    {
        get; private set;
    }



    #region Unity Messages
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Grounded && Input.GetKeyDown(jumpKey))
        {
            DoJump();
        }
    }
	
	private void FixedUpdate () {
		if(!Grounded)
			Grounded = body.velocity.y <= 0 && AreFeetOnGround;

        MoveX(InputHelper.DefaultJoystickInput.x);
    }
    #endregion



    private void DoJump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Grounded = false;
    }
    private void MoveX(float horizontalAxis)
    {
        body.velocity = body.velocity.WithX(horizontalAxis * horizontalSpeed);
    }



    #region Debug
    private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(FeetPositionInWorldSpace, groundCheckRadius);
	}
    #endregion
}
