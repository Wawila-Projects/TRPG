using System;
using System.Linq;
using System.Collections.Generic;
using Priority_Queue;

public static class AStar
{

    public static List<List<Tile>> FindPaths(Tile start, Tile goal) {
        var paths = new List<List<Tile>>();
        var banned = new List<Tile>();

        var path = FindPath(start, goal);
        while (path.Count != 0) {
            paths.Add(path);
            var tile = path.ElementAtOrDefault(path.Count-2);
            if (!tile.IsOccupied) break;
            banned.Add(tile);
            path = FindPath(start, goal, banned);
        }

        paths.OrderBy((p) => p.Count);
        return paths;
    }
    public static List<Tile> FindPath(Tile start, Tile goal, List<Tile> _banned = null)
    {
        var banned = _banned ?? new List<Tile>();
        var cameFrom = new Dictionary<Tile, Tile>();
        var costSoFar = new Dictionary<Tile, int>();
        cameFrom[start] = null;
        costSoFar[start] = 0;

        var frontier = new SimplePriorityQueue<Tile>();
        frontier.Enqueue(start, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current == null || current.isObstacle) continue;
            if (banned.Contains(current)) continue;

            if (current == goal)
                break;

            foreach (var tile in current.Neighbors)
            {
                var next = tile.GetComponent<Tile>();

                if(next == null || next.isObstacle) continue;
                if (banned.Contains(next)) continue;
                
                var newCost = costSoFar[current] + 1;

                if (costSoFar.ContainsKey(next) && newCost >= costSoFar[next]) continue;

                costSoFar[next] = newCost;
                var priority = newCost + Heuristic(goal, next);
                frontier.Enqueue(next, priority);
                cameFrom[next] = current;
            }
        }

        return GetPath(cameFrom, goal);
    }

    private static float Heuristic(Tile a, Tile b)
    {
        return a.Hex.GetDistance(b.Hex);
    }

    private static List<Tile> GetPath(IDictionary<Tile, Tile> dict, Tile start)
    {
        if (!dict.ContainsKey(start))
            return new List<Tile>();

        var path = new List<Tile> { start };
        var current = dict[start];

        while (current != null)
        {
            path.Add(current);
            current = dict[current];
        }

        path.Reverse();

        return path;
    }
}


//*
// * 
//frontier = PriorityQueue()
//frontier.put(start, 0)
//came_from = {}
//cost_so_far = {}
//came_from[start] = None
//cost_so_far[start] = 0

//while not frontier.empty():
//   current = frontier.get()

//   if current == goal:
//      break

//   for next in graph.neighbors(current):
//      new_cost = cost_so_far[current] + graph.cost(current, next)
//      if next not in cost_so_far or new_cost < cost_so_far[next]:
//         cost_so_far[next] = new_cost
//         priority = new_cost + heuristic(goal, next)
//         frontier.put(next, priority)
//         came_from[next] = current
// */
