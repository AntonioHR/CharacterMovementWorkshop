using AntonioHR.MovementExamples;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class DumbestMovementAnimated : MonoBehaviour {

	public float speed = 5.0f;
	public bool useRawInput = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool wasFacingLeft = false;
	

    //This is the 'dumbest' movement script for top-down movement, it uses the joystick value to move the transform manually
    //The biggest problems with this are:
    //          *No Collision Handling
	void Start () {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	void Update ()
    {
        Vector3 input = useRawInput ? InputHelper.DefaultJoystickInputRaw : InputHelper.DefaultJoystickInput;

        transform.position += input * speed * Time.deltaTime;



        animator.SetFloat("velocity_magnitude", input.magnitude);
        checkFacingSide(input);
    }

    private void checkFacingSide(Vector3 input)
    {
        bool movingLeft = input.x < 0;
        bool facingLeft = (movingLeft || (input.x == 0 && wasFacingLeft));
        spriteRenderer.flipX = facingLeft;
        wasFacingLeft = facingLeft;
    }
}
