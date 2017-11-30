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
	
	// Update is called once per frame
	void Update () {
		
	}


    private IEnumerator UpdateUI()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            datatext.text = Player.Instance().GetData() + " / " + Game.Instance().Alldatacount;
            steptext.text = "Step: " + MapCreator.Instance()._maxStep;
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
        SceneManager.LoadScene(0);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);
    }

}
