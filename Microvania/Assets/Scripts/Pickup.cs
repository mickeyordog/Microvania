using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Pickup : MonoBehaviour
{
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp()
    {
        PlayerInventory.Instance.AddItem(item);
        Destroy(gameObject);
    }
}

