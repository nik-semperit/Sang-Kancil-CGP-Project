using UnityEngine;
public class CameraFollow_2 : MonoBehaviour
{
    public Transform target;
    public float smooth = 3f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 pos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, pos, smooth * Time.deltaTime);
        }
    }
}