using System;
using UnityEngine;

public abstract class CatState
{
    protected readonly CatStateMachine StateMachine;
    protected readonly Cat Cat;
    protected Animator Animator;
    protected string AnimationStr;
    protected CountdownTimer Timer;
    protected bool IsAutoChangeState;
    public CatState(CatStateMachine stateMachine, Cat cat, Animator animator, string animation, bool isAutoChangeState = true)
    {
        StateMachine = stateMachine;
        Cat = cat;
        Animator = animator;
        AnimationStr = animation;
        IsAutoChangeState = isAutoChangeState;
        if(IsAutoChangeState)
        {
            Timer = new CountdownTimer(10f);
            Timer.OnTimerStop += () =>
            {
                ChangeRandomState();
            };
        }
    }

    protected void ChangeRandomState()
    {
        int randomValue = UnityEngine.Random.Range(2, 5);
        switch (randomValue)
        {
            //case 0:
            //    {
            //        StateMachine.ChangeState(Cat.CryState);
            //        break;
            //    }
            //case 1:
            //    {
            //        StateMachine.ChangeState(Cat.CatDanceState);
            //        break;
            //    }
            case 2:
                {
                    StateMachine.ChangeState(Cat.SleepState);
                    break;
                }
            case 3:
                {
                    StateMachine.ChangeState(Cat.WaitingState);
                    break;
                }
            case 4:
                {
                    StateMachine.ChangeState(Cat.LayDownState);
                    break;
                }
        }
    }

    public virtual void OnEnter()
    {
        Animator.SetBool(AnimationStr, true);
        DataPersistenceManager.instance.SaveGame();
        if(IsAutoChangeState)
        {
            Timer.Start();
        }
    }

    public virtual void OnExit()
    {
        Animator.SetBool(AnimationStr, false);
        if(IsAutoChangeState)
        {
            Timer.Pause();
        }
    }

    public virtual void Update()
    {
        if(IsAutoChangeState)
        {
            Timer.Tick(Time.deltaTime);
        }
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
        if (state == CurrentState) return;
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
    public CatInBoxState(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation, false)
    {
    }

    public override void OnEnter()
    {
        GameManager.instance.CatState = ECatState.InBox;

        base.OnEnter();

        //Cat.CatState = ECatState.InBox;
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
        GameManager.instance.CatState = ECatState.Dance;
        base.OnEnter();

        //Cat.CatState = ECatState.Dance;
    }
}

public class CatWaiting : CatState
{
    public CatWaiting(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation)
    {
    }

    public override void OnEnter()
    {
        GameManager.instance.CatState = ECatState.Waiting;
        base.OnEnter();

        //Cat.CatState = ECatState.Dance;
    }
}

public class CatLayDown : CatState
{
    public CatLayDown(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation)
    {
    }

    public override void OnEnter()
    {
        GameManager.instance.CatState = ECatState.LayDown;
        base.OnEnter();

        //Cat.CatState = ECatState.Dance;
    }
}

public class CatCry : CatState
{
    public CatCry(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation)
    {
    }

    public override void OnEnter()
    {
        GameManager.instance.CatState = ECatState.Cry;
        base.OnEnter();

        //Cat.CatState = ECatState.Dance;
    }
}

public class CatSleep : CatState
{
    public CatSleep(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation)
    {
    }

    public override void OnEnter()
    {
        GameManager.instance.CatState = ECatState.Cry;
        base.OnEnter();

        //Cat.CatState = ECatState.Dance;
    }
}

public class CatEatState : CatState
{
    private CountdownTimer EatingTimer;
    public CatEatState(CatStateMachine stateMachine, Cat cat, Animator animator, string animation) : base(stateMachine, cat, animator, animation, false)
    {
        EatingTimer = new CountdownTimer(3f);
        EatingTimer.OnTimerStop += () =>
        {
            //StateMachine.ChangeState(Cat.CatDanceState);
            ChangeRandomState();
        };
    }

    public override void OnEnter()
    {
        GameManager.instance.CatState = ECatState.Eating;
        base.OnEnter();

        //Cat.CatState = ECatState.Eating;
        EatingTimer.Start();
    }

    public override void Update()
    {
        base.Update();
        EatingTimer.Tick(Time.deltaTime);
    }
}