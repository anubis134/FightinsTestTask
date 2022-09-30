using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static Fighter;

public sealed class AttackState : State
{
    internal Action OnAttakedState;
    private Animator _fighterAnimator;
    private Fighter _opponentFighter;
    private Fighter _fighter;
    private FighterData _fighterData;

    internal AttackState(Animator fighterAnimator, Fighter fighter, Fighter opponentFighter, FighterData fighterData)
    {
        _fighterAnimator = fighterAnimator;
        _opponentFighter = opponentFighter;
        _fighterData = fighterData;
        _fighter = fighter;
    }
    public override async void Enter()
    {
        base.Enter();
        await Attack();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    private async Task Attack()
    {
        if (!_opponentFighter.TryGetComponent(out IDamagable damagable) || !_fighter.TryGetComponent(out Heath heath)) return;

        while (damagable.Health > 0 && heath.Health > 0)
        {
            Debug.Log("PUNCH");
            _fighterAnimator.SetTrigger("PunchTrigger");
            damagable.TakeDamage(_fighterData.Damage);
            if (damagable.Health <= 0)
            {
                _opponentFighter.SetConditionOfFighter(FighterConditions.Lose);
                _fighter.SetConditionOfFighter(FighterConditions.Win);
                return;
            }
            else if (heath.Health <= 0) 
            {
                    _fighter.SetConditionOfFighter(FighterConditions.Lose);
                    _opponentFighter.SetConditionOfFighter(FighterConditions.Win);
                    return;
            }
            await Task.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0f, 2f)));

        }

    }

}