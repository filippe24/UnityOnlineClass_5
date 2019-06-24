using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;


    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] bool isRunning = true; // todo make private


    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
   Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    // Start is called before the first frame update
	void Start () 
    {
        LoadBlocks();
        ColorStartAndEnd();
        ExploreNeighbours();
        Pathfind();
    }
    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }


    private void ExploreNeighbours()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinates = startWaypoint.GetGridPos() + direction;
            try
            {
                grid[explorationCoordinates].SetTopColor(Color.blue);
            }
            catch
            {
                // do nothing
            }
        }
    }
    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping the overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }

    //actual loop of the pathfinding
    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0)
        {
            var searchCenter = queue.Dequeue();
            print("Searching from: " + searchCenter); // todo remove log
            HaltIfEndFound(searchCenter);
        }

        print("Finished pathfinding?");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter == endWaypoint)
        {
            print("Searching from end node, therefore stopping"); // todo remove log
            isRunning = false;
        }
    }

}
