using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RunningState : State
{
    private Animator _fighterAnimator;
    private Fighter _opponentFighter;
    private Transform _fighterTransform;
    internal Action OnExitFromState;
    private bool isEntered { get; set; }

    internal RunningState(Animator fighterAnimator, Fighter opponentFighter, Transform fighterTransform)
    {
        _fighterAnimator = fighterAnimator;
        _opponentFighter = opponentFighter;
        _fighterTransform = fighterTransform;
    }

    public override void Enter()
    {
        base.Enter();
        _fighterAnimator.SetBool("Walk Forward", true);
        // movement and rotation logic in opponent side 
        _fighterTransform.DOLookAt(_opponentFighter.transform.position, 1f).OnComplete(() =>
        {
            isEntered = true;
        });

    }

    public override void Exit()
    {
        base.Exit();
        _fighterAnimator.SetBool("Walk Forward", false);
    }

    public override void Update()
    {
        base.Update();
        if (!isEntered) return;
        float distance = Vector3.Distance(_fighterTransform.position, _opponentFighter.transform.position);
        if (distance > 1.5f)
        {
            _fighterTransform.position = Vector3.MoveTowards(_fighterTransform.position, _opponentFighter.transform.position + _opponentFighter.transform.TransformDirection(Vector3.forward), Time.deltaTime);
        }
        else
        {
            isEntered = false;
            OnExitFromState?.Invoke();
        }
    }

}


