using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriveIntro : MonoBehaviour
{
    GameObject player;
    public Transform carStop;
    public Transform carGo;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Holder.canPlayerMove = false;
        player.GetComponent<PlayerCosmetics>().MakeInvisible();
        StartCoroutine(CarStop());
    }

    IEnumerator CarStop()
    {
        yield return new WaitForSeconds(1f);
        while (Vector3.Distance(transform.position, carStop.position) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, carStop.position, 0.01f);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        // player gets out of car
        player.GetComponent<PlayerCosmetics>().MakeVisible();
        Holder.canPlayerMove = true;
        // car leaves
        yield return new WaitForSeconds(0.25f);
        while (Vector3.Distance(transform.position, carGo.position) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(transform.position, carGo.position, 0.015f);
            yield return null;
        }
    }
}
