using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigureSet : MonoBehaviour
{
    public Image PreviewBackground, PreviewMap, PreviewBall;
    public SelectButtonText Color, Ball, Map;
    public SelectButton PrevButton, NextButton;
    public TextMeshProUGUI CurrentSetName;
    public Button SaveButton;
    public Action OnColorsSelect, OnBallsSelect, OnMapsSelect, OnPrevSelect, OnNextSelect;
    public Transform SetItemsPoint;
    public List<ConfigureSetItem> ConfigureSetItems;
    public List<ConfigureSetItem> AvailableSetItems;
    [SerializeField] private ConfigureSetItem _configureSetItemPrefab;
    [SerializeField] private HorizontalLayoutGroup _hLG;
    private static Vector2 _stepSize = new Vector2(2.13f, 0);
    private int _currentItem;
    private int _scrollStartIndex;
    private void OnEnable()
    {
        Color.Button.onClick.AddListener(ColorSelect);
        Ball.Button.onClick.AddListener(BallSelect);
        Map.Button.onClick.AddListener(MapSelect);
        PrevButton.Button.onClick.AddListener(PrevSelect);
        NextButton.Button.onClick.AddListener(NextSelect);
    }

    private void OnDisable()
    {
        Color.Button.onClick.RemoveListener(ColorSelect);
        Ball.Button.onClick.RemoveListener(BallSelect);
        Map.Button.onClick.RemoveListener(MapSelect);
        PrevButton.Button.onClick.RemoveListener(PrevSelect);
        NextButton.Button.onClick.RemoveListener(NextSelect);
    }

    public void Init()
    {
        Color.Activate();
        Ball.Deactivate();
        Map.Deactivate();
    }
    public void SetCurrentScrollItem(int currentItem)
    {
        _scrollStartIndex = (ConfigureSetItems.Count <= 4) ? 0 : (ConfigureSetItems.Count <= 6) ? -1 : (ConfigureSetItems.Count <= 8) ? -2 : -3;
        _currentItem = currentItem;

        int width = 430;
        int center;
        if (ConfigureSetItems.Count % 2 == 0)
            center = 225;
        else
            center = 0;

        Debug.Log($"Current Item {currentItem}; Num is {ConfigureSetItems.Count % 2}");

        SetItemsPoint.localPosition = new Vector2(
            center - ((_currentItem + _scrollStartIndex) * (width + _hLG.spacing)), 0);

        if (_currentItem + _scrollStartIndex == _scrollStartIndex)
        {
            PrevButton.Button.interactable = false;
            PrevButton.Deactivate();
        }

        if (_currentItem == ConfigureSetItems.Count - 3)
        {
            NextButton.Button.interactable = false;
            NextButton.Deactivate();
        }

        if (_currentItem < ConfigureSetItems.Count - 3)
        {
            NextButton.Activate();
            NextButton.Button.interactable = true;
        }

        if (_currentItem + _scrollStartIndex > _scrollStartIndex)
        {
            PrevButton.Activate();
            PrevButton.Button.interactable = true;
        }

    }
    public void ScrollItemsMoveLeft()
    {
        OnPrevSelect?.Invoke();

        int width = 430;
        int center;
        if (ConfigureSetItems.Count % 2 == 0)
            center = 225;
        else
            center = 0;

        _currentItem--;
        Debug.Log($"Current Index {_currentItem}; Num is {ConfigureSetItems.Count % 2}");

        SetItemsPoint.localPosition = new Vector2(center - ((_currentItem + _scrollStartIndex) * (width + _hLG.spacing)), 0);

        if (_currentItem + _scrollStartIndex == _scrollStartIndex)
        {
            PrevButton.Deactivate();
            PrevButton.Button.interactable = false;
        }


        if (_currentItem < ConfigureSetItems.Count - 3)
        {
            NextButton.Activate();
            NextButton.Button.interactable = true;
        }

    }
    public void ScrollItemsMoveRight()
    {
        OnNextSelect?.Invoke();

        int width = 430;
        int center;
        if (ConfigureSetItems.Count % 2 == 0)
            center = 225;
        else
            center = 0;

        _currentItem++;
        Debug.Log($"Current Index {_currentItem}; Num is {ConfigureSetItems.Count % 2}");

        SetItemsPoint.localPosition = new Vector2(center - ((_currentItem + _scrollStartIndex) * (width + _hLG.spacing)), 0);

        if (_currentItem == ConfigureSetItems.Count - 3)
        {
            NextButton.Button.interactable = false;
            NextButton.Deactivate();
        }


        if (_currentItem + _scrollStartIndex > _scrollStartIndex)
        {
            PrevButton.Activate();
            PrevButton.Button.interactable = true;
        }

    }
    public ConfigureSetItem CreateConfigureSetItem()
    {
        return Instantiate(_configureSetItemPrefab, SetItemsPoint);
    }

    public void ClearConfigureSetItems()
    {
        for (int i = 0; i < ConfigureSetItems.Count; i++)
        {
            Destroy(ConfigureSetItems[i].gameObject);
        }
        for (int i = 0; i < AvailableSetItems.Count; i++)
        {
            Destroy(AvailableSetItems[i].gameObject);
        }

        ConfigureSetItems.Clear();
        AvailableSetItems.Clear();
    }

    private void ColorSelect()
    {
        Color.Activate();
        Ball.Deactivate();
        Map.Deactivate();

        OnColorsSelect?.Invoke();
    }
    private void BallSelect()
    {
        Color.Deactivate();
        Ball.Activate();
        Map.Deactivate();

        OnBallsSelect?.Invoke();
    }
    private void MapSelect()
    {
        Color.Deactivate();
        Ball.Deactivate();
        Map.Activate();

        OnMapsSelect?.Invoke();
    }
    private void PrevSelect()
    {
        Debug.Log("Prev Select");
        ScrollItemsMoveLeft();
    }

    private void NextSelect()
    {
        Debug.Log("Next Select");
        ScrollItemsMoveRight();
    }
}
