using UnityEngine;

public class SelectButton : BasicButton
{
    public GameObject InactiveImage, ActiveImage;
    public void Activate()
    {
        InactiveImage.gameObject.SetActive(false);
        ActiveImage.gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        InactiveImage.gameObject.SetActive(true);
        ActiveImage.gameObject.SetActive(false);
    }
}
