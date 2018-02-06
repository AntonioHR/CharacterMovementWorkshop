using UnityEngine;

[RequireComponent(typeof(HorizontalForceSidescrollerMove))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimateByHorizontalForceMove : MonoBehaviour
{
    HorizontalForceSidescrollerMove mov;
    Animator animator;
    SpriteRenderer spr;

    private void Start()
    {
        mov = GetComponent<HorizontalForceSidescrollerMove>();
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        animator.SetFloat("velocity_x", Mathf.Abs(mov.Velocity.x));
        animator.SetFloat("velocity_y", mov.Velocity.y);

        animator.SetBool("grounded", mov.Grounded);

        spr.flipX = mov.FacingLeft;
    }
}
