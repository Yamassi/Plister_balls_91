using System;
using UnityEngine;
using UnityEngine.UI;

public class URLButton : MonoBehaviour
{
    [SerializeField] private String URL;
    private Button _button;
    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        _button.onClick.AddListener(GoToLink);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(GoToLink);
    }
    private void GoToLink()
    {
        Application.OpenURL(URL);
    }
}
