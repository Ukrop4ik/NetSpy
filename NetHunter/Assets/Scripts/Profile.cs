using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Profile : MonoBehaviour {

    public enum SelectedGameType
    {
        Campain,
        Survive
    }

    public SelectedGameType GameType;

    private static Profile instance;
    public static Profile Instance() { return instance; }

    public int Level;

    public void AddLevel(int value)
    {
        Level += value;
        PlayerPrefs.SetInt("Level", Level);
        PlayerPrefs.Save();
    }

    // Use this for initialization
    void Start () {

        print(PlayerPrefs.GetInt("Level"));

        Level = PlayerPrefs.GetInt("Level");

        if (PlayerPrefs.GetInt("Level") == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
            Level = PlayerPrefs.GetInt("Level");
            print(Level);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
        if(PlayerPrefs.GetInt("Level") != 0)
        SceneManager.LoadSceneAsync("0");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
