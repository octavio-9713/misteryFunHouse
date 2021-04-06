using System;
using UnityEngine;

[Serializable]
public class State
{
    public enum STATE {IDLE, PERSUING, FLEEING, ATTACK, WANDERING};
    public enum EVENT {ENTER, UPDATE, EXIT};

    public STATE name;
    public EVENT _stage;
    
    protected Enemy _enemy;
    protected Player _player;
    protected Animator _animator;

    protected State nextState;

    public State(Enemy enemy, Player player, Animator animator)
    {
        _enemy = enemy;
        _player = player;
        _animator = animator;
    }

    public virtual void Enter() { _stage = EVENT.UPDATE; }
    public virtual void Update() { _stage = EVENT.UPDATE; }
    public virtual void Exit() { _stage = EVENT.EXIT; }

    public State Process()
    {
        if (_stage == EVENT.ENTER)
            Enter();

        if (_stage == EVENT.UPDATE)
            Update();

        if (_stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }
}
