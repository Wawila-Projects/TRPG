using System;
using System.Collections.Generic;
using Assets.Take_II.Scripts.HexGrid;
using Priority_Queue;
using UnityEngine;

namespace Assets.Take_II.Scripts
{
    public class AStar
    {
        public List<string> FindPath(Tile start, Tile goal)
        {
            var cameFrom = new Dictionary<Tile, Tile>();
            var costSoFar = new Dictionary<Tile, int>();
            cameFrom[start] = null;
            costSoFar[start] = 0;

            var frontier = new SimplePriorityQueue<Tile>();
            frontier.Enqueue(start, 0);

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.IsEqualTo(goal))
                    break;

                foreach (var tile in current.Neighbors)
                {
                    var next = tile.GetComponent<Tile>();
                    var newCost = costSoFar[current] + next.Cost;

                    if (costSoFar.ContainsKey(next) && newCost >= costSoFar[next]) continue;

                    costSoFar[next] = newCost;
                    var priority = newCost + Heuristic(goal, next)*next.Cost;
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }

            }

            return GetPath(cameFrom, goal);
        }

        private static int Heuristic(Tile a, Tile b)
        {
            return Math.Max(Math.Abs(a.GridX - b.GridX), Math.Abs(a.GridY - b.GridY));
        }

        private static List<string> GetPath(IDictionary<Tile, Tile> dict, Tile start)
        {
            if (!dict.ContainsKey(start))
                return null;

            var path = new List<string> { start.Name };
            var current = dict[start];

            while (current != null)
            {
                path.Add(current.Name);
                current = dict[current];
            }

            path.Reverse();

            return path;
        }

        
    }
}

///*
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
