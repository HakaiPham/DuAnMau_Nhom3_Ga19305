using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

    public class Data 
    {
        public int score;
    }

    public class DataManager 
    {
        const string fileName = "data.txt";
        public static bool SaveData(Data data)
        {
            try
            {
                var json = JsonUtility.ToJson(data);
                var fileStream = new FileStream(fileName, FileMode.Create);
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(json);
                }
                return true;
            }
            catch(Exception e) 
            {
                Debug.Log($"Save data error: {e.Message}") ;
            }
            return false;
        }
        public static Data ReadData()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    // Open File
                    var fileStrem = new FileStream(fileName, FileMode.Open);
                    using ( var reader = new StreamReader(fileStrem))
                    {
                        //Read data file
                        var json = reader.ReadToEnd();
                        //Chuyen du lieu tu json sang class
                        var data = JsonUtility.FromJson<Data>(json);
                        return data;
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("Error loading File: " + e.Message);
            }
            return null;
        }
    }
