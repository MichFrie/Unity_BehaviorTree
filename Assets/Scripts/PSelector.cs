using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSelector : Node
{
	Node[] nodeArray;
	
	public PSelector(string n)
	{
		Name = n;
	}

	void OrderNodes()
	{
		nodeArray = children.ToArray();
		Sort(nodeArray, 0, children.Count - 1);
		children = new List<Node>(nodeArray);
	}
	
	public override Status Process()
	{
		OrderNodes();
		
		Status childStatus = children[currentChild].Process();
		
		if (childStatus == Status.RUNNING)
			return Status.RUNNING;

		if (childStatus == Status.SUCCESS)
		{
			currentChild = 0;
			return Status.SUCCESS;
		}

		currentChild++;
		
		if (currentChild >= children.Count)
		{
			currentChild = 0;
			return Status.FAILURE;
		}
		
		return Status.RUNNING;
	}
	
	int Partition(Node[] array, int low, int high)
	{
		Node pivot = array[high];

		int lowIndex = (low - 1);

		//2. Reorder the collection.
		for (int j = low; j < high; j++)
		{
			if (array[j].sortOrder <= pivot.sortOrder)
			{
				lowIndex++;

				Node temp = array[lowIndex];
				array[lowIndex] = array[j];
				array[j] = temp;
			}
		}

		Node temp1 = array[lowIndex + 1];
		array[lowIndex + 1] = array[high];
		array[high] = temp1;

		return lowIndex + 1;
	}

	void Sort(Node[] array, int low, int high)
	{
		if (low < high)
		{
			int partitionIndex = Partition(array, low, high);
			Sort(array, low, partitionIndex - 1);
			Sort(array, partitionIndex + 1, high);
		}
	}
}
