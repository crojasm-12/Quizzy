using UnityEngine;


/***************************
 * 
 * Quizzy Position Platform 1
 * -139.1, 4, 78.61
 * 
 * 
 * **************************/
public class QuizzyMovement : MonoBehaviour
{
    // Bindings
    public CharacterController controller;
    public Animator animator;
    public Rigidbody rigidBody;

    // Public Variables
    public float speed = 500f; // Adjust this value to control the movement speed
    public float rotationSpeed = 720f; // Degrees per second
    private float gravity = -9.81f;


    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity = 0.0f;

    // Input Values
    private float _horizontal;
    private float _vertical;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    // I need to keep in memory what is the direction of the character.
    private Vector3 _currentDirection;

    private void Start()
    {
        controller.enabled = false;
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
        _currentDirection = transform.forward;
        controller.enabled = true;
        Debug.Log($"Original Position: {_originalPosition.x}, {_originalPosition.y}, {_originalPosition.z}");
    }


    void Update()
    {
        _horizontal = UltimateJoystick.GetHorizontalAxis("LeftJoystick");
        _vertical = UltimateJoystick.GetVerticalAxis("LeftJoystick");

        UpdatePosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hitDirection = transform.position - collision.transform.position;
        hitDirection = hitDirection.normalized;

        // Adjust these values as needed
        float forceMagnitude = 300f; // The amount of force to apply
        hitDirection.y = 0; // Assuming you don't want to add force in the Y direction

        // Apply the force
        rigidBody.AddForce(hitDirection * forceMagnitude);
    }


    private void UpdatePosition()
    {
        //Debug.Log($"Location: {_horizontal}, {_vertical}");
        Vector3 direction = new Vector3(-_horizontal, 0, -_vertical);
        float magnitude = Mathf.Clamp01(direction.magnitude);

        Vector3 movementDirection;
        direction.Normalize();
        _currentDirection = direction;
        if (magnitude > 0.2f)
        {
            Quaternion toRotation = Quaternion.LookRotation(_currentDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        movementDirection = _currentDirection;
        if (controller.isGrounded)
        {
            movementDirection.y = -200.0f;
        }
        else
        {
            movementDirection.y += gravity * gravityMultiplier * Time.deltaTime;
        }
        controller.Move(movementDirection * magnitude * speed * Time.deltaTime);

        if (magnitude == 0.0f)
        {
            animator.SetTrigger("Idle");
        }
        else if (magnitude > 0.5f)
        {
            animator.SetTrigger("Run");
        }
        else
        {
            animator.SetTrigger("Walk");
        }

        if (transform.position.y < 0.0f)
        {
            controller.enabled = false; // Temporarily disable to reset position
            transform.position = _originalPosition;
            transform.rotation = _originalRotation;
            controller.enabled = true; // Re-enable the controller
            //_velocity = Vector3.zero;
        }

    }
}
