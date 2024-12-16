using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    

    // [Header] ini digunakan hanya untuk menampilkan label di Unity Editor agar terlihat rapi.
    [Header("Movement Parameters")]
    // Kecepatan gerakan pemain.
    [SerializeField] private float speed;

    // Kekuatan lompatan pemain.
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    // Coyote time adalah waktu tambahan setelah pemain "jatuh" dari platform, namun tetap bisa melompat.
    [SerializeField] private float coyotoTime;
    // Counter yang menghitung waktu coyote yang tersisa.
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    // Jumlah lompatan tambahan (double jump) yang bisa dilakukan pemain.
    [SerializeField] private int extraJumps;
    // Counter yang menghitung jumlah lompatan yang tersisa.
    private int jumpCounter;

    [Header("Layer")]
    // Layer untuk mendeteksi tanah (ground) agar tahu kapan pemain berada di tanah.
    [SerializeField] private LayerMask groundLayer;

    // Layer untuk mendeteksi dinding (wall) untuk keperluan wall jump.
    [SerializeField] private LayerMask wallLayer;

    [Header("Sounds")]
    // Suara yang akan dimainkan saat melompat.

    // Referensi ke Rigidbody2D untuk mengendalikan fisika karakter pemain.
    private Rigidbody2D body;

    // Referensi ke Animator untuk animasi karakter.
    private Animator anim;

    // Referensi ke BoxCollider2D untuk mendeteksi benturan fisik (misalnya, dengan tanah atau dinding).
    private BoxCollider2D boxCollider;

    // Cooldown setelah wall jump sebelum pemain bisa melakukan aksi lain.
    private float wallJumpCooldown;

    // Input gerakan horizontal dari pemain (kiri/kanan).
    private float horizontalInput;
    [Header("VFX Parameters")]
    // Referensi ke prefab VFX (misalnya, efek partikel saat lompat)
    [SerializeField] private GameObject jumpVFXPrefab;
    private bool wasGrounded = false;  // Menyimpan status sebelumnya apakah pemain di tanah atau tidak

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 20f;

    [SerializeField] private float knockbackDuration = 0.2f;  // Durasi knockback
    private bool isKnockedBack = false;  // Status pemain sedang knockback
// Faktor tambahan untuk knockback horizontal dan vertikal
[SerializeField] private float horizontalKnockbackMultiplier = 1.0f;
[SerializeField] private float verticalKnockbackMultiplier = 1.0f;



    private void Awake()
    {
        // Mendapatkan referensi komponen Rigidbody2D dan Animator dari objek pemain.
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Pastikan musuh memiliki tag "Enemy"
        {
            Debug.Log("Collided with: " + collision.gameObject.name + " on layer: " + collision.gameObject.layer);
            Debug.Log("Enemy hit!");
            anim.SetTrigger("hurt");  // Memanggil animasi hurt
            StartCoroutine(ApplyKnockback(collision));
        }
    }




