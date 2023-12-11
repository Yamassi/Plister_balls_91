using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigureDifficulty : MonoBehaviour
{
    public TextMeshProUGUI BallCostHeader;
    public SelectButton Cost10B, Cost50B, Cost100B, Cost250B;
    public TextMeshProUGUI WeightHeader, WeightInfo;
    public SelectButton Weight10B, Weight50B, Weight100B, Weight250B;
    public TextMeshProUGUI DifficultyHeader;
    public Transform DifficultiesPoint;
    public SelectButton PrevButton, NextButton;
    public TextMeshProUGUI CurrentDifficultyNumber;
    public Button PlayButton;
    public List<DifficultyItem> DifficultyItems;
    public List<DifficultyItem> AvailableDifficulties;
    public Action<int> OnCostSelect, OnWeightSelect, OnDifficultyChange;
    public Action OnPrevSelect, OnNextSelect;
    [SerializeField] private DifficultyItem _difficultyItemPrefab;
    [SerializeField] private HorizontalLayoutGroup _hLG;
    private int _currentItem;
    private int _scrollStartIndex;
    private void OnEnable()
    {
        Cost10B.Button.onClick.AddListener(Cost10Select);
        Cost50B.Button.onClick.AddListener(Cost50Select);
        Cost100B.Button.onClick.AddListener(Cost100Select);
        Cost250B.Button.onClick.AddListener(Cost250Select);

        Weight10B.Button.onClick.AddListener(Weight10Select);
        Weight50B.Button.onClick.AddListener(Weight50Select);
        Weight100B.Button.onClick.AddListener(Weight100Select);
        Weight250B.Button.onClick.AddListener(Weight250Select);

        PrevButton.Button.onClick.AddListener(PrevSelect);
        NextButton.Button.onClick.AddListener(NextSelect);
    }
    private void OnDisable()
    {
        Cost10B.Button.onClick.RemoveListener(Cost10Select);
        Cost50B.Button.onClick.RemoveListener(Cost50Select);
        Cost100B.Button.onClick.RemoveListener(Cost100Select);
        Cost250B.Button.onClick.RemoveListener(Cost250Select);

        Weight10B.Button.onClick.RemoveListener(Weight10Select);
        Weight50B.Button.onClick.RemoveListener(Weight50Select);
        Weight100B.Button.onClick.RemoveListener(Weight100Select);
        Weight250B.Button.onClick.RemoveListener(Weight250Select);

        PrevButton.Button.onClick.RemoveListener(PrevSelect);
        NextButton.Button.onClick.RemoveListener(NextSelect);
    }
    public void SetCost(int cost)
    {
        switch (cost)
        {
            case 10:
                Cost10B.Deactivate();
                Cost50B.Activate();
                Cost100B.Activate();
                Cost250B.Activate();
                break;
            case 50:
                Cost10B.Activate();
                Cost50B.Deactivate();
                Cost100B.Activate();
                Cost250B.Activate();
                break;
            case 100:
                Cost10B.Activate();
                Cost50B.Activate();
                Cost100B.Deactivate();
                Cost250B.Activate();
                break;
            case 250:
                Cost10B.Activate();
                Cost50B.Activate();
                Cost100B.Activate();
                Cost250B.Deactivate();
                break;
        }

    }
    public void SetWeight(int weight)
    {
        switch (weight)
        {
            case 10:
                Weight10B.Deactivate();
                Weight50B.Activate();
                Weight100B.Activate();
                Weight250B.Activate();
                break;
            case 50:
                Weight10B.Activate();
                Weight50B.Deactivate();
                Weight100B.Activate();
                Weight250B.Activate();
                break;
            case 100:
                Weight10B.Activate();
                Weight50B.Activate();
                Weight100B.Deactivate();
                Weight250B.Activate();
                break;
            case 250:
                Weight10B.Activate();
                Weight50B.Activate();
                Weight100B.Activate();
                Weight250B.Deactivate();
                break;
        }
    }
    public void SetCurrentDifficulty(int currentItem)
    {
        _scrollStartIndex = (DifficultyItems.Count <= 4) ? 0 : (DifficultyItems.Count <= 6) ? -1 : (DifficultyItems.Count <= 8) ? -2 : -3;
        _currentItem = currentItem;

        int width = 430;
        int center;
        if (DifficultyItems.Count % 2 == 0)
            center = 225;
        else
            center = 0;

        Debug.Log($"Current Item {currentItem}; Num is {DifficultyItems.Count % 2}");

        DifficultiesPoint.localPosition = new Vector2(
            center - ((_currentItem + _scrollStartIndex) * (width + _hLG.spacing)), 0);

        if (_currentItem + _scrollStartIndex == _scrollStartIndex)
        {
            PrevButton.Button.interactable = false;
            PrevButton.Deactivate();
        }

        if (_currentItem == DifficultyItems.Count - 3)
        {
            NextButton.Button.interactable = false;
            NextButton.Deactivate();
        }

        if (_currentItem < DifficultyItems.Count - 3)
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
    public void DifficultyItemsMoveLeft()
    {
        OnPrevSelect?.Invoke();

        int width = 430;
        int center;
        if (DifficultyItems.Count % 2 == 0)
            center = 225;
        else
            center = 0;

        _currentItem--;
        Debug.Log($"Current Index {_currentItem}; Num is {DifficultyItems.Count % 2}");

        DifficultiesPoint.localPosition = new Vector2(center - ((_currentItem + _scrollStartIndex) * (width + _hLG.spacing)), 0);

        if (_currentItem + _scrollStartIndex == _scrollStartIndex)
        {
            PrevButton.Deactivate();
            PrevButton.Button.interactable = false;
        }


        if (_currentItem < DifficultyItems.Count - 3)
        {
            NextButton.Activate();
            NextButton.Button.interactable = true;
        }
    }
    public void DifficultyItemsMoveRight()
    {
        OnNextSelect?.Invoke();

        int width = 430;
        int center;
        if (DifficultyItems.Count % 2 == 0)
            center = 225;
        else
            center = 0;

        _currentItem++;
        Debug.Log($"Current Index {_currentItem}; Num is {DifficultyItems.Count % 2}");

        DifficultiesPoint.localPosition = new Vector2(center - ((_currentItem + _scrollStartIndex) * (width + _hLG.spacing)), 0);

        if (_currentItem == DifficultyItems.Count - 3)
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

    private void PrevSelect()
    {
        Debug.Log("Prev Select");
        DifficultyItemsMoveLeft();
    }

    private void NextSelect()
    {
        Debug.Log("Next Select");
        DifficultyItemsMoveRight();
    }
    public void ClearConfigureSetItems()
    {
        for (int i = 0; i < DifficultyItems.Count; i++)
        {
            Destroy(DifficultyItems[i].gameObject);
        }

        DifficultyItems.Clear();
    }
    public DifficultyItem CreateSetItem()
    {
        return Instantiate(_difficultyItemPrefab, DifficultiesPoint);
    }
    private void Weight250Select()
    {
        Weight10B.Activate();
        Weight50B.Activate();
        Weight100B.Activate();
        Weight250B.Deactivate();

        OnWeightSelect?.Invoke(250);
    }

    private void Weight100Select()
    {
        Weight10B.Activate();
        Weight50B.Activate();
        Weight100B.Deactivate();
        Weight250B.Activate();

        OnWeightSelect?.Invoke(100);
    }

    private void Weight50Select()
    {
        Weight10B.Activate();
        Weight50B.Deactivate();
        Weight100B.Activate();
        Weight250B.Activate();

        OnWeightSelect?.Invoke(50);
    }

    private void Weight10Select()
    {
        Weight10B.Deactivate();
        Weight50B.Activate();
        Weight100B.Activate();
        Weight250B.Activate();

        OnWeightSelect?.Invoke(10);
    }

    private void Cost250Select()
    {
        Cost10B.Activate();
        Cost50B.Activate();
        Cost100B.Activate();
        Cost250B.Deactivate();

        OnCostSelect?.Invoke(250);
    }

    private void Cost100Select()
    {
        Cost10B.Activate();
        Cost50B.Activate();
        Cost100B.Deactivate();
        Cost250B.Activate();

        OnCostSelect?.Invoke(100);
    }

    private void Cost50Select()
    {
        Cost10B.Activate();
        Cost50B.Deactivate();
        Cost100B.Activate();
        Cost250B.Activate();

        OnCostSelect?.Invoke(50);
    }

    private void Cost10Select()
    {
        Cost10B.Deactivate();
        Cost50B.Activate();
        Cost100B.Activate();
        Cost250B.Activate();

        OnCostSelect?.Invoke(10);
    }
}
