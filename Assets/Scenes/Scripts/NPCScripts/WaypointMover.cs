using System.Collections;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public Transform waypointParent;
    public float moveSpeed = 2.0f;
    public float waitTime = 2.0f;
    public bool loopWaypoints = true;

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private bool isWaiting;
    private bool movingForward = true;

    private Rigidbody2D rb;
    private bool isMoving = true;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }
    }

    void FixedUpdate()
    {
        if (isWaiting || !isMoving) 
        {

            if (animator != null)
                animator.SetFloat("Speed", 0);
            return;
        }

        MoveToWayPoint();
    }

    void MoveToWayPoint()
{
    Transform target = waypoints[currentWaypointIndex];
    Vector2 direction = (target.position - transform.position).normalized;

    Vector2 newPosition = Vector2.MoveTowards(rb.position, target.position, moveSpeed * Time.deltaTime);
    rb.MovePosition(newPosition);

    Vector2 velocity = direction * moveSpeed;


    if (animator != null)
    {
        float speed = velocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (speed > 0.001f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            angle -= 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    if (Vector2.Distance(rb.position, target.position) < 0.1f)
    {
        isWaiting = true;
        StartCoroutine(WaitAtWayPoint());
    }
}


    IEnumerator WaitAtWayPoint()
    {
        yield return new WaitForSeconds(waitTime);

        if (movingForward)
        {
            if (currentWaypointIndex < waypoints.Length - 1)
            {
                currentWaypointIndex++;
            }
            else
            {
                movingForward = false;
                currentWaypointIndex--;
            }
        }
        else
        {
            if (currentWaypointIndex > 0)
            {
                currentWaypointIndex--;
            }
            else
            {
                movingForward = true;
                currentWaypointIndex++;
            }
        }

        isWaiting = false;
    }

    public void StopMovement()
    {
        isMoving = false;
        if (animator != null)
            animator.SetFloat("Speed", 0);
    }

    public void StartMovement()
    {
        isMoving = true;
    }
}
