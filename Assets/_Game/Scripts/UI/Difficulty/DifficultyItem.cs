using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyItem : MonoBehaviour
{
    public Image MapImage;
    public void SetEmpty()
    {
        MapImage.gameObject.SetActive(false);
    }
}
