using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    float speedX, speedY;

    private AudioManager audioManager;
    private bool isWalkingSoundPlaying = false;
    private AudioClip currentWalkingClip;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    

    void Start() {
        currentWalkingClip = audioManager.walking;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        rb.linearVelocity = new Vector2(speedX, speedY);


        float speed = new Vector2(speedX, speedY).magnitude;


        animator.SetFloat("Speed", speed);

if (speed > 0.1f) {
    float angle = Mathf.Atan2(speedY, speedX) * Mathf.Rad2Deg;
    angle -= 90f;
    transform.rotation = Quaternion.Euler(0, 0, angle);
}

        bool isMoving = Mathf.Abs(speedX) > 0.1f || Mathf.Abs(speedY) > 0.1f;

if (isMoving && !isWalkingSoundPlaying) {
    audioManager.PlayWalking(currentWalkingClip);
    isWalkingSoundPlaying = true;
} else if (!isMoving && isWalkingSoundPlaying) {
    audioManager.StopWalking();
    isWalkingSoundPlaying = false;
}
    }

private void OnTriggerEnter2D(Collider2D other)
{
    AudioClip newClip = audioManager.walking;
    if (other.CompareTag("Dirt"))
        newClip = audioManager.walkingOnDirtGravel;


    if (currentWalkingClip != newClip)
    {
        currentWalkingClip = newClip;
        if (isWalkingSoundPlaying)
        {
            audioManager.StopWalking();
            audioManager.PlayWalking(currentWalkingClip);
        }
    }
}

private void OnTriggerExit2D(Collider2D other)
{

    AudioClip newClip = audioManager.walking;
    if (currentWalkingClip != newClip)
    {
        currentWalkingClip = newClip;
        if (isWalkingSoundPlaying)
        {
            audioManager.StopWalking();
            audioManager.PlayWalking(currentWalkingClip);
        }
    }
}
}
