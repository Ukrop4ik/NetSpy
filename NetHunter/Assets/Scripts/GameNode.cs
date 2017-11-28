using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNode : MonoBehaviour {

    public List<LineRenderer> _lines = new List<LineRenderer>();

    public  int x;
    public  int y;
    public  float w;
    public bool closed;
    public  List<GameNode> neighbors = new List<GameNode>();
    public GameNode parent;
    public  List<GameNode> junctions = new List<GameNode>();
    public int order;
}
