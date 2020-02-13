
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public AudioClip audio = null;
    public bool isDefaultItem = false;
    public bool isDiamond = false;
    public bool isWings = false;
    public bool isScuba = false;
    public bool isFood = false;
    public bool isStamina = false;
    public bool isFlosh = false;
    public bool isCheckpoint = false;
    public int cost;

    public virtual void Use()
    {
        Debug.Log("use"+ name);
    }
    public void RemoveFromInventory()
    {
        //Inventory.instance.Remove(this);
    }
}
