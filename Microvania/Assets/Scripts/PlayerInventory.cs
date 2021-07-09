using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> items;

    public static event Action<Item> OnAnyItemPickedUp;

    public static PlayerInventory Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
        {
            Debug.Log("More than one instance of " + name);
            Destroy(this);
        }

        //items = new SortedSet<Item>();
    }

    public void AddItem(Item item)
    {
        OnAnyItemPickedUp?.Invoke(item);
        if (!items.Contains(item))
        {
            items.Add(item);
        }
            
    }
    public bool ContainsItem(Item targetItem)
    {
        return items.Contains(targetItem);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
