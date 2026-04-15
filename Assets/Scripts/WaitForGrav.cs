using UnityEngine;

public class WaitForGrav : MonoBehaviour
{
    [SerializeField] GameObject[] objs;
    [SerializeField] float timer, waitTime = 5.0f;
    Rigidbody rb;
    bool gravHeld = true;
    void Update()
    {
        if(timer < waitTime)
        {
            timer += Time.deltaTime;
        }
        else if(gravHeld)
        {
            if(objs != null)
            {
                foreach(GameObject obj in objs)
                {
                    rb = obj.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                    }
                    rb = null;
                }
            }
            gravHeld = false;
        }
    }
}
