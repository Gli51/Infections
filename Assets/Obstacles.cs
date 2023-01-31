using UnityEngine;
using System.Collections.Generic;

public class Obstacles : MonoBehaviour
{
    public Game gameManager;

    public new Rigidbody2D rigidbody;
    public CircleCollider2D circleCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    private void Awake()
    {
        gameManager = FindObjectOfType<Game>();

        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if(rigidbody == null)
            rigidbody = GetComponent<Rigidbody2D>();
        
        if(circleCollider == null)
            circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
      spriteRenderer.sprite = sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    
    public bool isTouching(List<Component> other) {
      for (int i = 0; i < other.Count; i++) {
        if (other[i] == this || other[i] == null) {
          continue;
        }
        if (this.GetComponent<Collider2D>().IsTouching(other[i].GetComponent<Collider2D>()) == true) {
          return true;
        }
      }
      return false;
    }
}