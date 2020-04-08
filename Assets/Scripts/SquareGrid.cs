using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGrid : MonoBehaviour
{
	public Vector2 gridWorldSize;
	public float squareSize;
	public LayerMask unwalkableMask;

	public Transform start, target;
	public List<Node> path;

	[HideInInspector]
	public Vector2Int gridSize;
	public Node[,] grid;

    void Start()
    {
		gridSize = Vector2Int.RoundToInt(gridWorldSize / squareSize);
		InitializeGrid();
    }

	void Update() {
		AStar.FindPath(this, NodeFromWorldPoint(start.position), NodeFromWorldPoint(target.position));
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
		Gizmos.color = Color.cyan;

		// Draw path with lines.
		if (path != null) {
			Gizmos.color = Color.red;
			for (int i = 0; i < path.Count - 1; i++) {
				Gizmos.DrawLine(path[i].worldPos, path[i+1].worldPos);
			}
		}

		// Fill in unwalkable nodes.
		if (grid != null) {
			foreach (Node node in grid) {
				if (!node.walkable) {
					Gizmos.color = Color.black;
					Gizmos.DrawCube(node.worldPos, Vector3.one * squareSize * 0.95f);
				}
			}
		}
	}

    void InitializeGrid() {
		grid = new Node[gridSize.x, gridSize.y];
		Vector3 gridRelativeBottomLeft = new Vector3(-gridWorldSize.x/2, 0, -gridWorldSize.y/2);
		Vector3 worldBottomLeft = transform.position + gridRelativeBottomLeft;
		for (int x = 0; x < gridSize.x; x++) {
			for (int y = 0; y < gridSize.y; y++) {
				Vector3 bottomLeftRelativePos = new Vector3(squareSize * (x + 0.5f), 0, squareSize * (y + 0.5f));
				Vector3 worldPos = worldBottomLeft + bottomLeftRelativePos;
				bool walkable = !(Physics.CheckSphere(worldPos, squareSize/2, unwalkableMask));
				Vector2Int gridPos = new Vector2Int(x, y);
				grid[x, y] = new Node(walkable, gridPos, worldPos);
			}
		}
	}

	public List<Node> GetNeighbors(Node node) {
		List<Node> neighbors = new List<Node>();
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0) {
					continue;
				}
				Vector2Int pos = node.gridPos + new Vector2Int(x, y);
				if (0 <= pos.x && pos.x < gridSize.x && 0 <= pos.y && pos.y < gridSize.y && grid[pos.x, pos.y].walkable) {
					neighbors.Add(grid[pos.x, pos.y]);
				}
			}
		}

		return neighbors;
	}

	public int edgeWeight(Node nodeA, Node nodeB) {
		Vector2Int diff = nodeA.gridPos - nodeB.gridPos;
		int sum = diff.x + diff.y;
		if (sum == 0 || sum == 2 || sum == -2) {
			return 14;
		} else {
			return 10;
		}
	}

	public Node NodeFromWorldPoint(Vector3 worldPos) {
		float ratioX = worldPos.x / gridWorldSize.x + 0.5f;
		float ratioY = worldPos.z / gridWorldSize.y + 0.5f;
		ratioX = Mathf.Clamp01(ratioX);
		ratioY = Mathf.Clamp01(ratioY);
		int x = Mathf.RoundToInt((gridSize.x - 1) * ratioX);
		int y = Mathf.RoundToInt((gridSize.y - 1) * ratioY);
		return grid[x, y];
	}

	public void RetracePath(Node startNode, Node targetNode, Dictionary<Node, Node> nodeToParent) {
		List<Node> newPath = new List<Node>();
		Node currentNode = targetNode;
		while (currentNode != startNode) {
			newPath.Add(currentNode);
			currentNode = nodeToParent[currentNode];
		}
		newPath.Add(startNode); // For simpler visualizing.
		newPath.Reverse(); // Optional.
		path = newPath;
	}
}
