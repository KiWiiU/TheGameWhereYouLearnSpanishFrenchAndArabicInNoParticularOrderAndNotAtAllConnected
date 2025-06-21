using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cashier : MonoBehaviour
{
    Events eventManager;
    Dictionary<string, int> playerGroceries;
    public GameObject confirmationMenu;
    public GameObject playerTeleportation;
    public DialogueList dialogues;
    GameObject player;
    bool triggered;
    public Cinemachine.CinemachineVirtualCamera houseCam;
    public Cinemachine.CinemachineVirtualCamera marketplaceCam;
    void Start()
    {
        triggered = false;
        eventManager = GameObject.FindWithTag("GameController").GetComponent<Events>();
        playerGroceries = new();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        player = collision.gameObject;
        if (player.tag == "Player") // if actually player
        {
            confirmationMenu.GetComponent<Canvas>().enabled = true;
            Holder.canPlayerMove = false;
        }
    }

    public void YesMenu()
    {
        triggered = true;
        confirmationMenu.GetComponent<Canvas>().enabled = false;
        Holder.canPlayerMove = true;
        Grade(player);
        eventManager.EnqueueDialogue(dialogues);
        StartCoroutine(eventManager.NextEvent());
        eventManager.OnEventsEnd += LeaveMarketplace;
    }

    private void LeaveMarketplace()
    {
        eventManager.OnEventsEnd -= LeaveMarketplace;
        player.transform.position = playerTeleportation.transform.position;
        houseCam.Priority = 10;
        marketplaceCam.Priority = 5;
        Destroy(player.transform.Find("ShoppingCart").gameObject);
    }

    public void NoMenu()
    {
        Holder.canPlayerMove = true;
        confirmationMenu.GetComponent<Canvas>().enabled = false;
    }

    public void ClearCart()
    {
        Holder.canPlayerMove = true;
        confirmationMenu.GetComponent<Canvas>().enabled = false;
        player.transform.Find("ShoppingCart").GetComponent<ShoppingCart>().ClearCart();
    }
    void Grade(GameObject player)
    {
        foreach (MarketplaceItem item in player.transform.Find("ShoppingCart").GetComponent<ShoppingCart>().items)
        { // count groceries
            try
            {
                playerGroceries.Add(item.Name, 1);
            }
            catch (ArgumentException) // if the item was counted before, increment value by one
            {
                playerGroceries[item.Name] = playerGroceries[item.Name] + 1;
            }
        }

        // check for correct items
        CheckItem("Apple", 1);
        CheckItem("Banana", 2);
        CheckItem("Grapes", 1);
        CheckItem("Orange", 1);
        CheckItem("Pear", 1);
        CheckItem("Strawberry", 5);

        // check for incorrect items (checkitem removes the key from the dictionary)
        eventManager.totalScore += playerGroceries.Count;
        print(eventManager.playerScore + "/" + eventManager.totalScore);
    }

    void CheckItem(string item, int count)
    {
        if (!playerGroceries.ContainsKey(item))
        {
            eventManager.totalScore++;
            return;
        }
        if (playerGroceries[item] == count)
        {
            eventManager.playerScore++;
            eventManager.totalScore++;
        }
        else
        {
            eventManager.totalScore++;
        }
        playerGroceries.Remove(item);
    }
}
