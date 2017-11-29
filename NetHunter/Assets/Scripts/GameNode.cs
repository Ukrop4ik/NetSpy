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

    public GameObject _linesRoot;

    private NodeStatus status;

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
               // _linesRoot.SetActive(false);
            }
            else
            {
                _linesRoot.SetActive(true);
            }
            ChangeNodeStatus(status);
        }
    }

    public List<LineRenderer> _lines = new List<LineRenderer>();

    [SerializeField]
    private List<Material> _mat = new List<Material>();

    public  int x;
    public  int y;
    public  float w;
    public bool closed;
    public  List<GameNode> neighbors = new List<GameNode>();
    public GameNode parent;
    public  List<GameNode> junctions = new List<GameNode>();
    public int order;


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
