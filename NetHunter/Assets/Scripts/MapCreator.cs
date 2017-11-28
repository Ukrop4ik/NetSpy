using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapCreator : MonoBehaviour {

    public Dictionary<Vector2Int, GameNode> GameNodes = new Dictionary<Vector2Int, GameNode>();

    public int NodeCount;
    [Range(0,0.9f)]
    public float Shuffle;
    Map _map = new Map();
    [SerializeField]
    private GameObject NODE;
    [ContextMenu("Create")]
    public void CreateMap()
    {
        _map.Place_random_nodes(NodeCount, Shuffle);
        _map.Calc_neighbors();
        _map.Trace_astar_wave();

        foreach (var ynodes in _map.nodes.Values)
        {
            foreach (var node in ynodes.Values)
            {
                GameObject no = Instantiate(NODE);
                GameNode game_node = no.GetComponent<GameNode>();
                game_node.x = node.x;
                game_node.y = node.y;
                game_node.w = node.w;
                GameNodes.Add(new Vector2Int(node.x, node.y), game_node);
                no.transform.position = new Vector3(node.x * 10, node.y * 10, 0);
                no.name = "X: " + node.x + " Y: " + node.y + " W: " + node.w;
            }
        }

        foreach (var ynodes in _map.nodes.Values)
        {
            foreach (var node in ynodes.Values)
            {
                GameNode _final_node = null;
                GameNodes.TryGetValue(new Vector2Int(node.x, node.y), out _final_node);

                int _lineRenderCount = 0;

                if (_final_node == null) continue;

                if (node.neighbors.Count > 0)
                    foreach (Node n in node.neighbors)
                    {
                        GameNode _neighbors_node = null;
                        GameNodes.TryGetValue(new Vector2Int(n.x, n.y), out _neighbors_node);
                        _final_node.neighbors.Add(_neighbors_node);
                       
                    }

                if (node.junctions.Count > 0)
                    foreach (Node n in node.junctions)
                    {
                        GameNode _junctions_node = null;
                        GameNodes.TryGetValue(new Vector2Int(n.x, n.y), out _junctions_node);
                        _final_node.junctions.Add(_junctions_node);

                        LineRenderer _line = _final_node._lines[_lineRenderCount];
                        _line.SetPosition(0, _final_node.transform.position);
                        _line.SetPosition(1, _junctions_node.transform.position);
                        _lineRenderCount++;
                    }

                if (node.parent != null)
                {
                    GameNode _parant_node = null;
                    GameNodes.TryGetValue(new Vector2Int(node.parent.x, node.parent.y), out _parant_node);
                    _final_node.parent = _parant_node;
                }
            }
        
        
        }
    } 
}
