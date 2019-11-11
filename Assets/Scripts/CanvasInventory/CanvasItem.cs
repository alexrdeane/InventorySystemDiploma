using UnityEngine;

public class CanvasItem
{
    #region Variables
    private int _id;
    private string _name;
    private string _description;
    private int _amount;
    private int _value;
    private CanvasItemType _canvasType;
    private Texture2D _icon;
    private GameObject _mesh;
    private bool _stackable;
    private bool _isStacked;
    #endregion
    #region Public Properties
    public int ID
    {
        get { return _id; }//read
        set { _id = value; }//write
    }
    public string Name
    {
        get { return _name; }//read
        set { _name = value; }//write
    }
    public string Description
    {
        get { return _description; }//read
        set { _description = value; }//write
    }
    public int Value
    {
        get { return _value; }//read
        set { _value = value; }//write
    }
    public int Amount
    {
        get { return _amount; }//read
        set { _amount = value; }//write
    }
    public GameObject ItemMesh
    {
        get { return _mesh; }
        set { _mesh = value; }

    }
    public bool Stackable
    {
        get { return _stackable; }//read
        set { _stackable = value; }//write
    }
    public bool IsStacked
    {
        get { return _isStacked ; }//read
        set { _isStacked = value; }//write
    }
    public Texture2D Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }
    public CanvasItemType Type
    {
        get { return _canvasType; }
        set { _canvasType = value; }
        #endregion
    }
}
public enum CanvasItemType
{
    All,
    Ingredients,
    Ammo,
    NumberOfTypes
}
