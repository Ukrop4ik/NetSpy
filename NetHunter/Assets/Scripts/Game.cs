using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public enum SelectedGameLooseType
    {
        Step,
        Clock
    }

    public SelectedGameLooseType looseType;

    [SerializeField]
    private GameNode _currentNode;
    public bool isStop = false;
    [SerializeField]
    private int alldatacount;
    public int _Time;

    private static Game instance;

    public int Alldatacount
    {
        get
        {
            return alldatacount;
        }

        set
        {
            alldatacount = value;
        }
    }

    public void Win()
    {
        isStop = true;
        UI.Instance().OpenPanel(UI.Instance().GetWinPanel());
        Profile.Instance().AddLevel(1);
    }
    public void Lose()
    {
        UI.Instance().OpenPanel(UI.Instance().GetLosePanel());
        isStop = true;
    }

    public static Game Instance() { return instance; }
    
    private IEnumerator Clock()
    {
        while(_Time > 0)
        {
            if (!isStop)
            {
                _Time--;

                if (_Time <= 0)
                {
                    Lose();
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void StartGame()
    {
        switch(Profile.Instance().GameType)
        {
            case Profile.SelectedGameType.Campain:
                MapCreator.Instance().CreateMap();
                break;
            case Profile.SelectedGameType.Survive:
                MapCreator.Instance().ConfiguredMapCreator(3000, 0.5f, 3000, 15000);
                break;
        }

        if(looseType == SelectedGameLooseType.Clock)
        {
            MapCreator.Instance()._maxStep = 0;
            _Time = (int)(MapCreator.Instance().NodeCount * 1.5f);
            StartCoroutine(Clock());
        }

    }

    public void SetCurrentNode(GameNode node)
    {
        if(_currentNode)
         _currentNode.Status = GameNode.NodeStatus.Cnow;
        _currentNode = node;
        node.Status = GameNode.NodeStatus.Current;
    }

    public void JumpToNode(GameNode node)
    {
        if (!CheckNodeToJump(node) || isStop) return;

        SetCurrentNode(node);

        if (looseType == SelectedGameLooseType.Step)
        {
            MapCreator.Instance()._maxStep--;
            if (MapCreator.Instance()._maxStep <= 0)
                Lose();
        }

        if (node.Type == GameNode.NodeType.Data)
        {
            Player.Instance().SetData(node.Data);
            print("Collect " + node.Data + " DATA ");
            UI.Instance().CreateTextFrame(node.Data.ToString(), Camera.main.WorldToScreenPoint(node.transform.position));
            node.Data = 0;
            node.Type = GameNode.NodeType.Null;

            if (Player.Instance().GetData() >= Alldatacount)
            {
                Win();
            }

        }

    }


    public bool CheckNodeToJump(GameNode node)
    {
        bool check = false;

        if (CheckNodeState(node))
        {
            if (node == _currentNode.parent)
            {
                check = true;
            }
            else
                foreach (GameNode _node in _currentNode.junctions)
                {
                    if (_node == node)
                    {
                        check = true;
                    }
                }
        }
        return check;
    }

    public bool CheckNodeState(GameNode node)
    {
        return node.State == GameNode.NodeState.Open;
    }

    public GameNode GetCurrentNode()
    {
        return _currentNode;
    }

    // Use this for initialization
    void Start () {

        if (Profile.Instance().Level % 3 == 0)
            looseType = SelectedGameLooseType.Clock;

        StartGame();
	}

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
