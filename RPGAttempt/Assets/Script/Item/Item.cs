using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IPickable
{
    public Role holder;
    public SpriteRenderer sprite;
    public List<string> interactOpts = new List<string>();

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        interactOpts.Add(UIString.place);
        interactOpts.Add(UIString.discard);
    }
    [SerializeField]public bool isPickable { get; set;}

    public void discard()
    {
        this.holder = null;
        this.isPickable = true;
    }

    public void pickUp(Role role)
    {
        this.holder = role;
        this.isPickable = false;
        role.itemBag.addItem(this);
    }

}
