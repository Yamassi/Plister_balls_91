using UnityEngine;
using UnityEngine.UI;

public class BasicButton : MonoBehaviour
{
    public Button Button;
    private void OnValidate()
    {
        Button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(Click);
    }

    private void Click()
    {
        // AudioSystem.Instance.PlayClick();
    }
}
