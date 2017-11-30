using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNode : MonoBehaviour {

    public enum NodeStatus
    {
        Uncnown,
        Current,
        Cnow
    }
    public enum NodeState
    {
        Close,
        Open
    }
    public enum NodeType
    {
        Null,
        Data
    }


    [SerializeField]
    private int Health;
    [SerializeField]
    private int Damage;

    public GameObject _linesRoot;

    private NodeStatus status;
    private NodeState state;
    private NodeType type;

    public NodeStatus Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
            if(status == NodeStatus.Uncnown)
            {
                _linesRoot.SetActive(false);
            }
            else
            {
                _linesRoot.SetActive(true);
            }
            ChangeNodeStatus(status);
        }
    }

    public NodeState State
    {
        get
        {
            if(Health <= 0)
            {
                state = NodeState.Open;
            }
            else
            {
                SetHealth( -Player.Instance().GetDamage());
                Player.Instance().SetHealth( -GetDamage());
            }

            return state;
        }

        set
        {
            state = value;
        }
    }

    public NodeType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public List<LineRenderer> _lines = new List<LineRenderer>();

    [SerializeField]
    private List<Material> _mat = new List<Material>();
    public int Data;
    public  int x;
    public  int y;
    public  float w;
    public bool closed;
    public  List<GameNode> neighbors = new List<GameNode>();
    public GameNode parent;
    public  List<GameNode> junctions = new List<GameNode>();
    public int order;


    public int GetHealth()
    {
        return Health;
    }
    public void SetHealth(int value)
    {
        Health += value;
    }
    public int GetDamage()
    {
        return Damage;
    }
    public void SetDamage(int value)
    {
        Damage += value;
    }

    public void SetNewLine(Vector3 coord1, Vector3 coord2)
    {
        foreach(LineRenderer line in _lines)
        {
            if(line.GetPosition(1) == Vector3.zero)
            {
                line.SetPosition(1, coord2);
                line.SetPosition(0, coord1);
                return;
            }
        }
    }

    public void ChangeNodeStatus(NodeStatus status)
    {
        switch(status)
        {
            case NodeStatus.Uncnown:
                gameObject.GetComponent<Renderer>().material = _mat[0];
                break;
            case NodeStatus.Cnow:
                gameObject.GetComponent<Renderer>().material = _mat[1];
                break;
            case NodeStatus.Current:
                gameObject.GetComponent<Renderer>().material = _mat[2];
                break;
        }
    }
}
