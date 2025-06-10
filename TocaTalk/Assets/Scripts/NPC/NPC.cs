using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCBase npc;

    private float wobbleTime = 1f;
    private float wobbleSpeed = 5f;
    private Vector2 lastPosition;
    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = npc.Sprite;
        lastPosition = transform.position;
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
    }
}
