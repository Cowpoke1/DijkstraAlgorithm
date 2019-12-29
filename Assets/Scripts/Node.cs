using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Node : MonoBehaviour
{
    [SerializeField]
    LineRenderer line;

    public List<Node> edges = new List<Node>();

    public void ShowGraph()
    {
        if (edges.Count == 0)
            return;

        line.positionCount = edges.Count * 2;

        for (int i = 0; i < edges.Count; i++)
        {
            if (edges[i] != null)
            {
                float distance = Vector3.Distance(transform.position, edges[i].transform.position);
                Vector3 diff = edges[i].transform.position - transform.position;
                Handles.Label(transform.position + (diff / 2), distance.ToString(), EditorStyles.whiteBoldLabel);
               // Handles.DrawLine(transform.position, edges[i].transform.position);
                line.SetPosition(i * 2, transform.position);
                line.SetPosition((i * 2 + 1), edges[i].transform.position);

                if (!edges[i].edges.Contains(this))
                {
                    edges[i].edges.Add(this);
                }

            }
            else
            {
                edges.RemoveAt(i);
            }
        }

    }


    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        ShowGraph();
#endif
    }

}
