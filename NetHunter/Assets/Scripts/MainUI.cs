using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour {

    [SerializeField]
    private Text _lvlText;

	// Use this for initialization
	void Start () {

        _lvlText.text = "Level: " + Profile.Instance().Level;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartCampain()
    {
        Profile.Instance().GameType = Profile.SelectedGameType.Campain;
        SceneManager.LoadScene("1");
    }
    public void StartSurvive()
    {
        Profile.Instance().GameType = Profile.SelectedGameType.Survive;
        SceneManager.LoadScene("1");
    }

    public void DropProgress()
    {
        PlayerPrefs.SetInt("Level", 1);
        Profile.Instance().Level = 1;
        _lvlText.text = "Level: " + Profile.Instance().Level;
    }
}
