using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
  public float speed;
  public int startIndex;
  public Transform[] locations;
  private int i;

  // Start is called before the first frame update
  void Start()
  {
    i = startIndex;
    transform.position = locations[i].position;
  }

  // Update is called once per frame
  void Update()
  {
    if (Vector2.Distance(transform.position, locations[i].position) < 0.1f)
    {
      i++;
      if (i == locations.Length) i = 0;
    }
    transform.position = Vector2.MoveTowards(transform.position, locations[i].position, speed * Time.deltaTime);
  }

  void OnCollisionEnter2D(Collision2D collision)
  {

    collision.transform.parent = transform;

  }

  void OnCollisionExit2D(Collision2D collision)
  {

    collision.transform.parent = null;

  }

  public void SetIndex(int index)
  {
    i = index;
  }
}
