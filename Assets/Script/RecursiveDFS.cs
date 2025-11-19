using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveDFS : MazeLogic
{
    public List<MapLocation> directions = new List<MapLocation>()
    {
        new MapLocation(1, 0), // move kanan dengan x + 1, z + 0 dari kordinat awal
        new MapLocation(0, 1), // move kanan dengan x + 0, z + 1 dari kordinat awal
        new MapLocation(-1, 0), // move kanan dengan x - 1, z + 0 dari kordinat awal
        new MapLocation(0, -1) // move kanan dengan x + 0, z - 1 dari kordinat awal
    };

    public override void Generatemaps()
    {
        Generate(5, 5);
    }

    void Generate(int x, int z)
    {
        if (CountSquareNeighbours(x, z) >= 2) return;
        map[x, z] = 0;

        directions.Shuffle();

        Generate(x + directions[0].x, z + directions[0].z);
        Generate(x + directions[1].x, z + directions[1].z);
        Generate(x + directions[2].x, z + directions[2].z);
        Generate(x + directions[3].x, z + directions[3].z);
    }
}
