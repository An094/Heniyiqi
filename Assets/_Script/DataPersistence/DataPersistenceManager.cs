using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    [Header("Auto Saving Configuration")]
    [SerializeField] private float autoSaveTimeSeconds = 60f;

    private GameData GameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler FileDataHandler;

    private Coroutine autoSaveCoroutine;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Initialize(string hashName, bool isMale)
    {
        FileDataHandler = new FileDataHandler(Application.persistentDataPath, hashName, useEncryption);

        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

        // start up the auto saving coroutine
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void DeleteProfileData(string profileId)
    {
        // delete the data for this profile id
        FileDataHandler.Delete(profileId);
        // initialize the selected profile id
        // reload the game so that our data matches the newly selected profile id
        LoadGame();
    }

    public void NewGame()
    {
        this.GameData = new GameData();
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        GameData = FileDataHandler.Load("Profiles");

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (GameData == null)
        {
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (GameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(GameData);
        }
    }

    public void SaveGame()
    {
        // if we don't have any data to save, log a warning here
        if (GameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(GameData);
        }

        // save that data to a file using the data handler
        FileDataHandler.Save(GameData, "Profiles");
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return GameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return FileDataHandler.LoadAllProfiles();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
}