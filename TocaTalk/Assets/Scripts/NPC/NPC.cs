using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCBase npc;

    private float wobbleTime = 1f;
    private float wobbleSpeed = 5f;
    private Vector2 lastPosition;
    private SpriteRenderer sprite;
    private SpriteRenderer playerSprite;
    private GameObject player;
    private BoxCollider2D boxCollider;
    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = npc.Sprite;
        lastPosition = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();
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
        sprite.sortingOrder = ((Vector2)transform.position + boxCollider.offset).y < playerSprite.bounds.center.y ? 9999 : -9999;
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
