using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory_UI : MonoBehaviour
{
    public GameObject player;
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InventoryUI(BaseEventData _data)
    {
        var data = _data as PointerEventData;
        Vector3 position = data.position;

        int index = 0;
        for(;index<5;index++)
        {
            if (inventory.transform.localScale.x / 5 * index + inventory.transform.position.x <= position.x &&
                position.x <= inventory.transform.localScale.x / 5 * (index + 1) + inventory.transform.position.x)
                break;
        }

        player.SendMessage("SelectItem", index);
    }
}
