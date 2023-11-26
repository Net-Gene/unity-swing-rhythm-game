using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

/// <summary>
/// GameLogic
/// </summary>
public class GameLogic : MonoBehaviour
{    
    /// <summary>
    /// time
    /// </summary>
    private float time;

    public static int score;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Start() 
    {

        time = Time.time;
    }
}
