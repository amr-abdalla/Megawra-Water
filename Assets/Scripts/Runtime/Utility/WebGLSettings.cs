using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebGLSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        #if !UNITY_EDITOR && UNITY_WEBGL
        // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
        //   WebGLInput.captureAllKeyboardInput = false;
        #endif
        
    }

}
