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

}
