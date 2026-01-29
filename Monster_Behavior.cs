using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.XR.GoogleVr;

public class Monster_Behavior : NetworkBehaviour
{
    [Header("Monster Settings")]

    public bool isMonsterAActive = true;
    public bool MonsterLocation1 = true; // Example variable to control monster location

    public bool isMonsterBActive = false; // Example for another monster
    public bool isMonsterCActive = false; // Example for another monster

    [Header("Chance Timer Settings")]
    [Tooltip("Time between each chance check in seconds")]
    public float MonsterAinterval = 15f; // Time between each chance check (seconds)
    public float MonsterBinterval = 15f; // Time between each chance check (seconds)
    public float MonsterCinterval = 15f; // Time between each chance check (seconds)
    public float time = 1f; // Time between each chance check (seconds)

    [Header("Chance Settings")]
    [Tooltip("Percentages for each action")]
    [Range(0, 100)] public int FirstStep = 50; // Example chance for the first step (50%)
    [Range(0, 100)] public int MonsterAAction1 = 50; // 50%
    [Range(0, 100)] public int MonsterAAction2 = 25; // 25%
    [Range(0, 100)] public int MonsterAAction3 = 5; // 5%
//--------------------------------------------------------------------------------
    [Range(0, 100)] public int MonsterBAction1 = 50; // 50%
    [Range(0, 100)] public int MonsterBAction2 = 25; // 25%
    [Range(0, 100)] public int MonsterBAction3 = 5; // 5%
//--------------------------------------------------------------------------------
    [Range(0, 100)] public int MonsterCAction1 = 50; // 50%
    [Range(0, 100)] public int MonsterCAction2 = 25; // 25%
    [Range(0, 100)] public int MonsterCAction3 = 5; // 5%
//--------------------------------------------------------------------------------

    public bool DayHasStarted = false; // Example variable to control when the day starts
    public int dayduration = 0; 

    private void Start()
    {

    }

    private void Update()
    {
        if (isMonsterAActive)
        {
            StartCoroutine(MonsterAActionSequence());
        }
        else
        {
            StopCoroutine(MonsterAActionSequence());
        }
        if (isMonsterBActive)
        {
            StartCoroutine(MonsterBActionSequence());
        }
        else
        {
            StopCoroutine(MonsterBActionSequence());
        }

        if (DayHasStarted == true)
        {
            StartCoroutine(DayStartLoop());
        }
        else
        {
            StopCoroutine(DayStartLoop());
        }

    }

    private IEnumerator MonsterAActionSequence()
    {
        yield return new WaitForSeconds(MonsterAinterval);
        float randomValue = Random.Range(0f, 100f);
        if (isMonsterAActive == false)
        {
            yield break; // Exit if Monster A is not active
        
        }
        if (randomValue < MonsterAAction1)
        {
            // Execute Monster A Action 1
        }
        else if (randomValue < MonsterAAction2 + MonsterAAction1)
        {
            // Execute Monster A Action 2
        }
        else if (randomValue < MonsterAAction3 + MonsterAAction2 + MonsterAAction1)
        {
            // Execute Monster A Action 3
        }
        else
        {
            // Execute Monster A Action 4
        }

    }

    private IEnumerator MonsterBActionSequence()
    {
        yield return new WaitForSeconds(MonsterBinterval);
        float randomValue = Random.Range(0f, 100f);
        if (randomValue < MonsterBAction1)
        {
            // Execute Monster B Action 1
        }
        else if (randomValue < MonsterBAction2 + MonsterBAction1)
        {
            // Execute Monster B Action 2
        }
        else if (randomValue < MonsterBAction3 + MonsterBAction2 + MonsterBAction1)
        {
            // Execute Monster B Action 3
        }
        else
        {
            // Execute Monster B Action 4
        }
    }

    private IEnumerator DayStartLoop()
    {
        while (DayHasStarted)
        {
            dayduration += 1;
            yield return new WaitForSeconds(time);
        }
    }
}
