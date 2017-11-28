using System;
using System.Collections.Generic;
using System.Linq;

    public class Map
    {

	public readonly Dictionary<int, Dictionary<int, Node>> nodes = new Dictionary<int, Dictionary<int, Node>>();
	private int minx = 100500;
	private int miny = 100500;
	private int maxx = -100500;
	private int maxy = -100500;
	private int startx;
	private int starty;

	public void place_node(int x, int y, float w)
	{
		minx = Math.Min(minx, x);
		miny = Math.Min(miny, y);
		maxx = Math.Max(maxx, x);
		maxy = Math.Max(maxy, y);
		Dictionary<int, Node> ynodes;
		if (!nodes.TryGetValue(x, out ynodes))
		{
			ynodes = new Dictionary<int, Node>();
			nodes.Add(x, ynodes);
		}
		ynodes[y] = new Node(x, y, w);
	}

	public bool contains(int x, int y)
	{
		Dictionary<int, Node> ynodes;
		Node node;
		return nodes.TryGetValue(x, out ynodes) && ynodes.TryGetValue(y, out node);
	}

	public bool try_get_node(int x, int y, out Node node)
	{
		node = null;
		Dictionary<int, Node> ynodes;
		return nodes.TryGetValue(x, out ynodes) && ynodes.TryGetValue(y, out node);
	}

	public Node get(int x, int y)
	{
		return nodes[x][y];
	}

	public void Place_random_nodes(int count, float shuffle)
	{
		startx = 0; // random
		starty = 0; // random
		place_node(startx, starty, 0.0f);

		var cnt = 0;

		var px = startx;
		var py = starty;
		while (cnt < count)
		{
			if (UnityEngine.Random.Range(0f, 1f) > shuffle)
			{
				var crd = UnityEngine.Random.Range(0, 2);
				var dr = UnityEngine.Random.Range(0f, 1f) > 0.5f ? -1 : 1;

                var dx = crd == 0 ? dr : 0;
				var dy = crd == 1 ? dr : 0;
				while (contains(px, py))
				{
					px += dx;
					py += dy;
				}
				place_node(px, py, UnityEngine.Random.Range(0f, 1f));
				cnt++;
			}
			else
			{
                		for (var dx = -1; dx <= 1; ++dx)
					for (var dy = -1; dy <= 1; ++dy)
					{
						var rx = px + dx;
						var ry = py + dy;
						if (!contains(rx, ry))
						{
							place_node(rx, ry, UnityEngine.Random.Range(0f, 1f));
							cnt++;
						}
					}
			}
		}

	}

	public void Calc_neighbors()
	{
		foreach (var ynodes in nodes.Values)
		{
			foreach (var node in ynodes.Values)
			{
				Node n;
				if (try_get_node(node.x - 1, node.y, out n))
					node.neighbors.Add(n);
				if (try_get_node(node.x + 1, node.y, out n))
					node.neighbors.Add(n);
				if (try_get_node(node.x, node.y + 1, out n))
					node.neighbors.Add(n);
				if (try_get_node(node.x, node.y - 1, out n))
					node.neighbors.Add(n);
			}
		}
	}

    public void Trace_astar_wave()
    {
        var order_counter = 0;
        var h = new Heap<Node>();
        h.Push(get(startx, starty).w, get(startx, starty));
        while (!h.Empty)
        {
            var cur_node = h.Pop();
            if (!cur_node.close())
                continue;
            if (cur_node.parent != null)
                cur_node.parent.junctions.Add(cur_node);
            cur_node.order = order_counter++;
            foreach (var n_node in cur_node.neighbors)
            {
                if (n_node.closed)
                    continue;
                n_node.parent = cur_node;
                h.Push(n_node.w, n_node);
            }
        }
    
	}



    }


