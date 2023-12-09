using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    private void Awake()
    {
        float randomX = Random.Range(-0.06f, 0.06f);
        if (randomX == 0)
            randomX = Random.Range(-0.06f, 0.06f);

        transform.DOLocalMoveX(randomX, 0.1f);
    }
}
