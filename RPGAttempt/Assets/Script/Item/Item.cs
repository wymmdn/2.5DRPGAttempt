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
    public bool isPickable { get; set;}

    public void discard()
    {
        
    }

    public void pickUp(Role role)
    {
        this.holder = role;
        role.itemBag.addItem(this);
    }

}
