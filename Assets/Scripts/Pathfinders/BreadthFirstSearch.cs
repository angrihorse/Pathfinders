using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BreadthFirstSearch
{
	public static void FindPath(SquareGrid graph, Node startNode, Node targetNode) {
		Dictionary<Node, Node> nodeParent = new Dictionary<Node, Node>();
		Queue<Node> frontier = new Queue<Node>();
		HashSet<Node> visited = new HashSet<Node>();

		frontier.Enqueue(startNode);
		while (frontier.Count > 0) {
			Node currentNode = frontier.Dequeue();

			if (currentNode == targetNode) {
				graph.RetracePath(startNode, targetNode, nodeParent);
				return;
			}

			foreach (Node neighbor in graph.GetNeighbors(currentNode)) {
				if (!visited.Contains(neighbor)) {
					visited.Add(neighbor);
					nodeParent[neighbor] = currentNode;
					frontier.Enqueue(neighbor);
				}
			}
		}

		graph.path = null;
	}
}
