using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private Transform _doorLeft;
    [SerializeField] private Transform _doorRight;
    [SerializeField] private float _distanceToOpen;
    [SerializeField] private float _duration;
    [SerializeField] private bool _isOpen;

    [SerializeField] private Ease _ease;

    private void Update()
    {
        //si on arrive à un nouveau palier
        if(_isOpen)
        {
            OpenTheDoor();
        }
        //si tout les ennemies sont entrés;
        else
        {
            CloseTheDoor();
        }
    }

    private void OpenTheDoor()
    {
        _doorLeft.DOLocalMoveX(_distanceToOpen, _duration).SetEase(_ease);
        _doorRight.DOLocalMoveX(-_distanceToOpen, _duration).SetEase(_ease);
    }

    private void CloseTheDoor()
    {
        _doorLeft.DOLocalMoveX(_originPosition, _duration).SetEase(_ease);
        _doorRight.DOLocalMoveX(-_originPosition, _duration).SetEase(_ease);
    }

    private float _originPosition = 0;
}
