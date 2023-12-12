using System.Collections;
using UnityEngine;
using Tretimi;
public class Bootloader : MonoBehaviour
{
    [SerializeField] private UIHolder _uIHolder;
    [SerializeField] private GamePlay _gamePlay;
    [SerializeField] private ItemsData _itemsData;
    private Game _game;
    private void Start()
    {
        _game = new Game(_uIHolder, _gamePlay, _itemsData);
        _game.InitAsync();
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Application Quit");
        DataProvider.SaveDataJSON(_game.Data);
    }
}
