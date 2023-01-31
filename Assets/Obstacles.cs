using UnityEngine;
using System.Collections.Generic;
using ElRaccoone.Tweens;


public class Obstacles : MonoBehaviour
{
  public Game gameManager;

  public new Rigidbody2D rigidbody;
  public CircleCollider2D circleCollider;
  public SpriteRenderer spriteRenderer;
  public Sprite sprite;

  private bool duringAnimation;
  public const float ANIMATION_TIME = 0.08f;
  public const float ANIMATION_SCALE = 1.02f;
  private Vector3 originalScale;
  private void Awake()
  {
    gameManager = FindObjectOfType<Game>();

    if (spriteRenderer == null)
      spriteRenderer = GetComponent<SpriteRenderer>();

    if (rigidbody == null)
      rigidbody = GetComponent<Rigidbody2D>();

    if (circleCollider == null)
      circleCollider = GetComponent<CircleCollider2D>();

    originalScale = this.transform.localScale;
  }

  private void Start()
  {
    spriteRenderer.sprite = sprite;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    // don't apply bouncy animation between two obstacles
    if (!collision.gameObject.CompareTag("Bullet"))
    {

      if (duringAnimation)
      {
        return;
      }
      this.gameObject.TweenLocalScale(new Vector3(ANIMATION_SCALE, ANIMATION_SCALE, 0), ANIMATION_TIME).SetEaseQuartInOut().SetFrom(originalScale).SetOnComplete(() =>
      {
        this.gameObject.TweenLocalScale(originalScale, ANIMATION_TIME).SetEaseQuartInOut();
        duringAnimation = false;
      }).SetOnCancel(() =>
      {
        this.gameObject.TweenLocalScale(originalScale, ANIMATION_TIME).SetEaseQuartInOut();
        duringAnimation = false;
      }).SetOnStart(() =>
      {
        duringAnimation = true;
      });
    }
  }

  public bool isTouching(List<Component> other)
  {
    for (int i = 0; i < other.Count; i++)
    {
      if (other[i] == this || other[i] == null)
      {
        continue;
      }
      if (this.GetComponent<Collider2D>().IsTouching(other[i].GetComponent<Collider2D>()) == true)
      {
        return true;
      }
    }
    return false;
  }
}