using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
	public static void FindPath(SquareGrid graph, Node startNode, Node targetNode) {
		Dictionary<Node, Node> nodeParent = new Dictionary<Node, Node>();
		Heap<Node> frontier = new Heap<Node>(graph.gridSize.x * graph.gridSize.y);
		Dictionary<Node, int> costFromStartTo = new Dictionary<Node, int>();
		for (int x = 0; x < graph.gridSize.x; x++) {
			for (int y = 0; y < graph.gridSize.y; y++) {
				costFromStartTo.Add(graph.grid[x, y], int.MaxValue);
			}
		}
		costFromStartTo[startNode] = 0;

		frontier.Insert(startNode, costFromStartTo[startNode]);
		while (frontier.itemCount > 0) {
			Node currentNode = frontier.Extract();

			if (currentNode == targetNode) {
				graph.RetracePath(startNode, targetNode, nodeParent);
				return;
			}

			foreach (Node neighbor in graph.GetNeighbors(currentNode)) {
				int newCost = costFromStartTo[currentNode] + graph.edgeWeight(currentNode, neighbor);
				if (newCost < costFromStartTo[neighbor]) {
					costFromStartTo[neighbor] = newCost;
					nodeParent[neighbor] = currentNode;
					int heuristics = EuclideanDistance(neighbor, targetNode);
					frontier.Insert(neighbor, costFromStartTo[neighbor] + heuristics);
				}
			}
		}

		graph.path = null;
	}

	static int ManhattanDistance(Node nodeA, Node nodeB) {
		Vector2Int diff = nodeA.gridPos - nodeB.gridPos;
		return Mathf.Abs(diff.x) + Mathf.Abs(diff.y);
	}

	static int EuclideanDistance(Node nodeA, Node nodeB) {
		return (int)Vector2Int.Distance(nodeA.gridPos, nodeB.gridPos);
	}
}
