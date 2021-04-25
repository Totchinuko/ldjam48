using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NeedleAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _duration;

    [SerializeField] Ease _ease;
    [SerializeField] RotateMode _rotateMode;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        for (int i = 0; i < _levelRotation.Length; i++)
        {
            _rotation.z = _levelRotation[i];
        }

        if(GotToTheLowerLevel)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        _transform.DOLocalRotate(_rotation, _duration, _rotateMode);
    }

    private Transform _transform;
    private bool _gotToTheLowerLevel;
    private float _rotationZ = -76.11f;

    private float[] _levelRotation = {-76.11f, -46.5f, -22f, 8.1f, 36.2f, 67,12f};

    public bool GotToTheLowerLevel { get => _gotToTheLowerLevel; set => _gotToTheLowerLevel = value; }
}
