using UnityEngine;

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
    private float velocity;

    // Input Values
    private float _horizontal;
    private float _vertical;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private void Start()
    {
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        Debug.Log($"Original Position: {_originalRotation.x}, {_originalRotation.y}, {_originalRotation.z}");
    }


    void Update()
    {
        /*
        foreach (Touch touch in Input.touches)
        {
            Vector2 touchPosition = touch.position;
            if (UltimateJoystick.FindObjectsByType<Joy(touchPosition))
            {
                // Process joystick movement
            }
            else
            {
                // Ignore touches outside the joystick area
            }
        }
        */
        _horizontal = UltimateJoystick.GetHorizontalAxis("LeftJoystick");
        _vertical = UltimateJoystick.GetVerticalAxis("LeftJoystick");

        UpdatePosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hitDirection = transform.position - collision.transform.position;
        hitDirection = hitDirection.normalized;

        // Adjust these values as needed
        float forceMagnitude = 100f; // The amount of force to apply
        //hitDirection.y = 0; // Assuming you don't want to add force in the Y direction

        // Apply the force
        rigidBody.AddForce(hitDirection * forceMagnitude);
    }


    private void UpdatePosition()
    {
        //Debug.Log($"Location: {_horizontal}, {_vertical}");
        Vector3 direction = new Vector3(-_horizontal , 0, -_vertical);
        float magnitude = Mathf.Clamp01(direction.magnitude);

        if (magnitude > 0.2f)
        {
            direction.Normalize();

            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (controller.isGrounded && velocity <= 0.0f)
            {
                velocity = 0.0f;
            }
            else
            {
                velocity += gravity * gravityMultiplier * Time.deltaTime;
            }
            direction.y = velocity;
            controller.Move(direction * magnitude * speed * Time.deltaTime);
        }

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
            transform.rotation = _originalRotation;
            transform.position = new Vector3(-139.1f, 4.0f, 78.61f);

            //Vector3 localPosition = transform.InverseTransformPoint(new Vector3(-139.1f, 4.0f, 78.61f));
            //controller.Move(localPosition);
            //transform.position = new Vector3(localPosition.x, localPosition.y, localPosition.z);
            //transform.transform.localPosition = localPosition;
            //controller.Move(_originalPosition);
        }

    }
}
