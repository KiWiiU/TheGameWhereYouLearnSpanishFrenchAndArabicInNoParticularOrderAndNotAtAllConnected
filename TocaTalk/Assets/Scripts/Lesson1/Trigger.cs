using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            GameObject.FindWithTag("GameController").GetComponent<Events>().Trigger();
            Destroy(this);
        }
    }
}
