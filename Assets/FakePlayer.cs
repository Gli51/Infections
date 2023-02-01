using UnityEngine;

public class FakePlayer : MonoBehaviour
{
  public Rigidbody2D rb;
  public GameObject aimer;

  public float maxSpeed;

  public float life = 1.0f;

  private void Awake()
  {
    if (rb == null)
      rb = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    if (aimer != null)
      aimer.SetActive(false);
    Move();
  }

  public void Move()
  {
    Vector3 mousePos = Input.mousePosition;
    // convert mouse position to world position
    Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
    // get direction from player to mouse position
    Vector3 direction = (worldPos - transform.position);
    Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
    rb.AddForce(direction2D * maxSpeed);
  }


  private void Update()
  {
    life -= Time.deltaTime;

    if (life < 0.0f || rb.velocity.magnitude < 0.1f)
    {
      Destroy(gameObject);
    }

    // update rotation in the direction of movement
    if (rb.velocity.magnitude > 0.0f)
    {
      transform.rotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
    }
  }


  private void OnCollisionEnter2D(Collision2D collision)
  {
  }

}
