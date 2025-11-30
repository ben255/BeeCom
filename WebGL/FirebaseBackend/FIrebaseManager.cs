using System.Runtime.InteropServices;
using UnityEngine;
using System;

public class FirebaseManager : MonoBehaviour
{
    // Make this a Singleton to be easily accessible
    public static FirebaseManager Instance { get; private set; }

    // Event to notify subscribers when scores are received.
    public event Action<string> OnScoresReceivedEvent;

    // --- JSLIB IMPORTS ---
    [DllImport("__Internal")]
    private static extern void FirebaseRunner_Initialize(string gameObjectName, string callbackMethodName);

    [DllImport("__Internal")]
    private static extern void FirebaseRunner_SendLevel(int level, string userName);

    [DllImport("__Internal")]
    private static extern void FirebaseRunner_GetScores(string gameObjectName, string callbackMethodName);


    private bool isFirebaseInitialized = false;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make it persistent
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize Firebase only in WebGL builds
#if !UNITY_EDITOR && UNITY_WEBGL
        // Pass the name of this GameObject and the name of the method to be called back
        FirebaseRunner_Initialize(gameObject.name, "OnFirebaseInitialized");
#else
        Debug.Log("Firebase will not be initialized in the Unity Editor.");
#endif
    }


    /// <summary>
    /// This public method is called from JavaScript when initialization is complete.
    /// The parameter 'result' will be "Success" or an error message.
    /// </summary>
    public void OnFirebaseInitialized(string result)
    {
        if (result == "Success")
        {
            Debug.Log("Firebase initialization successful!");
            isFirebaseInitialized = true;

            // It's now safe to call other Firebase functions.
            // You might want to trigger an event here for other scripts to listen to.
        }
        else
        {
            Debug.LogError("Firebase initialization failed: " + result);
        }
    }

    /// <summary>
    /// A public wrapper to get scores.
    /// </summary>
    public void GetLeaderboardScores()
    {
        if (isFirebaseInitialized)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            // The 'OnScoresReceived' method will be called with the JSON result
            FirebaseRunner_GetScores(gameObject.name, "OnScoresReceived");
#endif
        }
        else
        {
            Debug.LogError("Cannot get scores, Firebase is not initialized yet.");
        }
    }

    /// <summary>
    /// A public wrapper to send a score.
    /// </summary>
    public void SendLevel(int level, string userName)
    {
        if (isFirebaseInitialized)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            FirebaseRunner_SendLevel(level, userName);
#endif
        }
        else
        {
            Debug.LogError("Cannot send level, Firebase is not initialized yet.");
        }
    }
    /// <summary>
    /// This public method is called from JavaScript with the leaderboard data.
    /// </summary>
    public void OnScoresReceived(string scoresJson)
    {
        if (string.IsNullOrEmpty(scoresJson) || scoresJson == "{}")
        {
            Debug.Log("No scores found.");
            return;
        }

        Debug.Log("Scores received: " + scoresJson);
        // Invoke the event to pass the score data to subscribers (like MenuController)
        OnScoresReceivedEvent?.Invoke(scoresJson);
    }
}
