using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour//, IDataPersistence
{
    public CatStateMachine CatStateMachine {  get; set; }
    public CatInBoxState CatInBoxState {  get; set; }
    public CatEatState CatEatState { get; set; }
    public CatDanceState CatDanceState { get; set; }
    public CatCry CryState { get; set; }
    public CatSleep SleepState { get; set; }
    public CatWaiting WaitingState { get; set; }
    public CatLayDown LayDownState { get; set; }

    //public int CurrentHappyPoint { get; set; }

    //public ECatState CatState { get; set; }
    [SerializeField] private Animator Animator;

    // Start is called before the first frame update
    public void Init()
    {
        CatStateMachine = new CatStateMachine();
        CatInBoxState = new CatInBoxState(CatStateMachine, this, Animator, "InBox");
        CatEatState = new CatEatState(CatStateMachine, this, Animator, "Eat");
        CatDanceState = new CatDanceState(CatStateMachine, this, Animator, "Dance");
        CryState = new CatCry(CatStateMachine, this, Animator, "Cry");
        SleepState = new CatSleep(CatStateMachine, this, Animator, "Sleep");
        WaitingState = new CatWaiting(CatStateMachine, this, Animator, "Waiting");
        LayDownState = new CatLayDown(CatStateMachine, this, Animator, "Laydown");
        if(GameManager.instance.GetCurrentQuestionId() == 0)
        {
            CatStateMachine.Initialize(CatInBoxState);
        }
        else
        {
            CatStateMachine.Initialize(WaitingState);
        }

        UpdateState();

        //GameManager.UpdateData += UpdateData;
    }

    private void UpdateData()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        ECatState CatState = GameManager.instance.CatState;

        switch (CatState)
        {
            case ECatState.InBox:
                {
                    CatStateMachine.ChangeState(CatInBoxState);
                    break;
                }
            case ECatState.Eating:
                {
                    CatStateMachine.ChangeState(CatEatState);
                    break;
                }
            case ECatState.Dance:
                {
                    CatStateMachine.ChangeState(CatDanceState);
                    break;
                }
            case ECatState.Cry:
                {
                    CatStateMachine.ChangeState(CryState);
                    break;
                }
            case ECatState.Sleep:
                {
                    CatStateMachine.ChangeState(SleepState);
                    break;
                }
            case ECatState.Waiting:
                {
                    CatStateMachine.ChangeState(WaitingState);
                    break;
                }
            case ECatState.LayDown:
                {
                    CatStateMachine.ChangeState(LayDownState);
                    break;
                }
        }
    }    

    // Update is called once per frame
    void Update()
    {
        CatStateMachine.Update();
    }

    public void Eat()
    {
        GameManager.instance.CurrentHappyPoint += 20;
        CatStateMachine.ChangeState(CatEatState);
    }

    //public void LoadData(GameData data)
    //{
    //    if(data.HappyPoint == 0 && data.CatState == 0)
    //    {
    //        CurrentHappyPoint = 50;
    //    }
    //    else
    //    {
    //        CurrentHappyPoint = data.HappyPoint;
    //    }
    //    //if(gameObject.activeInHierarchy)
    //    {
    //        CatState = data.CatState;

    //        switch (CatState)
    //        {
    //            case ECatState.InBox:
    //                {
    //                    CatStateMachine.ChangeState(CatInBoxState);
    //                    break;
    //                }
    //            case ECatState.Eating:
    //                {
    //                    CatStateMachine.ChangeState(CatEatState);
    //                    break;
    //                }
    //            case ECatState.Dance:
    //                {
    //                    CatStateMachine.ChangeState(CatDanceState);
    //                    break;
    //                }
    //        }
    //    }
    //}

    //public void SaveData(GameData data)
    //{
    //    data.CatState = CatState;
    //    data.HappyPoint = CurrentHappyPoint;
    //}
}
