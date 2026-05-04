using UnityEngine;

public class UICamera : MonoBehaviour
{
    [SerializeField] Camera centerCam;
    [SerializeField] float offsetZ;
    private void Update()
    {
        this.gameObject.transform.position = centerCam.transform.position + (centerCam.transform.forward * offsetZ);
        this.gameObject.transform.rotation = centerCam.transform.rotation;
    }
}
