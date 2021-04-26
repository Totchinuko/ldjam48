using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VFXController : MonoBehaviour
{
    [Header("ShootVFX")]
    [SerializeField] private SpriteRenderer _blastSprite;
    private Sequence _sequence;
    private void Awake()
    {
        _blastSprite.color = new Color(1, 1, 1, 0);
        _sequence = DOTween.Sequence();
        _sequence.Append(_blastSprite.DOFade(1, 0));
        _sequence.Append(_blastSprite.DOFade(0, 0.3f));
        _sequence.SetAutoKill(false);
        _sequence.Pause();
    }

    public void DotweenBlast()
    {
        _sequence.Restart();
        _sequence.Play();
    }
}
