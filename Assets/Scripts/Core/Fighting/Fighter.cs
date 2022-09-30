using System.Collections.Generic;
using UnityEngine;
public class Fighter : MonoBehaviour
{
    [SerializeField]
    internal FighterData _fighterData;
    private StateMachine _stateMachine = new StateMachine();
    private IdleState _idleState;
    private RunningState _runningState;
    private AttackState _attackState;
    private Animator _fighterAnimator;
    [SerializeField]
    private Opponent _fighterOpponent = new Opponent();

    private void Start()
    {
        _fighterAnimator = GetComponent<Animator>();
        _idleState = new IdleState();
        FightingManager.OnFightersWasSortedByDistance += () =>
        { //TODO
            FindOpponentFighter();
            _runningState = new RunningState(_fighterAnimator, _fighterOpponent.OpponentFighter, transform);
            _runningState.OnExitFromState += () =>
            {
                _attackState = new AttackState(_fighterAnimator, this, _fighterOpponent.OpponentFighter, _fighterData);
                _stateMachine.ChangeState(_attackState);
            };
            _stateMachine.Initialize(_runningState);
        };

        Services.Singletone.FightingManager.AddFighterInList(this);
    }

    private void Update()
    {
        if (_stateMachine.CurrentState != null)
            _stateMachine.CurrentState.Update();
    }

    internal void SetOpponent(Fighter fighterOpponent)
    {
        _fighterOpponent.OpponentFighter = fighterOpponent;
    }

    internal void RemoveOpponent()
    {
        _fighterOpponent.OpponentFighter = null;
    }

    internal void SetConditionOfFighter(FighterConditions condition) 
    {
        _stateMachine.ChangeState(_idleState);

        switch ((int)condition) 
        {
            case 0:
                _fighterAnimator.SetTrigger("Win");
                break;

            case 1:
                _fighterAnimator.SetTrigger("Lose");
                break;
        }
    }

    private void FindOpponentFighter()
    {
        List<Fighter> sortedFighters = Services.Singletone.FightingManager.GetSortedFightersList();

        foreach (var fighterOpponent in sortedFighters)
        {
            if (fighterOpponent == this)
            {
                print("Equals");
                if (sortedFighters.IndexOf(fighterOpponent) == 0) _fighterOpponent.OpponentFighter = sortedFighters[1];

                if (sortedFighters.IndexOf(fighterOpponent) % 2 == 0)
                {
                    _fighterOpponent.OpponentFighter = sortedFighters[sortedFighters.IndexOf(fighterOpponent) + 1];
                }
                else
                {
                    _fighterOpponent.OpponentFighter = sortedFighters[sortedFighters.IndexOf(fighterOpponent) - 1];
                }
            }
        }
    }

    internal enum FighterConditions 
    {
     Win,
     Lose
    }
}
