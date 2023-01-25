using UnityEngine;


public class Obstacles : MonoBehaviour
{
    public Game gameManager;

    public new Rigidbody2D rigidbody;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

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
      spriteRenderer.sprite = sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}