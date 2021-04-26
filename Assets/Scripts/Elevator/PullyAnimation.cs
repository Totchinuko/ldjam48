using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PullyAnimation : MonoBehaviour
{
    [Header("Parameter Rotate")]
    [SerializeField] float _rotateTime = 0.5f;
    //[SerializeField] float _delay = 1f;
    [SerializeField] private Vector3 _rotation/* = new Vector3(0, 0, 1)*/;

    [Header("DoTween")]
    [SerializeField] RotateMode _rotateMode;
    [SerializeField] Ease _ease;

    [Header("Target")]
    [SerializeField] ElevatorController _elevator;

    public bool _startPully;

    private void Awake()
    {
        _transform = transform;
        _sequence = DOTween.Sequence();
        _sequence.Append(_transform.DORotate(_rotation, _rotateTime, _rotateMode).SetEase(_ease));
        _sequence.SetAutoKill(false);
        _sequence.SetLoops(-1);
        _sequence.Pause();
    }

    private void Update()
    {
        //si l'ascenseur descend
        if (_elevator._isGoingDown)// à remplacer avec event
        {
            _sequence.Play();
        }
        else
        {
            _sequence.Pause();
        }
    }

    private void ElevatorGoesDown()
    {
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
    private Sequence _sequence;

    private bool StartPully { get => _startPully; set => _startPully = value; }
}
