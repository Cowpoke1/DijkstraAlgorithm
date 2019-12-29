using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DijkstrasAlg : MonoBehaviour
{

    [Header("Starting Node")]
    [SerializeField]
    private Node nodeStart;
    [Header("End Node")]
    [SerializeField]
    private Node nodeEnd;
    [SerializeField]
    private LineRenderer linePath;
    private List<Node> nodes = new List<Node>();
    [SerializeField]
    Transform parentNodes;
    [SerializeField]
    Button btnPath;

    private void Start()
    {
        foreach (Transform child in parentNodes)
        {
            nodes.Add(child.GetComponent<Node>());
        }
        btnPath.onClick.AddListener(PathButton);
    }

    private void PathButton()
    {
        List<Vector3> path = GetPath();

        linePath.positionCount = path.Count;
        linePath.SetPositions(path.ToArray());
    }

    private List<Vector3> GetPath()
    {

        Path path = new Path();

        List<Node> unvisitedNodes = new List<Node>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
        Dictionary<Node, float> weights = new Dictionary<Node, float>();

        for (int i = 0; i < nodes.Count; i++)
        {
            Node node = nodes[i];
            unvisitedNodes.Add(node);
            weights.Add(node, float.MaxValue);
        }

        weights[nodeStart] = 0f;
        while (unvisitedNodes.Count != 0)
        {

            unvisitedNodes = unvisitedNodes.OrderBy(node => weights[node]).ToList();

            Node tempNode = unvisitedNodes[0];

            unvisitedNodes.Remove(tempNode);

            if (tempNode == nodeEnd)
            {
                while (previous.ContainsKey(tempNode))
                {

                    path.nodes.Insert(0, tempNode);

                    tempNode = previous[tempNode];
                }

                path.nodes.Insert(0, tempNode);
                break;
            }

            for (int i = 0; i < tempNode.edges.Count; i++)
            {
                Node neighbor = tempNode.edges[i];

                float weight = Vector3.Distance(tempNode.transform.position, neighbor.transform.position);

                float cost = weights[tempNode] + weight;

                if (cost < weights[neighbor])
                {
                    weights[neighbor] = cost;
                    previous[neighbor] = tempNode;
                }
            }
        }

        List<Vector3> tempPath = new List<Vector3>();
        path.nodes.ForEach((x) => tempPath.Add(x.transform.position));
        return tempPath;

    }

}
[Serializable]
public class Path
{

    public List<Node> nodes = new List<Node>();
    public float weight = 0f;

}

