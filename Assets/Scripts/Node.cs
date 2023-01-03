using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public enum Status
	{
		SUCCESS, 
		RUNNING, 
		FAILURE
	}

	public Status status;

	public List<Node> children = new List<Node>();

	public int currentChild = 0;
	public int sortOrder;

	public string Name;

	public Node()
	{
		
	}
	
	public Node(string n)
	{
		Name = n;
	}
	public Node(string n, int order)
	{
		Name = n;
		sortOrder = order;
	}

	public virtual Status Process()
	{
		return children[currentChild].Process();
	}
	
	public void AddChild(Node n)
	{
		children.Add(n);
	}

}
