using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MySetsItem : MonoBehaviour
{
    public int ID;
    public Image Background, BallImage, MapImage, AddImage;
    public TextMeshProUGUI Header, Name, ButtonText;
    public Button ChangeButton;
    public Action<int> OnMySetsItemButtonClicked;
    public bool Empty { get; private set; }
    private void OnEnable()
    {
        ChangeButton.onClick.AddListener(() => { OnMySetsItemButtonClicked?.Invoke(ID); });
    }
    private void OnDisable()
    {
        ChangeButton.onClick.RemoveListener(() => { OnMySetsItemButtonClicked?.Invoke(ID); });
    }
    public void SetItem(Sprite ballImage, Sprite backroundImage, Sprite mapImage)
    {
        Background.sprite = backroundImage;
        BallImage.sprite = ballImage;
        MapImage.sprite = mapImage;
        BallImage.gameObject.SetActive(true);
        Background.gameObject.SetActive(true);
        MapImage.gameObject.SetActive(true);
        AddImage.gameObject.SetActive(false);
        ButtonText.text = "CHANGE";

        Empty = false;
    }

    public void SetEmpty()
    {
        BallImage.gameObject.SetActive(false);
        Background.gameObject.SetActive(false);
        MapImage.gameObject.SetActive(false);
        AddImage.gameObject.SetActive(true);
        ButtonText.text = "ADD";

        Empty = true;
    }

}