private IEnumerator ApplyKnockback(Collision2D collision)
{
    isKnockedBack = true;  // Aktifkan status knockback

    // Dapatkan arah knockback (dari musuh ke pemain)
    Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

    // Terapkan faktor penyesuaian untuk knockback horizontal dan vertikal
    knockbackDirection.x *= horizontalKnockbackMultiplier;
    knockbackDirection.y *= verticalKnockbackMultiplier;

    // Terapkan knockback pada Rigidbody2D pemain
    body.velocity = new Vector2(knockbackDirection.x * knockbackForce, knockbackDirection.y * knockbackForce);

    // Nonaktifkan kontrol pemain selama knockback
    yield return new WaitForSeconds(knockbackDuration);

    isKnockedBack = false;  // Matikan status knockback
    Debug.Log("Knockback Direction: " + knockbackDirection);
    Debug.Log("Velocity: " + body.velocity);
    Debug.Log("Is Knocked Back: " + isKnockedBack);
}


    private void Update()

    {

         // Cek apakah sedang knockback
    if (isKnockedBack)
        return; // Mencegah input pemain saat knockback
    horizontalInput = Input.GetAxis("Horizontal");

    // Gerakan pemain berdasarkan input horizontal
    if (horizontalInput > 0.01f)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        // Pastikan suara "run" diputar berulang saat bergerak ke kanan
        if (!AudioManager.Instance.IsSFXPlaying("run"))
        {
            AudioManager.Instance.PlaySFX("run");
        }
    }
    else if (horizontalInput < -0.01f)
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

        // Pastikan suara "run" diputar berulang saat bergerak ke kiri
        if (!AudioManager.Instance.IsSFXPlaying("run"))
        {
            AudioManager.Instance.PlaySFX("run");
        }
    }
    else
    {
        // Hentikan suara "run" jika berhenti bergerak
        AudioManager.Instance.StopSFX("run");
    }


    // Pastikan Jump hanya dipanggil satu kali ketika tombol Space baru saja ditekan
    if (Input.GetKeyDown(KeyCode.Space) && (isGrounded() || coyoteCounter > 0 || jumpCounter > 0 || onWall()))
        Jump();

    // Animasi lari dan grounded
    anim.SetBool("run", horizontalInput != 0);
    anim.SetBool("grounded", isGrounded());

    // Logika untuk dinding dan lompatan dinding
    if (onWall())
    {
        body.gravityScale = 0;
        body.velocity = Vector2.zero;
    }
    else
    {
        body.gravityScale = 7;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        
        // Jika di tanah, reset coyote time dan jumpCounter
        if (isGrounded())
        {
            coyoteCounter = coyotoTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }
    horizontalInput = Input.GetAxis("Horizontal");

    // Cek apakah pemain di tanah
    bool isCurrentlyGrounded = isGrounded();

    // Jika pemain baru saja mendarat
    if (isCurrentlyGrounded && !wasGrounded)
    {
        // Memainkan efek suara landing
        AudioManager.Instance.PlaySFX("landing");
    }

    // Menyimpan status sebelumnya (grounded atau tidak)
    wasGrounded = isCurrentlyGrounded;
    // Logika wall jump cooldown
    if (wallJumpCooldown < 0.2f)
    {
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (onWall() && !isGrounded())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
        }
    }
    else
    {
        wallJumpCooldown += Time.deltaTime;
    }
}

    private void Jump()
    {
    // Memainkan efek suara lompatan
    AudioManager.Instance.PlaySFX("jump");

    // Coyote time memungkinkan pemain melompat setelah jatuh sebentar
    if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;

    // Lakukan wall jump jika pemain sedang di dinding
    if (onWall())
    {
        WallJump();
    }
    else
    {
        // Lakukan lompatan normal jika di tanah atau masih dalam coyote time
        body.velocity = new Vector2(body.velocity.x, jumpPower);

        // Reset coyote counter
        coyoteCounter = 0;

        // Kurangi jumlah lompatan yang tersisa jika menggunakan lompatan tambahan
        if (!isGrounded())
        {
            jumpCounter--;
        }

        // Tambahkan efek VFX lompat
        if (jumpVFXPrefab != null)
        {
            Vector3 vfxPosition = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z);
            GameObject vfxObject = Instantiate(jumpVFXPrefab, vfxPosition, Quaternion.identity);

            // Hancurkan efek setelah 1 detik
            Destroy(vfxObject, 0.2f);
        }
    }


        // Jika di tanah, reset kecepatan vertikal setelah lompatan.
    if (isGrounded())
{
    coyoteCounter = coyotoTime;
    jumpCounter = extraJumps;
}

        else if (onWall() && !isGrounded())
        {
            // Jika pemain berada di dinding dan ingin melakukan wall jump.
            if (horizontalInput == 0)
            {
                // Wall jump tanpa input arah.
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                // Wall jump dengan input arah.
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
        }
        Debug.Log("Jump triggered!");

    }

    // Fungsi khusus untuk wall jump.
    private void WallJump()
    {
        // Wall jump bisa disesuaikan di sini.
    }

    // Mengecek apakah pemain sedang berada di tanah menggunakan BoxCast.
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // Mengecek apakah pemain sedang menempel di dinding menggunakan BoxCast.
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    // Mengecek apakah pemain bisa melakukan serangan (misalnya, saat tidak bergerak dan di tanah).
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
    



}
