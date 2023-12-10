using UnityEngine;

[CreateAssetMenu(menuName = "ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public int ID;
    public string Name;
    public int Price;

}