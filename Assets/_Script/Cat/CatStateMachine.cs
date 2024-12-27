using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CatState
{
    protected readonly CatStateMachine StateMachine;
    protected readonly Cat Cat;
    protected Animator Animator;
    protected string AnimationStr;
    public CatState(CatStateMachine stateMachine, Cat cat, Animator animator, string animation)
    {
        StateMachine = stateMachine;
        Cat = cat;
        Animator = animator;
        AnimationStr = animation;
    }

    public virtual void OnEnter()
    {
        Animator.SetBool(AnimationStr, true);
    }

    public virtual void OnExit()
    {
        Animator.SetBool(AnimationStr, false);
    }

    public virtual void Update()
    {

    }

}

public class CatStateMachine
{
    public CatState CurrentState { get; set; }

    public void Initialize(CatState state)
    {
        CurrentState = state;
        CurrentState.OnEnter();
    }

    public void ChangeState(CatState state)
    {
        CurrentState.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}

public class CatInBoxState : CatState
{
    public CatInBoxState(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Cat.CatState = ECatState.InBox;
    }

    public override void Update()
    {
        base.Update();
    }
}

public class CatDanceState : CatState
{
    public CatDanceState(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Cat.CatState = ECatState.Dance;
    }
}

public class CatEatState : CatState
{
    private CountdownTimer EatingTimer;
    public CatEatState(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation)
    {
        EatingTimer = new CountdownTimer(3f);
        EatingTimer.OnTimerStop += () =>
        {
            StateMachine.ChangeState(Cat.CatDanceState);
        };
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Cat.CatState = ECatState.Eating;
        EatingTimer.Start();
    }

    public override void Update()
    {
        base.Update();
        EatingTimer.Tick(Time.deltaTime);
    }
}