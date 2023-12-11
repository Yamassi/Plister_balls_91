using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigureSetItem : MonoBehaviour
{
    public int ID;
    [SerializeField] private Image _colorImage, _ballImage, _mapImage;
    [SerializeField] private GameObject _color, _ball, _map, _emptyBackground;
    public void SetColor(Sprite colorImage)
    {
        _colorImage.sprite = colorImage;

        _color.gameObject.SetActive(true);
        _ball.gameObject.SetActive(false);
        _map.gameObject.SetActive(false);
        _emptyBackground.SetActive(false);
    }
    public void SetBall(Sprite ballImage)
    {
        _ballImage.sprite = ballImage;

        _color.gameObject.SetActive(false);
        _ball.gameObject.SetActive(true);
        _map.gameObject.SetActive(false);
        _emptyBackground.SetActive(false);
    }
    public void SetMap(Sprite mapImage)
    {
        _mapImage.sprite = mapImage;

        _color.gameObject.SetActive(false);
        _ball.gameObject.SetActive(false);
        _map.gameObject.SetActive(true);
        _emptyBackground.SetActive(false);
    }

    public void SetEmpty()
    {
        _color.gameObject.SetActive(false);
        _ball.gameObject.SetActive(false);
        _map.gameObject.SetActive(false);
        _emptyBackground.SetActive(true);
    }
}
