using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip[] audioclips;

  [SerializeField]
  private PhysicsBody2D erabyBody;
    private float minVelocity =0f;
    private float maxVelocity =100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float velocity = Mathf.Abs(erabyBody.VelocityX);
        Mathf.InverseLerp()

        
    }
}
