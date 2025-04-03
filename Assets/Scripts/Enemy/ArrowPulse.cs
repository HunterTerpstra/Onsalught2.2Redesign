using UnityEngine;

public class ArrowPulse : MonoBehaviour
{
    public float pulseSpeed = 1.2f;
    public float minScale = 0.9f;
    public float maxScale = 1.1f;
    
    private Vector3 originalScale;
    
    void Start()
    {
        originalScale = transform.localScale;
    }
    
    void Update()
    {
        // Only pulse when the game is paused (Time.timeScale == 0)
        if (Time.timeScale == 1f)
        {
            Debug.Log("Pulse");
            float scaleFactor = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.unscaledTime * pulseSpeed, 1f));
            transform.localScale = originalScale * scaleFactor;
        }
    }
}
