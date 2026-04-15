using Meta.XR;
using UnityEngine;

public class SceneCollisionDebug : MonoBehaviour
{
    public OVRInput.Button btn = OVRInput.Button.One;
    public Transform rayStartPt;
    public float rayLength = 5.0f;
    public EnvironmentRaycastManager envRayManager;
    public TMPro.TextMeshPro debugTextPrefab;
    void Start()
    {
        
    }
    void Update()
    {
        if(OVRInput.GetDown(btn))
        {
            Ray ray = new Ray(rayStartPt.position, rayStartPt.forward);

            bool hasHit = envRayManager.Raycast(ray, out var hit, rayLength);

            if (hasHit)
            {
                TMPro.TextMeshPro debugText = Instantiate(debugTextPrefab);
                Vector3 hitPt = hit.point;
                Vector3 hitNormal = hit.normal;

                debugText.transform.position = hitPt;
                debugText.transform.rotation = Quaternion.LookRotation(-hitNormal);

                debugText.text = "HIT";
            }        
        }
    }
}
