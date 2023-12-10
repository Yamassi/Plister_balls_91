using UnityEngine;

public class ConfigureSetState : State
{
    private ConfigureSet _configureSet;
    private int _currentConfigureSet;
    public ConfigureSetState(IStateSwitcher stateSwitcher, IDataService dataService,
    TopA topA, ConfigureSet configureSet) : base(stateSwitcher, dataService, topA)
    {
        _configureSet = configureSet;
    }
    public override void Enter()
    {
        _currentConfigureSet = PlayerPrefs.GetInt("CurrentConfigureSet");

        _topA.Header.HeaderText.text = $"SET {_currentConfigureSet + 1}";
        _topA.Header.gameObject.SetActive(true);
        _configureSet.gameObject.SetActive(true);
    }
    public override void Exit()
    {
        _topA.Header.gameObject.SetActive(false);
        _configureSet.gameObject.SetActive(false);
    }
}