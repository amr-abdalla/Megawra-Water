using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnim : MonoBehaviour
{
    
    [SerializeField]
    SpriteRenderer spriteRenderer = null;
    
    const float t =  0.4f;
    const float a = 15f;
    void Start()
    {
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(animRoutine());
    }

    IEnumerator animRoutine(){
        while(true){
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, a);
            yield return new WaitForSeconds(t);
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, -a);
            yield return new WaitForSeconds(t);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
