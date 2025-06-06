using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    // When player walks into trigger, it triggers the events of the level
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            GameObject.FindWithTag("GameController").GetComponent<Events>().doThing(true);
            Destroy(this);
        }
    }
}
