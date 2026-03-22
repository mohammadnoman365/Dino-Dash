using UnityEngine;

public class sawMovement : MonoBehaviour
{

    public Vector2 pos1;
    public Vector2 pos2;
    public Vector2 posdiff = new Vector2(2f,0);
    float speed = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos1 = transform.position;
        pos2 = pos1 + posdiff;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 0.5f));
        transform.Rotate(0, 0, 3f);
    }
}
