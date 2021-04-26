using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PullyAnimation : MonoBehaviour
{
    [Header("Parameter Rotate")]
    [SerializeField] float _rotateTime = 0.5f;
    //[SerializeField] float _delay = 1f;
    [SerializeField] private Vector3 _rotation = new Vector3(0, 0, 1);

    [Header("DoTween")]
    [SerializeField] RotateMode _rotateMode;
    //[SerializeField] Ease _ease;

    [Header("Target")]
    [SerializeField] ElevatorController _elevator;

    public bool _startPully;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        //si l'ascenseur descend
        if (_elevator._isGoingDown)// à remplacer avec event
        {
            ElevatorGoesDown();
        }
        //else
        //{
        //    ElevatorStopped();
        //}
    }

    private Tween ElevatorGoesDown()
    {
        Sequence _sequence = DOTween.Sequence();
        _sequence.Join(_transform.DORotate(_rotation, _rotateTime, _rotateMode)/*.SetDelay(_delay).SetEase(_ease).SetLoops(-1)*/);
        return _sequence;
        //_transform.DORotate(_rotation, _rotateTime, _rotateMode).SetDelay(_delay).SetEase(_ease).SetLoops(-1);
        //Debug.Log("vers Satan, toute!");
    }

    //private void ElevatorStopped()
    //{
    //    Sequence _sequence = DOTween.Sequence();
    //    _sequence.Append(_transform.DORotate(Vector3.zero, _rotateTime, _rotateMode).SetDelay(3).SetEase(_ease));

    //    //_transform.DORotate(Vector3.zero, _rotateTime, _rotateMode).SetDelay(_delay).SetEase(_ease);
    //    Debug.Log("on s'arrete");
    //}

    private Transform _transform;

    private bool StartPully { get => _startPully; set => _startPully = value; }
}
