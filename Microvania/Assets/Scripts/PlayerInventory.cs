using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Pickup> items;

    public static PlayerInventory Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (Instance != this)
            Destroy(this);

        public Pickup GetItemOfType(PickupType type)
    {
        foreach (Pickup pickup in items)
        {
            if (pickup.type == type)
            {
                return pickup;
            }
        }
        return null;
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
