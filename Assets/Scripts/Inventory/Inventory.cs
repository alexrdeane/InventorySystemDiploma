using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;


namespace Linear
{
    public class Inventory : MonoBehaviour
    {
        #region Variables
        public List<Item> inv = new List<Item>();//list of items
        public Item selectedItem;
        public bool showInv;

        public Vector2 scr;
        public Vector2 scrollPos;

        public int money;

        public int sortType;
        public Transform dropTransform;
        public GameObject curWeapon;
        public GameObject curHelm;

        [System.Serializable]
        public struct equipment
        {
            public string name;
            public Transform location;
            public GameObject curItem;
        };
        public equipment[] equipmentSlots;
        public GUISkin invSkin;

        public GameObject inventory;

        #endregion

        void Start()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.I))
            {
                inv.Add(ItemData.CreateItem(Random.Range(0, 3)));
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                inv[0].Amount += 1;
                Debug.Log(inv[0].Amount);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                showInv = !showInv;
                if (showInv)
                {
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    return;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
            }
        }

        private void OnGUI()
        {
            if (showInv)
            {
                inventory.SetActive(true);

                //scr.x = Screen.width / 16;
                //scr.y = Screen.height / 9;

                //GUI.Box(new Rect(0, 0, scr.x * 8, Screen.height), "");
                /*
                for (int i = 0; i < (int)ItemType.NumberOfTypes; i++)
                {
                    if (GUI.Button(new Rect(i * scr.x, 0, scr.x, 0.25f * scr.y), ((ItemType)i).ToString()))
                    {
                        sortType = i;
                    }
                }
                Display();
                if (selectedItem != null)
                {
                    GUI.Box(new Rect(4.75f * scr.x, 0.25f * scr.y, 1.5f * scr.x, 1.5f * scr.y), selectedItem.Name);
                    GUI.DrawTexture(new Rect(4.75f * scr.x, 0.55f * scr.y, 1.5f * scr.x, 1.5f * scr.y), selectedItem.Icon);
                    // GUI.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0f);
                    GUI.Box(new Rect(4.2f * scr.x, 2.2f * scr.y, 3 * scr.x, 3 * scr.y), selectedItem.Description);
                    GUI.Button(new Rect(6f * scr.x, 3.15f * scr.y, 1.5f * scr.x, 0.25f * scr.y), "Current Amount: " + selectedItem.Amount);
                    ItemUse(selectedItem.Type);
                }
                else
                {
                    return;
                }
                */
            }
            else
            {
                inventory.SetActive(false);
            }
        }

        void Display()
        {
            if (!(sortType == 0 || sortType == int.MinValue))
            {
                ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), sortType.ToString());
                int a = 0;//amount of type
                int s = 0;//slot position
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {
                        a++;
                    }
                }
                if (a <= 34)
                {
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if (inv[i].Type == type)
                        {
                            if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                            {
                                selectedItem = inv[i];
                            }
                            s++;
                        }
                    }
                }
                else
                {
                    if (a > 0)
                    {
                        scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollPos, new Rect(0, 0, 0, 8.5f * scr.y + ((a - 34) * (0.25f * scr.y))), false, true);
                        for (int i = 0; i < inv.Count; i++)
                        {
                            if (inv[i].Type == type)
                            {
                                if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + s * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                                {
                                    selectedItem = inv[i];
                                }
                                s++;
                            }
                        }
                    }
                    GUI.EndScrollView();
                }
            }
            else
            {
                if (inv.Count <= 34)//if we have 34 or less items in inventory
                {
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                    }
                }
                else//more than 34 items
                {

                    scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y), scrollPos, new Rect(0, 0, 0, 8.5f * scr.y + ((inv.Count - 34) * (0.25f * scr.y))), false, true);
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + i * (0.25f * scr.y), 3 * scr.x, 0.25f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];
                        }
                    }
                    GUI.EndScrollView();
                }
            }
        }

        void ItemUse(ItemType Type)
        {
            switch (Type)
            {
                case ItemType.Ingredients:
                    break;
                case ItemType.Potions:
                    break;
                case ItemType.Scrolls:
                    break;
                case ItemType.Food:
                    break;
                case ItemType.Apparel:
                    break;
                case ItemType.Weapons:

                    if (equipmentSlots[0].curItem == null || selectedItem.Name != equipmentSlots[0].curItem.name)
                    {
                        if (GUI.Button(new Rect(4f * scr.x, 5f * scr.y, 1f * scr.x, .5f * scr.y), "Equip"))
                        {
                            if (equipmentSlots[0].curItem != null)
                            {
                                Destroy(equipmentSlots[0].curItem);
                            }
                            curWeapon = Instantiate(selectedItem.ItemMesh, equipmentSlots[0].location);
                            equipmentSlots[0].curItem = curWeapon;
                            curWeapon.name = selectedItem.Name;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(4f * scr.x, 5f * scr.y, 1f * scr.x, .5f * scr.y), "Unequip"))
                        {
                            if (equipmentSlots[0].curItem != null)
                            {
                                Destroy(equipmentSlots[0].curItem);
                            }
                            Destroy(curWeapon);
                        }
                    }
                    break;
                case ItemType.Resources:
                    break;
                case ItemType.Equipment:
                    break;
                case ItemType.Armour:
                    if (equipmentSlots[1].curItem == null || selectedItem.Name != equipmentSlots[1].curItem.name)
                    {
                        if (GUI.Button(new Rect(4f * scr.x, 5f * scr.y, 1f * scr.x, .5f * scr.y), "Equip"))
                        {
                            if (equipmentSlots[1].curItem != null)
                            {
                                Destroy(equipmentSlots[1].curItem);
                            }
                            curHelm = Instantiate(selectedItem.ItemMesh, equipmentSlots[1].location);
                            equipmentSlots[1].curItem = curHelm;
                            curHelm.name = selectedItem.Name;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(4f * scr.x, 5f * scr.y, 1f * scr.x, .5f * scr.y), "Unequip"))
                        {
                            if (equipmentSlots[1].curItem != null)
                            {
                                Destroy(equipmentSlots[1].curItem);
                            }
                            Destroy(curHelm);
                        }
                    }
                    break;
                case ItemType.QuestItems:
                    break;
                case ItemType.Money:
                    break;
                case ItemType.Junk:
                    break;
                case ItemType.Miscellaneous:
                    break;
                default:
                    break;
            }
            if (GUI.Button(new Rect(6f * scr.x, 8f * scr.y, 1.5f * scr.x, 0.25f * scr.y), "Discard"))
            {
                for (int i = 0; i < equipmentSlots.Length; i++)
                {
                    //check equiped item
                    if (equipmentSlots[i].curItem != null && selectedItem.ItemMesh.name == equipmentSlots[i].curItem.name)
                    {
                        // if deleted
                        Destroy(equipmentSlots[i].curItem);
                    }
                }
                // sawn in front
                GameObject droppedItem = Instantiate(selectedItem.ItemMesh, dropTransform.position, Quaternion.identity);
                droppedItem.name = selectedItem.Name;
                // just in case
                droppedItem.AddComponent<Rigidbody>().useGravity = true;
                //reduce or delete
                if (selectedItem.Amount > 1)
                {
                    selectedItem.Amount--;
                }
                else
                {
                    inv.Remove(selectedItem);
                    selectedItem = null;
                    return;
                }
            }
        }
    }
}