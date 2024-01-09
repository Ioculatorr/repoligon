using System;
using System.Collections;
using UnityEngine;

public class PhotoHandler : MonoBehaviour
{
    // //public Camera snapshotCamera;
    
private Sprite snapshotSprite;
[SerializeField] private GameObject targetPhoto;

    //
    // private Texture2D screenCapture;
    //
    // private void Start()
    // {
    //     screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    // }
    //
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         StartCoroutine(CaptureSnapshot());
    //     }
    // }
    //
    // IEnumerator CaptureSnapshot()
    // {
    //     yield return new WaitForEndOfFrame();
    //
    //     Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
    //     
    //     screenCapture.ReadPixels(regionToRead, 0, 0, false);
    //     screenCapture.Apply();
    //     ShowPhoto();
    // }
    //
    // void ShowPhoto()
    // {
    //     snapshotSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100f);
    //     targetPhoto.GetComponent<SpriteRenderer>().sprite = snapshotSprite;
    // }

    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private AudioSource photoAudio;
    [SerializeField] private AudioClip[] photoClips;
    [SerializeField] private Light photoLight;

    private void Start()
    {
        photoLight.enabled = false;
    }

    void Update()
    {
        // Example: Call ReadPixels when a specific key is pressed (e.g., space bar)
        if (Input.GetKeyDown(KeyCode.P))
        {
            PhotoSound();
            StartCoroutine(PhotoLight());
            Invoke("InvokeReadPixels", 0.05f);
        }
    }

    void InvokeReadPixels()
    {
        ReadPixelsFromRenderTexture(renderTexture);
    }

    void ReadPixelsFromRenderTexture(RenderTexture rt)
    {
        // Create a Texture2D to read pixels into
        Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

        // Set the target render texture
        RenderTexture.active = rt;

        // Read pixels from the render texture to the Texture2D
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();
        
        snapshotSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
        targetPhoto.GetComponent<SpriteRenderer>().sprite = snapshotSprite;

        // Reset the active render texture
        RenderTexture.active = null;
    }

    IEnumerator PhotoLight()
    {
        photoLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        photoLight.enabled = false;
    }

    void PhotoSound()
    {
        AudioClip clip = photoClips[UnityEngine.Random.Range(0, photoClips.Length)];
        photoAudio.PlayOneShot(clip);
    }
}

