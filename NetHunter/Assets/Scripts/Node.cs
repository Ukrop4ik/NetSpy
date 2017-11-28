using System;
using System.Collections.Generic;
using System.Linq;


    public class Node
    {
	public readonly int x;
	public readonly int y;
	public readonly float w;
	public bool closed;
	public readonly List<Node> neighbors = new List<Node>();
	public Node parent;
	public readonly List<Node> junctions = new List<Node>();
	public int order;
	
	public Node(int x, int y, float w)
	{
		this.x = x;
		this.y = y;
		this.w = w;
	}

	public bool close()
	{
		if (closed)
			return false;
		closed = true;
		return true;
	}

    }



