using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootloader : MonoBehaviour
{
    [SerializeField] private UIHolder _uIHolder;
    private Game _game;
    private void Awake()
    {
        _game = new Game(_uIHolder);
        _game.Init();
    }
}
