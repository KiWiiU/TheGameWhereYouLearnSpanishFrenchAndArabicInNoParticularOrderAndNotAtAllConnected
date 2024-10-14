using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Collider bounds;
    public float walkSpeed;
    public void Update() {
        Vector3 vec = transform.position;
        vec += new Vector3(Input.GetAxisRaw("Horizontal")*walkSpeed,0,0);
        vec += new Vector3(0,Input.GetAxisRaw("Vertical")*walkSpeed,0);
        transform.position = vec;
    }
}
