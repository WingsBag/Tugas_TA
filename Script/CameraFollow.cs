using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (!target)
        {
            Debug.LogWarning("CameraFollow: Target belum di-assign!");
            return;
        }

        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            -10f // pastikan kamera tetap di belakang
        );
    }
}