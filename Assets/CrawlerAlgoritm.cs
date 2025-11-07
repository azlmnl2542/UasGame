using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerAlgoritm : MazeLogic
{
    public override void GenerateMaps()
    {
        bool done = false;
        int x = width / 2;
        int z = depth / 2;

        while (!done)
        {
            // tandai posisi saat ini sebagai jalan
            map[x, z] = 0;

            // Tentukan apakah bergerak di sumbu X atau Z
            if (Random.Range(0, 100) < 50)
            {
                // bergerak di sumbu X
                x += Random.Range(-1, 2);
            }
            else
            {
                // bergerak di sumbu Z
                z += Random.Range(-1, 2);
            }

            // cek apakah keluar batas map
            if (x < 0 || x >= width || z < 0 || z >= depth)
            {
                done = true;
            }
            else
            {
                done = false;
            }
        }
    }
}   