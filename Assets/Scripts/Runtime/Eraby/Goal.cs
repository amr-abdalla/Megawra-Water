using System;
using UnityEngine;

public class Goal : MonoBehaviour
{


    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private Transform goalTransform;


    [SerializeField]
    private float distanceToGoal;
    void Awake()
    {
        levelManager.RegisterToLevelStart(i =>
        {
            Vector3 postion = goalTransform.position;
            postion.x = distanceToGoal * Mathf.Pow(2, i);
            goalTransform.position = postion;
        });


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) levelManager.EndLevel(true);
    }
}
