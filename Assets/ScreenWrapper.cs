using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{

  //0.1 of the view size
  public float margin = 0.1f;
  private SpriteRenderer sprite;
  private SpriteRenderer leftSpriteRenderer;
  private SpriteRenderer rightSpriteRenderer;

  // TODO: collision isn't synced
  void SyncSprite()
  {
    leftSpriteRenderer.sprite = sprite.sprite;
    rightSpriteRenderer.sprite = sprite.sprite;

    //assign the same sorting layer and order to the new game object
    leftSpriteRenderer.sortingLayerName = sprite.sortingLayerName;
    leftSpriteRenderer.sortingOrder = sprite.sortingOrder;
    rightSpriteRenderer.sortingLayerName = sprite.sortingLayerName;
    rightSpriteRenderer.sortingOrder = sprite.sortingOrder;

    //assign the same color to the new game object
    leftSpriteRenderer.color = sprite.color;
    rightSpriteRenderer.color = sprite.color;

    //assign the same material to the new game object
    leftSpriteRenderer.material = sprite.material;
    rightSpriteRenderer.material = sprite.material;

    //assign the same flip to the new game object
    leftSpriteRenderer.flipX = sprite.flipX;
    leftSpriteRenderer.flipY = sprite.flipY;
    rightSpriteRenderer.flipX = sprite.flipX;
    rightSpriteRenderer.flipY = sprite.flipY;

    //assign the same sorting layer and order to the new game object
    leftSpriteRenderer.sortingLayerName = sprite.sortingLayerName;
    leftSpriteRenderer.sortingOrder = sprite.sortingOrder;
    rightSpriteRenderer.sortingLayerName = sprite.sortingLayerName;
    rightSpriteRenderer.sortingOrder = sprite.sortingOrder;

    //assign the same position but shifted
    leftSpriteRenderer.transform.position = sprite.transform.position + new Vector3(-Camera.main.orthographicSize * Camera.main.aspect * 2, 0, 0);
    rightSpriteRenderer.transform.position = sprite.transform.position + new Vector3(Camera.main.orthographicSize * Camera.main.aspect * 2, 0, 0);

    //assign the same size
    leftSpriteRenderer.transform.localScale = sprite.transform.localScale;
    rightSpriteRenderer.transform.localScale = sprite.transform.localScale;
  }

  // Start is called before the first frame update
  void Start()
  {
    // if the game object has sprite
    sprite = GetComponent<SpriteRenderer>();
    if (sprite != null)
    {
      //create a new game object to hold the sprite
      GameObject leftSprite = new GameObject(this.gameObject.name + "_leftSprite");
      GameObject rightSprite = new GameObject(this.gameObject.name + "_rightSprite");

      //assign the same sprite to the new game object
      leftSpriteRenderer = leftSprite.AddComponent<SpriteRenderer>();
      rightSpriteRenderer = rightSprite.AddComponent<SpriteRenderer>();
    }
  }

  // Update is called once per frame
  void Update()
  {
    //this is the width of the screen in world units (it depends on the camera settings)
    //add a margin so the wrapping area is slightly larger than the camera view and the asteroids
    //exit the screen before teleporting on the other side
    float screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2 + margin;
    float screenHeight = Camera.main.orthographicSize * 2 + margin;

    //i can't assign a vector component to a transform directly so I use a temporary variable
    //even if most of the times won't be changes
    Vector2 newPosition = transform.position;

    //check all the margin 
    if (transform.position.x > screenWidth / 2)
    {
      newPosition.x = -screenWidth / 2;
    }

    if (transform.position.x < -screenWidth / 2)
    {
      newPosition.x = screenWidth / 2;
    }

    if (transform.position.y > screenHeight / 2)
    {
      Debug.Log("Error: Asteroid out of bounds");
    }

    if (transform.position.y < -screenHeight / 2)
    {
      Debug.Log("Error: Asteroid out of bounds");
    }

    //assign it to the transform
    transform.position = newPosition;

    // sync
    if (sprite != null)
    {
      SyncSprite();
    }
  }
}
