using UnityEngine;

public class Goal : MonoBehaviour
{


    [SerializeField]
    private LevelManager levelManager;


    [SerializeField]
    private float distanceToGoal;
    void Awake()
    {
        levelManager.RegisterToLevelStart(i =>
        {
            Vector3 postion = transform.position;
            postion.x = distanceToGoal * (i + 1);
            transform.position = postion;
        });


    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Player")) levelManager.StartNextLevel();
    }
}
