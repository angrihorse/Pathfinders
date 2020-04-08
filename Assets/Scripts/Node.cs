using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public bool walkable;
	public Vector2Int gridPos;
	public Vector3 worldPos;

	public Node(bool _walkable, Vector2Int _gridPos, Vector3 _worldPos) {
		walkable = _walkable;
		gridPos = _gridPos;
		worldPos = _worldPos;
	}
}
