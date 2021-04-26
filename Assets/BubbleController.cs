using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BubbleController : MonoBehaviour
{
    [Header("BubbleVFX")]
    [SerializeField] private SpriteRenderer _bubbleSprite;
    private Sequence _sequence;
    private void Awake()
    {
        _bubbleSprite.color = new Color(1, 1, 1, 0);
        _sequence = DOTween.Sequence();
        _sequence.Append(_bubbleSprite.DOFade(1, 0));
        _sequence.Append(_bubbleSprite.DOFade(0, 2));
        _sequence.SetAutoKill(false);
        _sequence.Pause();
    }

    public void DotweenBlast()
    {
        _sequence.Restart();
        _sequence.Play();
    }
}
