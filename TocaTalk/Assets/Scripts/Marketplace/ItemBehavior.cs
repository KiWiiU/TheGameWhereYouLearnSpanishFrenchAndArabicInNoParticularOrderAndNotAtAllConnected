using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{

    public MarketplaceItem item;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.Sprite;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<ShoppingCart>().addItem(item);
            Destroy(gameObject);
        }
    }
}
