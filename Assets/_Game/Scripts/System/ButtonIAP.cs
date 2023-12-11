using UnityEngine;
using UnityEngine.UI;

public class ButtonIAP : MonoBehaviour
{
    [SerializeField] private Button _button;
    private string _id;
    public void SetPurchaseID(string id)
    {
        _id = id;
    }
    private void OnEnable()
    {
        _button.onClick.AddListener(Action);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(Action);
    }
    private void Action()
    {
        Debug.Log($"Send Purchase {_id}");
        // EventHolder.OnPurchasing?.Invoke(_id);
    }
}
