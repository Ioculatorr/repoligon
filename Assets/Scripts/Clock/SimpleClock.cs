using UnityEngine;
using TMPro;

public class SimpleClock : MonoBehaviour
{
    private TextMeshProUGUI clockText;
    private int hours = 0;
    private int minutes = 0;
    private int seconds = 0;

    [SerializeField] private float timeScale = 1.0f; // Time scale multiplier
    [SerializeField] private float updateInterval = 1f; // Update interval for the clock display
    [SerializeField] private int maxHours = 24; // Maximum hours before resetting
    
    [SerializeField] private AudioClip hourChangeSound; // Sound to play when hour changes
    
    private int lastHour = -1; // Track the last hour value

    void Start()
    {
        // Get the TextMeshProUGUI component attached to the GameObject
        clockText = GetComponent<TextMeshProUGUI>();

        // Start the coroutine to update the clock
        StartCoroutine(UpdateClock());
    }

    System.Collections.IEnumerator UpdateClock()
    {
        while (true)
        {
            // Calculate hours, minutes, and seconds
            float currentTime = Time.time * timeScale;
            hours = Mathf.FloorToInt(currentTime / 3600) % maxHours;
            minutes = Mathf.FloorToInt((currentTime % 3600) / 60);
            seconds = Mathf.FloorToInt(currentTime % 60);

            // Format the time as a string (HH:mm:ss)
            string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

            // Update the TextMeshProUGUI component to display the current time
            clockText.text = timeString;
            
            
            // Check if the hour has changed since the last update
            if (hours != lastHour)
            {
                lastHour = hours;
                // Play the hour change sound
                if (hourChangeSound != null)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(hourChangeSound);
                }
            }

            // Wait for the specified update interval
            yield return new WaitForSeconds(updateInterval);
        }
    }
}