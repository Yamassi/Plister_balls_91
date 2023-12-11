using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MySetsState : State
{
    private MySets _mySets;
    public MySetsState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, MySets mySets) : base(stateSwitcher, dataService, topA)
    {
        _mySets = mySets;
    }
    public override void Enter()
    {
        _topA.Coins.gameObject.SetActive(true);
        _topA.BackButton.gameObject.SetActive(true);
        _topA.SettingsButton.gameObject.SetActive(true);
        _mySets.gameObject.SetActive(true);

        _topA.BackButton.onClick.AddListener(BackToMainMenu);

        UpdateMySets();
    }
    public override void Exit()
    {
        _topA.Coins.gameObject.SetActive(false);
        _topA.BackButton.gameObject.SetActive(false);
        _topA.SettingsButton.gameObject.SetActive(false);
        _mySets.gameObject.SetActive(false);

        _topA.BackButton.onClick.RemoveListener(BackToMainMenu);
    }
    private async void UpdateMySets()
    {
        var mySetsData = _dataService.GetData().MySets;

        for (int i = 0; i < _mySets.MySetsItems.Count; i++)
        {
            _mySets.MySetsItems[i].SetEmpty();
            _mySets.MySetsItems[i].Name.text = $"SET {i + 1}";
        }

        for (int i = 0; i < mySetsData.Count; i++)
        {
            Sprite ballImage = await Tretimi.Assets.GetAsset<Sprite>($"Ball{mySetsData[i].ball}");
            Sprite backgroundImage = await Tretimi.Assets.GetAsset<Sprite>($"Background{mySetsData[i].background}");
            Sprite mapImage = await Tretimi.Assets.GetAsset<Sprite>($"Map{mySetsData[i].map}");

            _mySets.MySetsItems[i].SetItem(ballImage, backgroundImage, mapImage);
        }

        for (int i = 0; i < _mySets.MySetsItems.Count; i++)
        {
            if (_mySets.MySetsItems[i].Empty)
                _mySets.MySetsItems[i].OnMySetsItemButtonClicked += GoToAddSet;
            else
                _mySets.MySetsItems[i].OnMySetsItemButtonClicked += GoToConfigureSet;
        }
    }

    private void GoToConfigureSet(int id)
    {
        _stateSwitcher.SwitchState<ConfigureSetState>();
        PlayerPrefs.SetInt("CurrentConfigureSet", id);
    }

    private void GoToAddSet(int id)
    {
        _stateSwitcher.SwitchState<ConfigureSetState>();
        PlayerPrefs.SetInt("CurrentConfigureSet", id);
    }

    private void BackToMainMenu()
    {
        _stateSwitcher.SwitchState<MainMenuState>();
    }
}