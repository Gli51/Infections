using UnityEngine;

public class Player : MonoBehaviour
{
  public Rigidbody2D rb;

  // 0 = invalid player, 1 = blue player, 2 = red player
  public int infectedState;
  public Game game;
  public float maxSpeed;
  public Vector2 startPos, endPos, direction;
  public FakePlayer fakePlayerPrefab;
  public bool canMove;
  private Vector3 mousePos;
  private Camera mainCam;
  private Vector3 originalLocalScale;
  public GameObject aimer;

  private void Awake()
  {
    if (rb == null)
      rb = GetComponent<Rigidbody2D>();

    game = FindObjectOfType<Game>();
    originalLocalScale = transform.localScale;
  }

  private void Start()
  {
    mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
    canMove = false;
    aimer.SetActive(false);
  }


  private void Update()
  {
    // spawn fake player
    if ((game.state == GameState.BLUETURN && infectedState == 1) || (game.state == GameState.REDTURN && infectedState == 2))
    {

      FakePlayer fp = Instantiate(fakePlayerPrefab, transform.position, transform.rotation);
      if (infectedState == 1)
      {
        fp.GetComponent<SpriteRenderer>().color = Color.green;
        fp.gameObject.layer = LayerMask.NameToLayer("TransparentFX");
      }
      else
      {
        fp.GetComponent<SpriteRenderer>().color = Color.red;
        fp.gameObject.layer = LayerMask.NameToLayer("Water");
      }
      Physics2D.IgnoreCollision(fp.GetComponent<Collider2D>(), GetComponent<Collider2D>());
      Physics2D.IgnoreCollision(fp.GetComponent<Collider2D>(), GetComponent<ImaginaryRigidBody>().shadow.GetComponent<Collider2D>());
    }

    // claimp the speed of the player
    if (rb.velocity.magnitude > maxSpeed)
    {
      rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    //aiming arrow follows mouse
    mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    Vector3 rotation = mousePos - transform.position;
    float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
    aimer.transform.rotation = Quaternion.Euler(0, 0, rotZ);

    if (canMove == true)
    {
      aimer.SetActive(true);
    }
    float BASE_SPEED = Mathf.Exp(1.0f);
    // if player is moving, rotate the player to the direction of movement
    if (rb.velocity.magnitude > BASE_SPEED) // since we are using LogE
    {
      float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

      // squeeze the player when moving
      transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y / (Mathf.Log(rb.velocity.magnitude)), originalLocalScale.z);
    }
    else
    {
      // reset the player size when not moving
      transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y, originalLocalScale.z);
    }
    if (transform.localScale.y < originalLocalScale.y)
    {
      transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y + (transform.localScale.y - originalLocalScale.y) / 4.0f, originalLocalScale.z);
    }


  }


  private void OnCollisionEnter2D(Collision2D collision)
  {
  }

}
