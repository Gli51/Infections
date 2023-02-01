using UnityEngine;

public class ImaginaryRigidBody : MonoBehaviour
{
  private Rigidbody2D rb;
  private Collider2D collision;
  public GameObject shadow;

  void Sync()
  {
    // sync position
    shadow.transform.position = transform.position;
    // sync rotation
    shadow.transform.rotation = transform.rotation;
    // sync scale
    shadow.transform.localScale = transform.localScale;
    // sync velocity
    shadow.GetComponent<Rigidbody2D>().velocity = rb.velocity;
    // sync angular velocity
    shadow.GetComponent<Rigidbody2D>().angularVelocity = rb.angularVelocity;
  }

  // Start is called before the first frame update
  void Start()
  {
    if (shadow == null)
    {
      shadow = new GameObject(gameObject.name + "_Shadow");
      shadow.transform.parent = transform.parent;
      shadow.transform.position = transform.position;
      shadow.transform.rotation = transform.rotation;
      shadow.transform.localScale = transform.localScale;
      // change layer
      shadow.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
    // get foreign component
    collision = GetComponent<CircleCollider2D>();
    if (collision == null)
    {
      collision = GetComponent<CapsuleCollider2D>();
    }
    if (collision == null)
    {
      collision = GetComponent<BoxCollider2D>();
    }
    if (collision == null)
    {
      collision = GetComponent<PolygonCollider2D>();
    }
    if (collision == null)
    {
      collision = GetComponent<EdgeCollider2D>();
    }
    if (collision == null)
    {
      collision = GetComponent<CompositeCollider2D>();
    }
    if (collision == null)
    {
      Debug.LogError("No collider found on shadowed object");
    }

    rb = GetComponent<Rigidbody2D>();
    if (rb == null)
    {
      Debug.LogError("No rigidbody found on shadowed object");
    }

    // duplicate component
    collision = (Collider2D) CopyComponent(collision, shadow);
    rb = (Rigidbody2D) CopyComponent(rb, shadow);
  }

  // copilot thinks credit: https://answers.unity.com/questions/1019994/how-to-copy-a-component-at-runtime.html
  // actual credit: https://answers.unity.com/questions/458207/copy-a-component-at-runtime.html
  Component CopyComponent(Component original, GameObject destination)
  {
    System.Type type = original.GetType();
    Component copy = destination.AddComponent(type);
    // Copied fields can be restricted with BindingFlags
    System.Reflection.FieldInfo[] fields = type.GetFields();
    foreach (System.Reflection.FieldInfo field in fields)
    {
      field.SetValue(copy, field.GetValue(original));
    }
    return copy;
  }

  // Update is called once per frame
  void Update()
  {
    Sync();
  }
}
