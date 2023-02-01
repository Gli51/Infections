using UnityEngine;

public class FakePlayer : MonoBehaviour
{
  public Rigidbody2D rb;

  public float maxSpeed;

  public float life = 1.0f;

  private void Awake()
  {
    if (rb == null)
      rb = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {

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

    if (life < 0.0f)
    {
      Destroy(gameObject);
    }
  }


  private void OnCollisionEnter2D(Collision2D collision)
  {
  }

}
