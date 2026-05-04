using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using UnityEngine;

public class SpawnBoomerang : MonoBehaviour
{
    [SerializeField] Transform boomerangSpawnLoc, pinSpawnLoc;
    [SerializeField] GameObject boomerangPrefab, pinPrefab;
    [SerializeField] ScoreKeeper scoreKeeper;
    [SerializeField] Button rangBtn, pinBtn;
    GameObject currentRang;
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
        if (pinPrefab != null && pinSpawnLoc != null)
        {
            Instantiate(pinPrefab,pinSpawnLoc);
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
