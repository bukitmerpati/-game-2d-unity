using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float currentPosx;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float verticalOffset; // Offset sumbu Y untuk posisi kamera
    [SerializeField] private float speed; // Kecepatan pergerakan kamera

    private float lookAhead;

    private void Update()
    {
        // Menentukan posisi target kamera, termasuk mengikuti posisi Y pemain dengan offset
        Vector3 targetPosition = new Vector3(
            player.position.x + lookAhead,
            Mathf.Lerp(transform.position.y, player.position.y + verticalOffset, Time.deltaTime * cameraSpeed), // Pergerakan vertikal halus
            transform.position.z
        );

        // Menggerakkan kamera secara halus menuju target posisi menggunakan SmoothDamp
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            speed
        );

        // Mengubah nilai lookAhead secara bertahap agar kamera mengikuti gerakan horizontal pemain dengan lancar
        lookAhead = Mathf.Lerp(
            lookAhead,
            (aheadDistance * player.localScale.x),
            Time.deltaTime * cameraSpeed
        );
    }

    public void MoveToNewBuilding(Transform _newBuilding)
    {
        currentPosx = _newBuilding.position.x;
    }
}
