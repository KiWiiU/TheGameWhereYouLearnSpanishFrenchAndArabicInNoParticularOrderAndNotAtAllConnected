using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingCart : MonoBehaviour
{
    public List<MarketplaceItem> items;
    public GameObject visualPrefab;
    public GameObject confirmationMenuObj;
    private Transform parent;
    private Vector3 offset;
    private Vector3 initialOffset;
    private int numRows;
    private int numColumns;
    private int itemsInColumn;
    public float cartSlope = 4.642857f;
    public void Start()
    {
        itemsInColumn = 0;
        numRows = 1;
        numColumns = 3;
        parent = transform.GetChild(0);
        initialOffset = new Vector3(-3f, -1.83f, 0);
        offset = initialOffset;
        items = new List<MarketplaceItem>();
    }

    public void confirmationMenu(GameObject obj)
    {
        Holder.canPlayerMove = false;
        MarketplaceItem item = obj.GetComponent<ItemBehavior>().item;
        confirmationMenuObj.GetComponent<Canvas>().enabled = true;
        confirmationMenuObj.GetComponent<Animator>().SetBool("isOpen", true);
        confirmationMenuObj.transform.GetChild(0).Find("Question").GetComponent<TMP_Text>().text = "Do you want to buy " + item.Name + "?";
        confirmationMenuObj.transform.GetChild(0).Find("Yes").GetComponent<Button>().onClick.RemoveAllListeners();
        confirmationMenuObj.transform.GetChild(0).Find("No").GetComponent<Button>().onClick.RemoveAllListeners();
        confirmationMenuObj.transform.GetChild(0).Find("Yes").GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(closeMenu());
            addItem(item);
            // Destroy(obj);
        });
        confirmationMenuObj.transform.GetChild(0).Find("No").GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(closeMenu());
        });
    }

    private IEnumerator closeMenu()
    {
        confirmationMenuObj.GetComponent<Animator>().SetBool("isOpen", false);
        yield return new WaitForSeconds(0.5f);
        Holder.canPlayerMove = true;
    }
    public void addItem(MarketplaceItem item)
    {
        items.Add(item);
        GameObject visual = Instantiate(visualPrefab, parent);
        visual.GetComponent<SpriteRenderer>().sprite = item.Sprite;
        visual.transform.localPosition = offset;
        offset.x += 3f;
        itemsInColumn++;
        if (itemsInColumn >= numColumns)
        {
            offset.y += 4.33f;
            numRows++;
            numColumns = 3 + Mathf.FloorToInt((numRows + 3) / cartSlope);
            offset.x = initialOffset.x - ((numColumns - 3) * 1.5f);
            itemsInColumn = 0;
        }
    }

    public void ClearCart()
    {
        items.Clear();
        foreach (Transform child in transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
    }

}
