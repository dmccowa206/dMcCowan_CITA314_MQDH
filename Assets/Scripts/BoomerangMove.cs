using UnityEngine;

public class BoomerangMove : MonoBehaviour
{
    [SerializeField] float spinSpd = 1.0f;
    public bool isThrown = false, trajSet = false;
    Vector3 trajectory, markStart, mark1, mark2, markEnd;
    ScoreKeeper scoreKeeper;
    Rigidbody rb, rbOther;
    float t, rngTime;
    Vector3 delta1 = new Vector3(1, .5f, 1), delta2 = new Vector3(1, 0, 1);
    private void Start()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(isThrown)
        {
            this.gameObject.transform.Rotate(transform.up * spinSpd);
        }
    }
    void MoveEllipse()
    {
        t += Time.deltaTime / rngTime;
        if (gameObject.transform.position == markEnd)
        {
            SetMarks();
        }
        Vector3 pointInCurve = 
        Vector3.Lerp(
            Vector3.Lerp(
                Vector3.Lerp(markStart, mark1, t),
                Vector3.Lerp(mark1, mark2, t), t),
            Vector3.Lerp(
                Vector3.Lerp(mark1, mark2, t),
                Vector3.Lerp(mark2, markEnd, t), t), t);
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.transform.parent.parent.gameObject.CompareTag("Pin"))
        {
            scoreKeeper.GainScore();
            rbOther = other.gameObject.GetComponentInParent<Rigidbody>();
            rbOther.isKinematic = false;
            Destroy(other.gameObject.transform.parent.parent.gameObject, 10f);
        }
    }
    void OnSelectEntered()
    {
        t = 0;
        isThrown = false;
        trajSet = false;
    }
    void OnSelectExited()
    {
        isThrown = true;
        rngTime = Random.Range(1.0f, 3.0f);
        trajectory = rb.linearVelocity;
        SetMarks();
    }
    void SetMarks()
    {
        markStart = gameObject.transform.position;
        mark1 = markStart + Vector3.Scale(trajectory, delta1);
        mark2 = mark1 + Vector3.Scale(trajectory, delta2);
        markEnd = mark1 + Vector3.Scale(trajectory, delta2);
    }
}
