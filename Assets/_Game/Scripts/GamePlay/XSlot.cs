using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tretimi;
using TMPro;
using System;
public class XSlot : MonoBehaviour
{
    [SerializeField] private float _coeficcient;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TextMeshPro _text;
    public Action<float> OnBallFallToXSlot;
    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private async void Awake()
    {
        _text.text = $"{_coeficcient}x";

        if (_coeficcient < 0.5f)
            _spriteRenderer.sprite = await Assets.GetAsset<Sprite>("xSlot0");

        if (_coeficcient >= 0.5f && _coeficcient < 1)
            _spriteRenderer.sprite = await Assets.GetAsset<Sprite>("xSlot1");

        if (_coeficcient >= 1 && _coeficcient < 1.5f)
            _spriteRenderer.sprite = await Assets.GetAsset<Sprite>("xSlot2");

        if (_coeficcient >= 1.5f && _coeficcient < 2)
            _spriteRenderer.sprite = await Assets.GetAsset<Sprite>("xSlot3");

        if (_coeficcient >= 2f)
            _spriteRenderer.sprite = await Assets.GetAsset<Sprite>("xSlot4");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // int savingPercent = 0;
        Debug.Log($"Trigger enter {other.gameObject.name}");
        // if (other.gameObject.TryGetComponent<Ball>(out Ball ball))
        // {
        //     savingPercent = ball.SavingPercent;
        // };

        OnBallFallToXSlot?.Invoke(_coeficcient);
        Destroy(other.gameObject);
        AudioSystem.Instance.BallFallToSlot();
    }
}
