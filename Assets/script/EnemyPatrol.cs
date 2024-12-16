using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement2D : MonoBehaviour
{
    // Objek tujuan titik A dan B yang akan dituju oleh alien
    public GameObject pointA;
    public GameObject pointB;

    // Komponen Rigidbody2D dan Animator dari objek alien
    private Rigidbody2D rb;
    private Animator anim;

    // Titik tujuan saat ini
    private Transform currentPoint;

    // Kecepatan pergerakan alien
    public float speed;

    // Fungsi Start dijalankan sekali ketika objek diaktifkan
    void Start()
    {
        // Mendapatkan komponen Rigidbody2D yang terpasang pada game object ini
        rb = GetComponent<Rigidbody2D>();
        
        // Mendapatkan komponen Animator yang terpasang pada game object ini
        anim = GetComponent<Animator>();
        
        // Mengatur titik tujuan awal menjadi pointB
        currentPoint = pointB.transform;
        
        // Mengatur parameter animasi isRunning menjadi true untuk memulai animasi berlari
        anim.SetBool("siijo", true);
    }

    // Fungsi Update dipanggil sekali per frame
    void Update()
    {
        // Menghitung vektor arah dari posisi alien menuju titik tujuan saat ini
        Vector2 point = currentPoint.position - transform.position;

        // Jika titik tujuan saat ini adalah pointB, bergerak ke kanan
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        // Jika titik tujuan saat ini adalah pointA, bergerak ke kiri
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        // Mengecek jika jarak antara posisi alien dengan titik tujuan kurang dari 0.5 unit
        // dan titik tujuan saat ini adalah pointB, maka ubah titik tujuan menjadi pointA
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        // Mengecek jika jarak antara posisi alien dengan titik tujuan kurang dari 0.5 unit
        // dan titik tujuan saat ini adalah pointA, maka ubah titik tujuan menjadi pointB
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }

    // Fungsi untuk membalik arah alien dengan mengubah skala sumbu X
    private void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Fungsi untuk menggambar titik A dan B serta garis penghubung antara keduanya di editor Unity
    private void OnDrawGizmos()
    {
        // Menggambar bola di posisi pointA
        Gizmos.DrawSphere(pointA.transform.position, 0.5f);
        
        // Menggambar bola di posisi pointB
        Gizmos.DrawSphere(pointB.transform.position, 0.5f);
        
        // Menggambar garis penghubung antara pointA dan pointB
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
