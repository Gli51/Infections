using UnityEngine;


public class InfectableCell : MonoBehaviour
{
    public Game gameManager;

    public new Rigidbody2D rigidbody;

    // sprite 0, 1, 2 corresponding to not-infected, infected by blue, infected by red
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // 0 = not-infected, 1 = infected by blue, 2 = infected by red
    public int infectedState = 0;


    private void Awake()
    {
        gameManager = FindObjectOfType<Game>();

        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if(rigidbody == null)
            rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
      // every cell start with not-infected state
      spriteRenderer.sprite = sprites[0];

      // the position should be set by whoever spawn the infectablecell
      // perhaps in Game.cs
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      // when player hit the cell, it will be infected
      if (collision.gameObject.CompareTag("Player"))
      {
        // if the cell is not infected, it will be infected by the player
        if (infectedState == 0)
        {
          infectedState = collision.gameObject.GetComponent<Player>().infectedState;
          spriteRenderer.sprite = sprites[infectedState];
        }
        // if the cell is infected by the other player, it will be infected by both players
        else if (infectedState != collision.gameObject.GetComponent<Player>().infectedState)
        {
          infectedState = 3;
          spriteRenderer.sprite = sprites[infectedState];
        }
      }
    }

    public bool isTouching(InfectableCell[] otherCells) {
      for (int i = 0; i < otherCells.Length; i++) {
        if (otherCells[i] == this || otherCells[i] == null) {
          continue;
        }
        if (this.GetComponent<Collider2D>().IsTouching(otherCells[i].GetComponent<Collider2D>()) == true) {
          return true;
        }
      }
      return false;
    }
}
