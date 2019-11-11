using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasItemData
{
    public static Item CreateItem(int itemID)
    {
        int _amount = 0;
        int _value = 0;
        int _damage = 0;
        int _armour = 0;
        int _heal = 0;
        int _durability = 0;
        int _mana = 0;
        string _icon = "";
        string _mesh = "";
        bool _stackable;
        bool _isStacked = false;
        string _name = "";
        string _description = "";
        string _rarity = "";
        CanvasItemType _canvasType;
        switch (itemID)
        {
            #region Ingredient 0 - 10
            case 0:
                _name = "Apple";
                _description = "A fruit that heals or fuels your body";
                _amount = 1;
                _value = 1;
                _canvasType = CanvasItemType.Ingredients;
                _icon = "Ingredients/Apple";
                _mesh = "Ingredients/Apple";
                _damage = 0;
                _armour = 0;
                _heal = 2;
                _rarity = "common";
                _stackable = true;
                _isStacked = false;
                break;
            #endregion
            #region Ammo 11 - 20
            case 11:
                _name = "Pistol Bullet";
                _description = "A small bullet that is cheap but packs a punch";
                _amount = 1;
                _value = 1;
                _canvasType = CanvasItemType.Ammo;
                _icon = "Ammo/Pistol";
                _mesh = "Ammo/Pistol";
                _damage = 0;
                _armour = 0;
                _heal = 2;
                _rarity = "common";
                _stackable = true;
                _isStacked = false;
                break;
            #endregion
            default:
                _name = "default";
                _description = "default item";
                _amount = 1;
                _value = 1;
                _canvasType = CanvasItemType.Ingredients;
                _icon = "Ingredients/Apple";
                _mesh = "Ingredients/Apple";
                _damage = 0;
                _armour = 0;
                _heal = 0;
                _rarity = "common";
                _stackable = true;
                _isStacked = false;
                break;
        }

        Item temp = new Item
        {
            ID = itemID,
            Value = _value,
            Amount = _amount,
            Icon = Resources.Load("Icons/" + _icon) as Texture2D,
            ItemMesh = Resources.Load("Meshes/" + _mesh) as GameObject,
            Name = _name,
            Description = _description,
            Stackable = _stackable       
    };
        return temp;
    }
}

