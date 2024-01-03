using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSolt : MonoBehaviour,IPointerClickHandler
{
    private ItemBag bag;
    public Image image;
    public Item item;

    private void Awake()
    {
        image = GetComponent<Image>();
        bag = GetComponentInParent<ItemBag>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            EventHandler.CallOpenInteractPanel(item.interactOpts, item, this.transform.position);
            bag.clickSlot = this;
        }
    }
    public void addItem(Item item)
    { 
        this.item = item;
        this.image.sprite = item.sprite.sprite;
        item.transform.SetParent(this.transform);
        item.gameObject.SetActive(false);
    }
}
