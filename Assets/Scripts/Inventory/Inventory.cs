using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;  // Amount of slots in inventory

    // Current list of items in inventory
    public List<Item> items = new List<Item>();
    public int diamonds = 0;
    GameObject playerState;
    private void Start()
    {
        playerState = GameObject.Find("PlayerState");
        if (playerState != null)
        {
            diamonds = playerState.GetComponent<Player>().diamonds;
        }
    }

    public int Diamonds()
    {
        return diamonds;
    }
    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        // Debug.Log("lisätty " + diamonds);
    }
    public void AddDiamond()
    {
        diamonds += 1;
       // Debug.Log("lisätty " + diamonds);
    }
    public void RemoveDiamond(int cost)
    {
        diamonds -= cost;
    }

    public bool Add(Item item)
    {
        if (item.isFood)
            {
                return false;
            }
        if (item.isDiamond)
        {
            return true;
        }
        if (item.isStamina)
        {
            return true;
        }
        if (item.isCheckpoint)
        {
            return false;
        }
        if (item.isFlosh)
        {
            return true;
        }
        // Don't do anything if it's a default item

        if (!item.isDefaultItem)
        {
            
            // Check if out of space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            if (diamonds >= item.cost)
            {
                RemoveDiamond(item.cost);
                items.Add(item);    // Add item to list

                return true;
            }
            // Trigger callback
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return false;
    }

    // Remove an item
    public void Remove(Item item)
    {
        items.Remove(item);     // Remove item from list

        // Trigger callback
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
