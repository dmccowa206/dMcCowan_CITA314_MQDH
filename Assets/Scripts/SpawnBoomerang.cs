using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using UnityEngine;

public class SpawnBoomerang : MonoBehaviour
{
    [SerializeField] Transform boomerangSpawnLoc, pinSpawnLoc;
    [SerializeField] GameObject boomerangPrefab, pinPrefab;
    [SerializeField] ScoreKeeper scoreKeeper;
    GameObject currentRang, currentPin;
    private void Start()
    {
        SpawnRang();
        SpawnPin();
    }
    public void SpawnRang()
    {
        if(currentRang != null)
        {
            Destroy(currentRang);
            currentRang = null;
        }
        if (boomerangPrefab != null && boomerangSpawnLoc != null)
        {
            currentRang = Instantiate(boomerangPrefab, boomerangSpawnLoc);
            scoreKeeper.ResetScore();
        }
    }
    public void SpawnPin()
    {
        if(currentPin != null)
        {
            Destroy(currentPin);
            currentPin = null;
        }
        if (pinPrefab != null && pinSpawnLoc != null)
        {
            currentPin = Instantiate(pinPrefab,pinSpawnLoc);
        }
    }
    void OnRangClick()
    {
        SpawnRang();
    }
    void OnPinClick()
    {
        SpawnPin();
    }
}
