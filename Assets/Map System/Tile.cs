using System;
using System.Collections.Generic;
using Assets.GameSystem;
using UnityEditor;
using UnityEngine;

public class Tile : MonoBehaviour, IEquatable<Tile> {
    public Hex Hex;
    public Character Occupant;
    public List<Tile> Neighbors;
    public bool isObstacle;
    public GameObject Prefab;
    public int WorldX => _coordinates.Column;
    public int WorldY => _coordinates.Row;
    public bool IsOccupied => Occupant != null;
    private OffsetCoordinates _coordinates;

    public void Init (Hex hex) {
        isObstacle = tag == "Obstacle";
        Hex = hex;
        GetNeighbors ();
    }

    public void InitPointy (Hex hex, bool even = true) {
        Hex = hex;
        float x;
        if (even) {
            _coordinates = OffsetCoordinates.EvenPointyOffset (hex);
            x = WorldY % 2 == 0 ? Mathf.Sqrt (3f) * WorldX :
                Mathf.Sqrt (3f) * (WorldX - 1) + (Mathf.Sqrt (3f) / 2f);
        } else {
            _coordinates = OffsetCoordinates.OddPointyOffset (hex);
            x = WorldY % 2 == 0 ? Mathf.Sqrt (3f) * WorldX :
                Mathf.Sqrt (3f) * (WorldX - 1) + (Mathf.Sqrt (3f) / 2f);
        }
        var y = WorldX % 2 == 0 ? 3f * WorldY / 2f : 3f * WorldY;
        transform.position = new Vector3 (x, 3f * WorldY / 2f);
    }

    public void InitFlat (Hex hex, bool even = true) {
        Hex = hex;
        //gameObject.transform.rotation = Quaternion.Euler(0,0,30);
        if (even) {
            _coordinates = OffsetCoordinates.EvenFlatOffset (hex);
        } else {
            _coordinates = OffsetCoordinates.OddFlatOffset (hex);
        }
        var x = WorldY % 2 == 0 ? Mathf.Sqrt (3f) * WorldX : Mathf.Sqrt (3f) * (WorldX - 1) + (Mathf.Sqrt (3f) / 2f);
        var y = WorldX % 2 == 0 ? 3f * WorldY / 2f : 3f * WorldY;
        transform.position = new Vector3 (x, 3f * WorldY / 2f);
    }

    public void GetNeighbors () {
        Neighbors = new List<Tile> ();
        foreach (var hex in Hex.Neighbors) {
            var go = GameObject.Find (hex.ToString ());
            if (go != null)
                Neighbors.Add (go.GetComponent<Tile> ());
        }
    }

    public int GetDistance (Tile other) {
        return Hex.GetDistance (other.Hex);
    }

    public int GetDistance (Hex other) {
        return Hex.GetDistance (other);
    }

    public static List<Tile> GetTilesInsideRangeFromOrigin (int range) {
        var tiles = new List<Tile> ();
        foreach (var tile in MapCoordinator.Coordinator.Map) {
            var checkX = Math.Abs (tile.Hex.X) <= range;
            var checkY = Math.Abs (tile.Hex.Y) <= range;
            var checkZ = Math.Abs (tile.Hex.Z) <= range;
            var validHex = (tile.Hex.X + tile.Hex.Y + tile.Hex.Z) == 0;
            if (checkX && checkY && checkZ && validHex) {
                tiles.Add (tile);
            }
        }
        return tiles;
    }

    public List<Tile> GetTilesAtDistance (int distance) {
        if (distance == 1) {
            return Neighbors;
        }

        var tiles = new List<Tile> ();
        foreach (var tile in MapCoordinator.Coordinator.Map) {
            if (tile == this || tile.GetDistance (Hex) != distance) continue;
            tiles.Add (tile);
        }
        return tiles;
    }

    public List<Tile> GetTilesInsideRange (int range) {
        if (range == 1) {
            return Neighbors;
        }

        var tiles = new List<Tile> ();
        foreach (var tile in MapCoordinator.Coordinator.Map) {
            if (tile == this) continue;
            var checkX = tile.Hex.X <= Hex.X + range &&
                tile.Hex.X >= Hex.X - range;
            var checkY = (tile.Hex.Y) <= Hex.Y + range &&
                (tile.Hex.Y) >= Hex.Y - range;
            var checkZ = (tile.Hex.Z) <= Hex.Z + range &&
                (tile.Hex.Z) >= Hex.Z - range;
            var validHex = (tile.Hex.X + tile.Hex.Y + tile.Hex.Z) == 0;
            if (checkX && checkY && checkZ && validHex) {
                tiles.Add (tile);
            }
        }
        return tiles;
    }

    public override int GetHashCode () {
        return Hex.GetHashCode ();
    }

    public override bool Equals (System.Object other) {

        if (other is Tile tile) {;
            return Hex.Equals (tile.Hex);
        }
        if (other is Hex hex) {
            return Hex.Equals (Hex);
        }
        return false;
    }

    public bool Equals (Tile other) {
        return Hex.Equals (other.Hex);
    }

    public static bool operator == (Tile lhs, Tile rhs) {
        if (System.Object.ReferenceEquals (lhs, rhs)) {
            return true;
        }
        if (lhs is null || rhs is null) {
            return false;
        }

        return lhs?.Hex == rhs?.Hex;
    }

    public static bool operator != (Tile lhs, Tile rhs) {
        return !(lhs == rhs);
    }

    // public void OnDrawGizmos() {
    //     return;
    //     if (Hex is null) return;

    //     var text = Hex.ToString();
    //     var style = new GUIStyle() {
    //         fontSize = 8
    //     };
    //     var position = transform.position;
    //     Handles.Label(position, text, style);
    // }
}