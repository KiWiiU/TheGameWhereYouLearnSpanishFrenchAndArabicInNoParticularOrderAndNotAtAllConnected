using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MarketplaceTrigger : MonoBehaviour
{
    public GameObject playerTeleportation;

    public Cinemachine.CinemachineVirtualCamera houseCam;
    public Cinemachine.CinemachineVirtualCamera marketplaceCam;

    public GameObject itemConfirmationMenu;

    public GameObject shoppingCartPrefab;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = playerTeleportation.transform.position;
            houseCam.Priority = 5;
            marketplaceCam.Priority = 10;
            GameObject cart = Instantiate(shoppingCartPrefab, other.gameObject.transform);
            cart.name = "ShoppingCart";
            cart.GetComponent<ShoppingCart>().confirmationMenuObj = itemConfirmationMenu;
        }
    }
}
