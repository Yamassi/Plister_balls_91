using System.Collections;
using UnityEngine;
using Tretimi;
public class Bootloader : MonoBehaviour
{
    [SerializeField] private UIHolder _uIHolder;
    [SerializeField] private GamePlay _gamePlay;
    [SerializeField] private ItemsData _itemsData;
    [SerializeField] private InAppCallbacks _iAP;
    private Game _game;
    private void Start()
    {
        _game = new Game(_uIHolder, _gamePlay, _itemsData);
        _game.Init();
        _iAP.Init(_game);
    }
    private void OnApplicationFocus(bool isFocus)
    {
        Debug.Log($"Application Pause {isFocus}");
        if (!isFocus)
            DataProvider.SaveDataJSON(_game.GetData());
    }
    private void OnApplicationQuit()
    {
        DataProvider.SaveDataJSON(_game.GetData());
    }
}
