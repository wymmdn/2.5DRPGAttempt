using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBag : MonoBehaviour
{
    
    public ItemSolt clickSlot;
    [SerializeField]public RaycastHit2D rayCast;
    public Transform hitTransform;
    [SerializeField]private GameObject slotPrefab;
    [SerializeField]private Dictionary<ItemSolt,Item> items = new Dictionary<ItemSolt,Item>();
    private GridLayoutGroup bagPanel;
    private PlayerController player;
    private Role holder;
    private bool onPlace;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(tagtag.player).GetComponent<PlayerController>();
        bagPanel = GetComponentInChildren<GridLayoutGroup>();
        clickSlot = null;
        onPlace = false;
    }
    private void OnEnable()
    {
        EventHandler.CloseInteractPanel += performAct;
    }
    private void OnDisable()
    {
        EventHandler.CloseInteractPanel -= performAct;
    }
    private void Update()
    {
        if (onPlace == true)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickSlot.item.transform.position = new Vector3(pos.x,pos.y);
            if (Input.GetMouseButton(0))
            {
                onPlace = false;
                hitTransform = Physics2D.OverlapPoint(clickSlot.item.transform.position, (LayerMask.GetMask("Default") | 
                                                                                          LayerMask.GetMask("UI") | 
                                                                                          LayerMask.GetMask("Barrier")))?.transform;
                if (hitTransform == null)
                {
                    StartCoroutine(onMove(pos, clickSlot));
                }
                else {
                    placeFailed();
                }
                clickSlot.item.gameObject.SetActive(false);
                clickSlot.image.enabled = true;
                clickSlot = null;
            }
        }
    }
    public void addItem(Item item)
    {
        ItemSolt slot = Instantiate(slotPrefab, bagPanel.transform).GetComponent<ItemSolt>();
        slot.addItem(item);
        items.Add(slot, item);
    }
    private void performAct(string act)
    {
        Debug.Log(act);
        if (clickSlot == null) return;
        switch (act)
        { 
            case "":
                break;
            case UIString.place:
                startPlace(clickSlot);
                break;
            case UIString.equip:
                equip();
                break;
            default:
                break;
        }
        if(act != UIString.place)
            clickSlot = null;
    }
    private void startPlace(ItemSolt slot)
    {
        slot.item.gameObject.SetActive(true);
        slot.image.enabled = false;
        onPlace = true;
    }
    private void equip()
    {
        clickSlot.item.gameObject.SetActive(true);
        Weapon weapon = clickSlot.item as Weapon;
        weapon.equip(player);
        items.Remove(clickSlot);
        clickSlot.transform.SetParent(null);
        Destroy(clickSlot.gameObject);
    }
    private IEnumerator onMove(Vector2 target,ItemSolt slot)
    {
        yield return null;
        while (Vector2.Distance(player.transform.position, target) > player.interactRadius)
        {
            player.movement(target);
            if (Input.GetKeyDown(KeyCode.W) ||
               Input.GetKeyDown(KeyCode.A) ||
               Input.GetKeyDown(KeyCode.S) ||
               Input.GetKeyDown(KeyCode.D) ||
               Input.GetKeyDown(KeyCode.Space) ||
               onPlace == true)
            {
                placeFailed();
                yield break;
            }
            yield return null;
        }
        placeSucceed(target,slot);
    }
    private void placeSucceed(Vector2 target,ItemSolt slot)
    {
        slot.item.gameObject.SetActive(true);
        slot.item.transform.SetParent(player.transform.parent);
        slot.item.transform.position = target;
        slot.item.discard();
        items.Remove(slot);
        slot.transform.SetParent(null);
        Destroy(slot.gameObject);
    }
    private void placeFailed()
    { 
    
    }
}
