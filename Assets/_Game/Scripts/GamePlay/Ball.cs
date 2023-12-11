using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _rigidbody2D.simulated = false;
    }
    public void Launch()
    {
        _rigidbody2D.simulated = true;
        float randomX = Random.Range(-0.05f, 0.05f);
        if (randomX == 0)
            randomX = Random.Range(-0.05f, 0.05f);

        transform.DOLocalMoveX(randomX, 0.05f);

        AudioSystem.Instance.BallLaunch();
    }
}
