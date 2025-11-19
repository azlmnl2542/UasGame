using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{

    private static System.Random rng = new System.Random(); // Membuat random generator dengan nama rng

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count; // menyimpan jumlah list yang belum di shuffle
        while (n > 1) // jika jumlah list masih lebih besar dari 1 (artinya masih bisa di shuffle)
        {
            n--; // mengurangi jumlah list setiap iterasi
            int k = rng.Next(n + 1); // membuar random index antara 0 dan jumlah list yang belum di shuffle

            // men tracking proses shuffle
            //Debug.Log($"Remaining items to shuffle: {n + 1}");
            //Debug.Log($"Random index chosen: {k}");
            //Debug.Log($"Swapping items: {list[k]} (at index {k}) with {list[n]} (at index {n})");

            //mengganti elemen
            T value = list[k]; //menyimpan item di RandomIndex Position
            list[k] = list[n]; //mengganti random value (k) dengan value n
            list[n] = value; //menaruh value dari k ke posisi n

            //men tracking list seterah di ganti
            //Debug.Log($"List after swap: {string.Join(", ", list)}");
        }
    }
}
