using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NeedleAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _duration;
    [SerializeField] private bool _isDown;

    [SerializeField] Ease _ease;
    [SerializeField] RotateMode _rotateMode;

    private void Awake()
    {
        _transform = transform;
        _sequence = DOTween.Sequence();
        _sequence.Append(_transform.DORotate(_rotation, _duration, _rotateMode).SetEase(_ease));
        _sequence.SetAutoKill(false);
        _sequence.SetLoops(-1);
        _sequence.Pause();
    }

    private void Update()
    {

        if(GotToTheLowerLevel)
        {
            _sequence.Play();
        }
        else
        {
            _sequence.Pause();
        }
    }

    private Sequence _sequence;
    private Transform _transform;

    //private float[] _levelRotation = {-76.11f, -46.5f, -22f, 8.1f, 36.2f, 67,12f};

    public bool GotToTheLowerLevel { get => _isDown; set => _isDown = value; }
}
