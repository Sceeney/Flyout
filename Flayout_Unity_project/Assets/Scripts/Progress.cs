using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int Coins;
    public int Cars;
}

public class Progress : MonoBehaviour
{

    public PlayerInfo PlayerInfo;
    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();
    [SerializeField] TextMeshProUGUI _playerInfoText;
    public static Progress Instance;

    private void Awake()
    {
            LoadExtern();
    }


    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
#if UNITY_WEBGL
        SaveExtern(jsonString);
#endif
    }

    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        if (_playerInfoText)
        {
            _playerInfoText.text = PlayerInfo.Coins + "\n" + PlayerInfo.Cars;
        }
    }

}
