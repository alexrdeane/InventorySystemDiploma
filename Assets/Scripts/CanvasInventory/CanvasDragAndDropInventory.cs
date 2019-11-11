using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDragAndDropInventory : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    //bool that activates the visibility of the inventory
    public bool showInv;
    //list of items
    public List<Item> inv = new List<Item>();
    public int slotX, slotY;
    public Rect inventorySize;
    [Header("Dragging")]
    //bool to see if player is dragging an item
    public bool isDragging;
    //last position of the item dragged
    public int draggedFrom;
    //current item being dragged
    public Item draggedItem;
    public GameObject droppedItem;

    [Header("References and Locations")]
    public Vector2 scr;

    public GameObject invMenu;
    #endregion
    #region Clamp to Screen
    //makes sure the inventory window doesnt go outside the border of the screen
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion
    #region Add Item
    //creates item that can be seen in the inventory
    public void AddItem(int itemID)
    {
        for (int i = 0; i < inv.Count; i++)
        {
            if (inv[i].Name == null)
            {
                inv[i] = ItemData.CreateItem(itemID);
                Debug.Log("Add Item: " + inv[i].Name);
                return;
            }
        }
    }
    #endregion
    #region Drop Item
    //if the item is dragged outside of the menu the item will turn into the mesh and fall in the environmemt
    public void DropItem()
    {
        droppedItem = draggedItem.ItemMesh;
        droppedItem = Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        droppedItem.AddComponent<Rigidbody>().useGravity = true;
        droppedItem.name = draggedItem.Name;

        droppedItem = null;
    }
    #endregion
    #region Draw Item
    //displays the icon in the inventory
    void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scr.x * 0.5f, scr.y * 0.5f), draggedItem.Icon);
        }
    }
    #endregion
    #region Toggle Inventory
    //void that toggles inventory visibility when I is pressed
    public void ToggleInv()
    {
        showInv = !showInv;
        if (showInv)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    #endregion
    //allows the player to move the inventory menu around the screen with mouse
    #region Drag Inventory
    void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, scr.y * 0.25f, scr.x * 6, scr.y * 0.5f), "Banner");
        GUI.Box(new Rect(0, scr.y * 4f, scr.x * 6, scr.y * 0.5f), "Gold Display");
        //locations of items in inventory
        #region Nested for loop
        int i = 0;
        Event e = Event.current;
        for (int x = 0; x < slotX; x++)
        {
            for (int y = 0; y < slotY; y++)
            {
                Rect slotLocation = new Rect(scr.x * 0 + x * (scr.x * 0.75f), scr.y * 0.75f + y * (scr.x * 0.65f), scr.x * 0.75f, scr.y * 0.65f);
                GUI.Box(slotLocation, "");
                //if an item is picked up by the cursor the player can move it
                #region Pickup Item
                if (inv[i].Name != null && e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !isDragging && !Input.GetKey(KeyCode.LeftShift))
                {
                    draggedItem = inv[i];
                    inv[i] = new Item();
                    isDragging = true;
                    draggedFrom = i;
                    Debug.Log("Currently dragging your " + draggedItem.Name);
                }
                #endregion
                //if it is dropped on a tile it will swap places with that item
                #region Swap Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name != null)
                {
                    Debug.Log("Swapping your " + draggedItem.Name + "with " + inv[i].Name);
                    inv[draggedFrom] = inv[i];
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isDragging = false;
                }
                #endregion
                //drops item on slot if mouse is released
                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inv[i].Name == null)
                {
                    Debug.Log("Placing your " + inv[i].Name);
                    inv[i] = draggedItem;
                    draggedItem = new Item();
                    isDragging = false;
                }
                #endregion
                #region Return Item

                #endregion
                //displays icon next to mouse
                #region Draw Item Icon
                if (inv[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inv[i].Icon);
                    #region Set ToolTip on Mouse Hover
                    if (slotLocation.Contains(e.mousePosition) && !isDragging && showInv)
                    {

                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #endregion
        /*
        #region Drag Points
        GUI.DragWindow(new Rect(0, 0, scr.x * 6, scr.y * 0.25f));
        GUI.DragWindow(new Rect(0, scr.y * 0.25f, scr.x * 0.25f, scr.y * 3.75f));
        GUI.DragWindow(new Rect(scr.x * 5.5f, scr.y * 0.25f, scr.x * 0.25f, scr.y * 3.75f));
        GUI.DragWindow(new Rect(0, scr.y * 4, scr.x * 6, scr.y * 0.25f));
        #endregion
    */
    }
    #endregion

    #region Start
    private void Start()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;

        inventorySize = new Rect(scr.x, scr.y, scr.x * 6, scr.y * 4.5f);
        for (int i = 0; i < slotX * slotY; i++)
        {
            inv.Add(new Item());
        }

        AddItem(0);
        AddItem(11);
    }
    #endregion
    #region Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInv();

        }
        if (scr.x != Screen.width / 16)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;
            inventorySize = new Rect(scr.x, scr.y, scr.x * 6, scr.y * 4.5f);
        }
    }
    #endregion
    #region OnGUI
    private void OnGUI()
    {
        Event e = Event.current;
        #region Inventory when true
        if (showInv)
        {
            invMenu.SetActive(true);
        }
        #endregion
        #region Drop Item
        if ((e.button == 0 && e.type == EventType.MouseUp && isDragging) || (isDragging && !showInv))
        {
            DropItem();
            Debug.Log("Dropped Item " + draggedItem.Name);
            draggedItem = new Item();
            isDragging = false;
        }
        #endregion
        #region Draw Item On Mouse
        if (isDragging)
        {
            if (draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scr.x * 0.5f, scr.y * 0.5f);
                GUI.Window(72, mouseLocation, DrawItem, "");
            }
        }
        if (showInv == false)
        {
            invMenu.SetActive(false);
        }
        #endregion
    }
    #endregion
}
