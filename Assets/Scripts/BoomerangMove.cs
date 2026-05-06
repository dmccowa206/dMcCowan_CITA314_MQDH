using UnityEngine;

public class BoomerangMove : MonoBehaviour
{
    [SerializeField] float spinSpd = 1.0f;
    public bool isThrown = false, trajSet = false;
    Vector3 trajectory, markStart, mark1, mark2, markEnd;
    ScoreKeeper scoreKeeper;
    Rigidbody rb, rbOther;
    float t, rngTime;
    Vector3 delta1 = new Vector3(.431f, 0, .902f), delta2 = new Vector3(-.124f, 0, .992f), deltaEnd = new Vector3(-.874f, 0, .486f);
    Vector3 retDelta1 = new Vector3(-.814f,0,-.486f), retDelta2 = new Vector3(-.124f,0,-.992f);
    bool loopCheck = false;
    Vector3 startLoc, invertTraj = new Vector3(-1,1,-1);
    [SerializeField] Transform player;
    UIManager uiMan;
    private void Start()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        rb = GetComponent<Rigidbody>();
        player = FindFirstObjectByType<AudioListener>().gameObject.transform;
        uiMan = FindAnyObjectByType<UIManager>();
    }
    void Update()
    {
        if(isThrown && trajSet)
        {
            this.gameObject.transform.Rotate(transform.up * spinSpd);
            MoveEllipse();
        }
    }
    void MoveEllipse()
    {   
        Debug.Log("MoveEllipse");
        t += Time.deltaTime / rngTime;
        if (gameObject.transform.position == markEnd)
        {
            if(loopCheck)
            {
                Fall();
            }
            else
            {
                SetReturnMarks();
                t = 0;
            }
        }
        Vector3 pointInCurve = 
        Vector3.Lerp(
            Vector3.Lerp(
                Vector3.Lerp(markStart, mark1, t),
                Vector3.Lerp(mark1, mark2, t), t),
            Vector3.Lerp(
                Vector3.Lerp(mark1, mark2, t),
                Vector3.Lerp(mark2, markEnd, t), t), t);
        gameObject.transform.position = pointInCurve;
    }
    // private void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log("Collision: " + other.gameObject.name);
    //     if(other.gameObject.CompareTag("Pin"))
    //     {
    //         Debug.Log("isPin");
    //         scoreKeeper.GainScore();
    //         rbOther = other.gameObject.GetComponent<Rigidbody>();
    //         rbOther.isKinematic = false;
    //         Destroy(other.gameObject, 3f);
    //     }
    //     // else
    //     // {
    //     //     Fall();
    //     // }
    // }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        if(other.gameObject.CompareTag("Pin"))
        {
            Debug.Log("isPinTrigger");
            scoreKeeper.GainScore();
            rbOther = other.gameObject.GetComponent<Rigidbody>();
            rbOther.isKinematic = false;
            rbOther.useGravity = true;
            Destroy(other.gameObject, 3f);
        }
        // else
        // {
        //     Fall();
        // }
    }
    public void OnGrab()
    {
        Debug.Log("Grab");
        t = 0;
        isThrown = false;
        trajSet = false;
        scoreKeeper.ResetElligibility();
        if (uiMan != null)
        {
            uiMan.ToggleUI(false);
        }
    }
    public void OnThrow()
    {
        Debug.Log("Throw");
        if (player != null)
        {
            startLoc = player.position;
        }
        else
        {
            startLoc = gameObject.transform.position;
        }
        isThrown = true;
        loopCheck = false;
        rb.isKinematic = false;
        rngTime = Random.Range(1.5f, 2.5f);
        Invoke("SetTraj", .05f);
    }
    void SetMarks()
    {
        markStart = gameObject.transform.position;
        mark1 = markStart + Vector3.Scale(trajectory, delta1);
        mark2 = mark1 + Vector3.Scale(trajectory, delta2);
        markEnd = mark1 + Vector3.Scale(trajectory, deltaEnd);
        Debug.Log("Marks: " + markStart + " | " + mark1 + " | " + mark2 + " | " + markEnd);
    }
    void SetReturnMarks()
    {
        markStart = gameObject.transform.position;
        mark1 = markStart + Vector3.Scale(trajectory, retDelta1);
        mark2 = mark1 + Vector3.Scale(trajectory,retDelta2);
        markEnd = startLoc;
        Debug.Log("Marks: " + markStart + " | " + mark1 + " | " + mark2 + " | " + markEnd);
        loopCheck = true;
    }
    void Fall()
    {
        isThrown = false;
        trajSet = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(Vector3.Scale(trajectory, invertTraj));
        Invoke("FailedReGrab", 3f);
    }
    void SetTraj()
    {
        trajectory = rb.linearVelocity;
        rb.isKinematic = true;
        trajSet = true;
        SetMarks();
    }
    void FailedReGrab()
    {
        if (uiMan != null && rb.isKinematic == false)
        {
            uiMan.ToggleUI(true);
        }
    }
}
