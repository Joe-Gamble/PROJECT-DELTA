using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isRunning;
    private bool isIdle;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    //TP Variables
    public float turn_smooth_time = 0.1f;
    private float turn_smooth_velocity;

    private InputManager inputManager;
    private PlayerCamManager pcm;
    private Animator animator;

    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        pcm = PlayerCamManager.Instance;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    void Update()
    {
        Vector2 move = PlayerMove();
        PlayerAnimate(move);
    }

    void PlayerAnimate(Vector2 move)
    {
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.y);

        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isIdle", isIdle);
    }

    Vector2 PlayerMove()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        isRunning = inputManager.Shift();

        //Movement

        Vector2 movement = inputManager.GetPlayerMovement();

        isIdle = (movement == Vector2.zero);

        Vector3 move;
        float speed = playerSpeed;

        if (isRunning)
        {
            speed *= 2;
        }

        if (pcm.GetCamState() == PlayerCamManager.CamStates.FIRST_PERSON)
        {
            //FPS
            move = transform.right * movement.x + transform.forward * movement.y;

            move.y = 0;
            controller.Move(move * Time.deltaTime * speed);
        }
        else
        {
            Vector3 direction = new Vector3(movement.x, 0f, movement.y).normalized;
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + pcm.GetRawCam().transform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turn_smooth_velocity, turn_smooth_time);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);

                movement.y = Mathf.Abs(movement.y);
            }
        }

        // Changes the height position of the player..
        if (inputManager.Jumping() && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        return movement;
    }
}
