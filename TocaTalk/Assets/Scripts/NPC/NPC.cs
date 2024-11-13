using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCBase npc;
    public void Start() {
        GetComponent<SpriteRenderer>().sprite = npc.Sprite;
    }
}
