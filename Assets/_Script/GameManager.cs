using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject SelectionScreen;
    [SerializeField] private GameObject SignUpScreen;
    [SerializeField] private GameObject EnterCodeScreen;
    [SerializeField] private GameObject MainScreen;

    public bool AmIMale { get; set; }
    public string MyName { get; set; }
    public string MyParterName { get; set; }
    public SerializableDateTime FirstDay { get; set; }
    public SerializableDateTime AccCreationDay { get; set; }

    public static GameManager instance { get; private set; }

    public static event Action UpdateData;

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

    private void OnEnable()
    {
        FileMonitor.FileModified += OnFileChanged;
    }

    private void OnDisable()
    {
        FileMonitor.FileModified -= OnFileChanged;
    }

    private void OnFileChanged()
    {
        DataPersistenceManager.instance.LoadGame();
        UpdateData?.Invoke();
    }

    public void LoadData(GameData data)
    {
        FirstDay = data.FirstDay;
        AccCreationDay = data.AccCreationDay;

        if(MyName != null && MyName.Length > 0)
        {
            MyParterName = AmIMale ? data.Female.Name : data.Male.Name;
        }
    }

    public void SaveData(GameData data)
    {
        if (AmIMale)
        {
            data.Male.Name = MyName;
            data.Female.Name = MyParterName;
        }
        else
        {
            data.Female.Name = MyName;
            data.Male.Name = MyParterName;
        }

        data.FirstDay = FirstDay;
        data.AccCreationDay = AccCreationDay;
    }

    public int GetTogetherDays()
    {
        DateTime now = DateTime.Now;
        DateTime firstDay = FirstDay.ToDateTime();
        TimeSpan duration = now - firstDay;
        return duration.Days;
    }

    // Start is called before the first frame update
    void Start()
    {
        SelectionScreen.SetActive(true);
        SignUpScreen.SetActive(false);
        EnterCodeScreen.SetActive(false);
        MainScreen.SetActive(false);
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
        MainScreen.SetActive(false);
    }

    public void ShowEnterCodeScreen()
    {
        SelectionScreen.SetActive(false);
        SignUpScreen.SetActive(false);
        EnterCodeScreen.SetActive(true);
        MainScreen.SetActive(false);
    }

    public void ShowMainScreen()
    {
        SelectionScreen.SetActive(false);
        SignUpScreen.SetActive(false);
        EnterCodeScreen.SetActive(false);
        MainScreen.SetActive(true);
    }
}
