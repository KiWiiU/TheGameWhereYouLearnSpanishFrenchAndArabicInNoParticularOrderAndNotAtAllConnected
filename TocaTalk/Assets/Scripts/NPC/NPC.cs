using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCBase npc;

    private float wobbleTime = 1f;
    private float wobbleSpeed = 5f;
    private Vector2 lastPosition;
    private SpriteRenderer sprite;
    private GameObject player;
    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = npc.Sprite;
        lastPosition = transform.position;
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (!transform.position.Equals(lastPosition))
        {
            wobbleTime += Time.deltaTime * wobbleSpeed;
            float rotationZ = Mathf.Sin(wobbleTime) * wobbleSpeed;
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        lastPosition = transform.position;
        sprite.sortingOrder = transform.position.y < player.transform.position.y ? 9999 : -9999;
    }


    public IEnumerator MoveTo(Vector2 position, float speed)
    {
        while (transform.position != (Vector3)position)
        {
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
            yield return null;
        }
    }
}
