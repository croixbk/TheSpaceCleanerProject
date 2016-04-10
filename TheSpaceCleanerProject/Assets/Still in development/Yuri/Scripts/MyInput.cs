using UnityEngine;
using System.Collections;

public class MyInput : MonoBehaviour
{

    float touchDuration;
    Touch touch;
    bool doubleTapped = false;
    bool oneClick = false;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public bool getDoubleTouch()
    {
        /*
                #if UNITY_ANDROID
                print("asdad");
                if (Input.touchCount > 0)
                {
                    //if there is any touch
                    touchDuration += Time.deltaTime;
                    touch = Input.GetTouch(0);

                    //making sure it only check the touch once && it was a short touch/tap and not a dragging.
                    if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f)
                         StartCoroutine("singleOrDoubleTouch");
                }
                else if (doubleTapped == true){
                    doubleTapped = false;
                    touchDuration = 0.0f;
                    print("DOUBLE TAP");
                    return true;            
                }else {
                    touchDuration = 0.0f;
                }
                #endif
        */

        if (Input.GetMouseButtonDown(0))
        {
            if (!oneClick) // primeiro click
            {
                oneClick = true;
                /*ou touchTime*/touchDuration = Time.time; // salva o tempo atual
            }
            else
            {
                oneClick = false; // achou o double click, reseta
                PlayerMovement.testDirections += "\n\r DOUBLE CLICK";
            }
        }
        if (oneClick)
        {
            // se o tempo passado superou 0.2
            if ((Time.time - touchDuration) > 0.2f)
            {
                //reseta o onClick para que o proximo seja somente um click normal
                oneClick = false;
            }
        }   
            
        return false;
    }

    IEnumerator singleOrDoubleTouch()
    {
        yield return new WaitForSeconds(0.3f);
        if (touch.tapCount == 1)
            doubleTapped = false;
        else if (touch.tapCount == 2)
        {
            //this coroutine has been called twice. We should stop the next one here otherwise we get two double tap
            StopCoroutine("singleOrDouble");
            doubleTapped = true;
        }
    }

}