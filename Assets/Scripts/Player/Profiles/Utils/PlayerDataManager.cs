using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Saves, Loads, and Creates PlayerData.
public class PlayerDataManager
{
    public const string GAME_DATA_FOLDER_NAME = "Game_Data";
    public const string PLAYER_DATA_EXTENSION = "_data.json";

    private static string filePath = Path.Combine(Application.persistentDataPath, GAME_DATA_FOLDER_NAME);
    public static string FilePath {
        get {
            if(!Directory.Exists(filePath)) {
                Directory.CreateDirectory(filePath);
            }
            return filePath;
        }
    }

    public static void SavePlayerData(PlayerData data) {
        string targetPath = Path.Combine(FilePath, data.playerName, data.playerName + PLAYER_DATA_EXTENSION);

        string dataTxt = JsonUtility.ToJson(data);
        File.WriteAllText(targetPath, dataTxt);
    }

    public static void CreatePlayerData(string username) {
        string userFolderPath = Path.Combine(FilePath, username);
        string targetPath = Path.Combine(userFolderPath, username + PLAYER_DATA_EXTENSION);

        PlayerData newPlayer = new PlayerData();
        newPlayer.playerName = username;
        newPlayer.hiScore = 0;

        Directory.CreateDirectory(userFolderPath);

        string dataTxt = JsonUtility.ToJson(newPlayer);
        File.WriteAllText(targetPath, dataTxt);
    }

    public static PlayerData LoadPlayerData(string username) {
        PlayerData loadedData = null;

        string targetPath = Path.Combine(FilePath, username, username + PLAYER_DATA_EXTENSION);
        if(File.Exists(targetPath)) {
            loadedData = new PlayerData();
            string rawData = File.ReadAllText(targetPath);

            loadedData = JsonUtility.FromJson<PlayerData>(rawData);
        }

        return loadedData;
    }

    public static List<PlayerData> LoadAllPlayers() {
        List<PlayerData> playerDataList = new List<PlayerData>();

        //Get all files ending in PLAYER_DATA_EXTENSION
        string[] files = Directory.GetFiles(FilePath, "*" + PLAYER_DATA_EXTENSION, SearchOption.AllDirectories);

        if(files.Length > 0) {
            for(int i = 0; i < files.Length; i++) {
                PlayerData loadedData = new PlayerData();
                string rawData = File.ReadAllText(files[i]);

                loadedData = JsonUtility.FromJson<PlayerData>(rawData);
                playerDataList.Add(loadedData);
            }
        }

        return playerDataList;
    }

    public static bool CheckUsernameExists(string username) {
        string targetPath = Path.Combine(FilePath, username, username + PLAYER_DATA_EXTENSION);
        return File.Exists(targetPath);
    }
}
