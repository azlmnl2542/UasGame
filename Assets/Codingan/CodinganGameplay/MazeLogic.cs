using System.Collections.Generic;
using UnityEngine;

public class MapLocation
{
    public int x;
    public int z;

    public MapLocation(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
}

public class MazeLogic : MonoBehaviour
{
    [Header("Maze Settings")]
    public int width = 30;                  // x length
    public int depth = 30;                  // z length
    public int scale = 6;                   // ukuran skala cube
    public GameObject Character;
    public GameObject Enemy;
    public int EnemyCount = 3;
    public List<GameObject> Cube;           // Maze Wall (List untuk variasi)
    public byte[,] map;                     // Array 2D untuk menyimpan data maze
    GameObject[,] BuildingList;

    void Start()
    {
        InitialiseMap();
        GenerateMaps();
        DrawMaps(); 
        PlaceCharacter();
        PlaceEnemy();
    }

    // Method 1: Inisialisasi semua cell menjadi tembok (1)
    void InitialiseMap()
    {
        map = new byte[width, depth];
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, z] = 1;  // 1 = wall, 0 = corridor
            }
        }
    }

    // Method 2: Generate lorong secara random
    public virtual void GenerateMaps()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Random.Range(0, 100) < 50)
                {
                    map[x, z] = 0;  // 0 = corridor
                }
            }
        }
    }

    // Method 3: Gambar maze berdasarkan array map
    void DrawMaps()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (map[x, z] == 1)
                {
                    Vector3 pos = new Vector3(x * scale, 0, z * scale);
                    GameObject wall = Instantiate(Cube[Random.Range(0, Cube.Count)], pos, Quaternion.identity);
                    wall.transform.localScale = new Vector3(scale, scale, scale);
                    wall.transform.position = pos;
                }
            }
        }
    }

    // Hitung jumlah tetangga kosong di sekitar cell (atas, bawah, kiri, kanan)
    public int CountSquareNeighbours(int x, int z)
    {
        int count = 0;

        if (x <= 0 || x >= width - 1 || z <= 0 || z >= depth - 1)
            return 5;

        if (map[x - 1, z] == 0) count++;
        if (map[x + 1, z] == 0) count++;
        if (map[x, z + 1] == 0) count++;
        if (map[x, z - 1] == 0) count++;

        return count;
    }

    public virtual void PlaceCharacter()
    {
        bool PlayerSet = false;
        for (int i = 0; i < depth; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int x = Random.Range(0, width);
                int z = Random.Range(0, depth);
                if (map[x, z] == 0 && !PlayerSet)
                {
                    Debug.Log("placing character");
                    PlayerSet = true;
                    Character.transform.position = new Vector3(x * scale, 0, z * scale);
                }
                else if (PlayerSet)
                {
                    Debug.Log("already Placing character");
                    return;
                }
            }
        }
    }

    public virtual void PlaceEnemy()
    {
        int EnemySet = 0;
        for (int i = 0; i < depth; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int x = Random.Range(0, width);
                int z = Random.Range(0, depth);
                if (map[x, z] == 0 && EnemySet != EnemyCount)
                {
                    Debug.Log("place Enemy");
                    EnemySet++;
                    Instantiate(Enemy, new Vector3(x * scale, 0, z * scale), Quaternion.identity);
                }
                else if (EnemySet == EnemyCount)
                {
                    Debug.Log("already Placing All the Enemy");
                    return;
                }
            }
        }
    }
}