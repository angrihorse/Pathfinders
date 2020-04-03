using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static List<Node> RetracePath(Node startNode, Node targetNode, Dictionary<Node, Node> nodeToParent) {
		List<Node> path = new List<Node>();
		Node currentNode = targetNode;
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = nodeToParent[currentNode];
		}
		path.Add(startNode); // For simpler visualizing.
		path.Reverse(); // Optional.
		return path;
	}
}
