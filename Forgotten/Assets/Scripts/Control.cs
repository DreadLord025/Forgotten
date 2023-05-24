using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    public Animator anim;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool moveUp = false;
    private bool moveDown = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            float horizontalInput = 0f;
            float verticalInput = 0f;

            if (moveUp) verticalInput += 1f;
            if (moveDown) verticalInput -= 1f;
            if (moveLeft) horizontalInput -= 1f;
            if (moveRight) horizontalInput += 1f;

            movement = new Vector2(horizontalInput, verticalInput).normalized;

            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void MoveUpStart() { moveRight = true; }
    public void MoveDownStart() { moveRight = true; }
    public void MoveLeftStart() { moveRight = true; }
    public void MoveRightStart() { moveRight = true; }
    public void MoveUpStop() { moveUp = false; }
    public void MoveDownStop() { moveDown = false; }
    public void MoveLeftStop() { moveLeft = false; }
    public void MoveRightStop() { moveRight = false; }
}