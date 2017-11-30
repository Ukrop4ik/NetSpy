using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField]
    private GameNode _currentNode;

    [SerializeField]
    private int alldatacount;

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

    public static Game Instance() { return instance; }

    public void SetCurrentNode(GameNode node)
    {
        if(_currentNode)
         _currentNode.Status = GameNode.NodeStatus.Cnow;
        _currentNode = node;
        node.Status = GameNode.NodeStatus.Current;
    }

    public void JumpToNode(GameNode node)
    {
        if (!CheckNodeToJump(node)) return;

        SetCurrentNode(node);

        MapCreator.Instance()._maxStep--;

        if(node.Type == GameNode.NodeType.Data)
        {
            Player.Instance().SetData(node.Data);
            print("Collect " + node.Data + " DATA ");
            UI.Instance().CreateTextFrame(node.Data.ToString(), Camera.main.WorldToScreenPoint(node.transform.position));
            node.Data = 0;
            node.Type = GameNode.NodeType.Null;

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
       
	}

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
