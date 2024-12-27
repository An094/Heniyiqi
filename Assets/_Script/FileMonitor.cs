using System;
using System.IO;
using UnityEngine;

public class FileMonitor : MonoBehaviour
{
    public static string FilePath = "";
    public static event Action FileModified;
    public static DateTime LastWriteTime;
    public static bool IsInitalized = false;

    public static void Initialize(string filePath)
    {
        IsInitalized = true;
        FilePath = filePath;
        LastWriteTime = File.GetLastWriteTime(FilePath);
    }

    void Update()
    {
        if (IsInitalized)
        {
            DateTime currentWriteTime = File.GetLastWriteTime(FilePath);

            if (currentWriteTime != LastWriteTime)
            {
                Debug.Log("File has been modified by another instance!");
                LastWriteTime = currentWriteTime;
                FileModified?.Invoke();
            }
        }

    }
}
