using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject SelectionScreen;
    [SerializeField] private GameObject SignUpScreen;
    [SerializeField] private GameObject EnterCodeScreen;
    [SerializeField] private GameObject MainScreen;

    public bool IsMale { get; set; }
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one GameManager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadData(GameData data)
    {

    }

    public void SaveData(GameData data)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        SelectionScreen.SetActive(true);
        SignUpScreen.SetActive(false);
        EnterCodeScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSignUpScreen()
    {
        SelectionScreen.SetActive(false);
        SignUpScreen.SetActive(true);
        EnterCodeScreen.SetActive(false);
    }

    public void ShowEnterCodeScreen()
    {
        SelectionScreen.SetActive(false);
        SignUpScreen.SetActive(false);
        EnterCodeScreen.SetActive(true);
    }

    public void ShowMainScreen()
    {
        SelectionScreen.SetActive(false);
        SignUpScreen.SetActive(false);
        EnterCodeScreen.SetActive(false);
        MainScreen.SetActive(true);
    }
}
