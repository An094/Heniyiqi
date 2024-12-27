using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour, IDataPersistence
{
    public CatStateMachine CatStateMachine {  get; set; }
    public CatInBoxState CatInBoxState {  get; set; }
    public CatEatState CatEatState { get; set; }
    public CatDanceState CatDanceState { get; set; }

    public ECatState CatState { get; set; }
    [SerializeField] private Animator Animator;
    // Start is called before the first frame update
    void OnEnable()
    {
        CatStateMachine = new CatStateMachine();
        CatInBoxState = new CatInBoxState(CatStateMachine, this, Animator, "InBox");
        CatEatState = new CatEatState(CatStateMachine, this, Animator, "Eat");
        CatDanceState = new CatDanceState(CatStateMachine, this, Animator, "Dance");
        CatStateMachine.Initialize(CatInBoxState);
        DataPersistenceManager.instance.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        CatStateMachine.Update();
    }

    public void Eat()
    {
        CatState = ECatState.Eating;
        CatStateMachine.ChangeState(CatEatState);
        DataPersistenceManager.instance.SaveGame();
    }

    public void LoadData(GameData data)
    {
        if(gameObject.activeInHierarchy)
        {
            CatState = data.CatState;

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
            }
        }
    }

    public void SaveData(GameData data)
    {
        data.CatState = CatState;
    }
}
