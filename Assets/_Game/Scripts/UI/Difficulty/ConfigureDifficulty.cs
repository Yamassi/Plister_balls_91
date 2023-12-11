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
    public Action<int> OnCostSelect;
    public Action<int> OnWeightSelect;
    public Action<int> OnDifficultyChange;
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
    public void SetDifficulty(int difficulty)
    {

    }
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
