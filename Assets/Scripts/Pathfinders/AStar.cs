using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
	public static List<Node> FindPath(SquareGrid graph) {
		Node startNode = graph.NodeFromWorldPoint(graph.start.position);
		Node targetNode = graph.NodeFromWorldPoint(graph.target.position);

		Dictionary<Node, Node> nodeParent = new Dictionary<Node, Node>();
		PriorityQueue<Node> frontier = new PriorityQueue<Node>();
		Dictionary<Node, int> costFromStartTo = new Dictionary<Node, int>();
		for (int x = 0; x < graph.gridSize.x; x++) {
			for (int y = 0; y < graph.gridSize.y; y++) {
				costFromStartTo.Add(graph.grid[x, y], int.MaxValue);
			}
		}
		costFromStartTo[startNode] = 0;

		frontier.Enqueue(startNode, costFromStartTo[startNode]);
		while (frontier.Count > 0) {
			Node currentNode = frontier.Dequeue();

			if (currentNode == targetNode) {
				return Utils.RetracePath(startNode, targetNode, nodeParent);
			}

			foreach (Node neighbor in graph.GetNeighbors(currentNode)) {
				int newCost = costFromStartTo[currentNode] + graph.edgeWeight(currentNode, neighbor);
				if (newCost < costFromStartTo[neighbor]) {
					costFromStartTo[neighbor] = newCost;
					nodeParent[neighbor] = currentNode;
					int heuristics = ManhattanDistance(neighbor, targetNode);
					frontier.Enqueue(neighbor, costFromStartTo[neighbor] + heuristics);
					// Remove costFromStartTo[neighbor] and you will get Greedy Best First Search.
				}
			}
		}
		return null;
	}

	static int ManhattanDistance(Node nodeA, Node nodeB) {
		Vector2Int diff = nodeA.gridPos - nodeB.gridPos;
		return Mathf.Abs(diff.x) + Mathf.Abs(diff.y);
	}

	static int EuclideanDistance(Node nodeA, Node nodeB) {
		return (int)Vector2Int.Distance(nodeA.gridPos, nodeB.gridPos);
	}
}
