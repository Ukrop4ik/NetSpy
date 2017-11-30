using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapCreator : MonoBehaviour {

    public Dictionary<Vector2Int, GameNode> GameNodes = new Dictionary<Vector2Int, GameNode>();

    public int NodeCount;
    [Range(0,0.9f)]
    public float Shuffle;
    [Range(0, 0.9f)]
    public float JoinChance;
    public Vector2 RandomPoz;
    Map _map = new Map();
    [SerializeField]
    private GameObject NODE;


    private static MapCreator instance;
    public static MapCreator Instance() { return instance; }

    public List<GameObject> _visualize = new List<GameObject>();
    int index = 0;
    private void Start()
    {
        instance = this;
        CreateMap();
    }

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
                _visualize.Add(no);
                GameNode game_node = no.GetComponent<GameNode>();
                game_node.x = node.x;
                game_node.y = node.y;
                game_node.w = node.w;
                GameNodes.Add(new Vector2Int(node.x, node.y), game_node);

                float random_pos = 0f;
                random_pos = Random.Range(-1f, 5f);
                float random_pos_z = 0f;
                random_pos_z = Random.Range(-1f, 5f);
                no.transform.position = new Vector3(node.x  * 10 + Random.Range(RandomPoz.x, RandomPoz.y), node.y * 10 + Random.Range(RandomPoz.x, RandomPoz.y),  0);


                no.name = "X: " + node.x + " Y: " + node.y + " W: " + node.w;

                if (node.x == 0 && node.y == 0)
                {
                    Game.Instance().SetCurrentNode(game_node);
                }
                else
                    game_node.Status = GameNode.NodeStatus.Uncnown;
            }
        }

        foreach (var ynodes in _map.nodes.Values)
        {
            foreach (var node in ynodes.Values)
            {
                GameNode _final_node = null;
                GameNodes.TryGetValue(new Vector2Int(node.x, node.y), out _final_node);

                //int _lineRenderCount = 0;

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

                        //LineRenderer _line = _final_node._lines[_lineRenderCount];
                        //_line.SetPosition(0, _final_node.transform.position);
                        //_line.SetPosition(1, _junctions_node.transform.position);
                        //_lineRenderCount++;
                    }

                if (node.parent != null)
                {
                    GameNode _parant_node = null;
                    GameNodes.TryGetValue(new Vector2Int(node.parent.x, node.parent.y), out _parant_node);
                    _final_node.parent = _parant_node;
                }

            }
        
        
        }

        CorrectJunctions(JoinChance);

       // StartCoroutine(SetVisual());
    }


    public void CorrectJunctions(float join_chance)
    {

        foreach (GameNode node in GameNodes.Values)
        {

            if (Random.Range(0f, 1f) < join_chance)
            {
                foreach (GameNode _neiNode in node.neighbors)
                {
                    if (node.junctions.Contains(_neiNode) || node.parent == _neiNode) continue;
                    node.junctions.Add(_neiNode);
                    _neiNode.junctions.Add(node);

                }
            }

            if(node.junctions.Count > 0 )
            foreach(GameNode juncnode in node.junctions)
            {
                node.SetNewLine(node.transform.position, juncnode.transform.position);
            }

            if(node.parent != null)
            node.SetNewLine(node.transform.position, node.parent.transform.position);

        }

    }

    public IEnumerator SetVisual()
    {
        while(index < _visualize.Count)
        {
            _visualize[index].GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.02f);
            index++;
        }

    }
}
