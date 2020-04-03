using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dijkstra
{
	public static List<Node> FindPath(SquareGrid graph) {
		Node startNode = graph.NodeFromWorldPoint(graph.start.position);
		Node targetNode = graph.NodeFromWorldPoint(graph.target.position);

		Dictionary<Node, Node> nodeParent = new Dictionary<Node, Node>();
		PriorityQueue<Node> frontier = new PriorityQueue<Node>();
		Dictionary<Node, int> costFromStart = new Dictionary<Node, int>();
		for (int x = 0; x < graph.gridSize.x; x++) {
			for (int y = 0; y < graph.gridSize.y; y++) {
				costFromStart.Add(graph.grid[x, y], int.MaxValue);
			}
		}
		costFromStart[startNode] = 0;

		frontier.Enqueue(startNode, costFromStart[startNode]);
		while (frontier.Count > 0) {
			Node currentNode = frontier.Dequeue();

			if (currentNode == targetNode) {
				return Utils.RetracePath(startNode, targetNode, nodeParent);
			}

			foreach (Node neighbor in graph.GetNeighbors(currentNode)) {
				int newCost = costFromStart[currentNode] + graph.edgeWeight(currentNode, neighbor);
				if (newCost < costFromStart[neighbor]) {
					costFromStart[neighbor] = newCost;
					nodeParent[neighbor] = currentNode;
					frontier.Enqueue(neighbor, costFromStart[neighbor]);
				}
			}
		}
		return null;
	}
}
