using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour {


    private static UI instance;
    public static UI Instance() { return instance; }
    [SerializeField]
    private GameObject _menu;
    [SerializeField]
    private GameObject _menu_win;
    [SerializeField]
    private GameObject _menu_lose;
    [SerializeField]
    private Text datatext;
    [SerializeField]
    private Text steptext;
    [SerializeField]
    private GameObject _textFrame;

    // Use this for initialization
    void Start () {
        instance = this;
        StartCoroutine(UpdateUI());
	}

    public GameObject GetLosePanel()
    {
        return _menu_lose;
    }

    // Update is called once per frame
    void Update ()
    {
        Game.Instance().isStop = _menu.activeInHierarchy;	
	}

    public GameObject GetWinPanel()
    {
        return _menu_win;
    }

    public void LoadScene(string id)
    {
        SceneManager.LoadScene(id);
    }

    private IEnumerator UpdateUI()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            datatext.text = Player.Instance().GetData() + " / " + Game.Instance().Alldatacount;

            if (Game.Instance().looseType == Game.SelectedGameLooseType.Step && MapCreator.Instance()._maxStep > 0)
                steptext.text = "Step: " + MapCreator.Instance()._maxStep;
            if (Game.Instance().looseType == Game.SelectedGameLooseType.Clock)
                steptext.text = "Time: " + Game.Instance()._Time;

        }
    }

    public void CreateTextFrame(string text, Vector3 pos)
    {
        GameObject frame = Instantiate(_textFrame, transform);
        frame.transform.position = pos;
        frame.GetComponent<Text>().text = text;
        Destroy(frame, 1f);
    }

    public void CreateNewWeb()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);
    }

}
