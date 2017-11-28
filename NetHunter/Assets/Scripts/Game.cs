using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField]
    private GameNode _currentNode;

    private static Game instance;
    public static Game Instance() { return instance; }

    public void SetCurrentNode(GameNode node)
    {
        _currentNode = node;
        node.Status = GameNode.NodeStatus.Current;
    }

    public void JumpToNode(GameNode node)
    {
        if (!CheckNodeToJump(node)) return;

        _currentNode.Status = GameNode.NodeStatus.Cnow;
        _currentNode = node;
        _currentNode.Status = GameNode.NodeStatus.Current;
    }

    public bool CheckNodeToJump(GameNode node)
    {
        bool check = false;

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
    
        return check;
    }

    public GameNode GetCurrentNode()
    {
        return _currentNode;
    }

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
