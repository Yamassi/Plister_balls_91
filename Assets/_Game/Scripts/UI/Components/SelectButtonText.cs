using TMPro;
using UnityEngine;

public class SelectButtonText : BasicButton
{
    public GameObject ActiveImage, InactiveImage;
    public TextMeshProUGUI Header, Name;

    public void Activate()
    {
        InactiveImage.gameObject.SetActive(true);
        ActiveImage.gameObject.SetActive(false);
    }
    public void Deactivate()
    {
        InactiveImage.gameObject.SetActive(false);
        ActiveImage.gameObject.SetActive(true);
    }
}
