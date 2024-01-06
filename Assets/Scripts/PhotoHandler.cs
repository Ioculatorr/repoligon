using System.Collections;
using UnityEngine;

public class PhotoHandler : MonoBehaviour
{
    //public Camera snapshotCamera;
    private Sprite snapshotSprite;
    [SerializeField] private GameObject targetPhoto;

    private Texture2D screenCapture;
    
    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(CaptureSnapshot());
        }
    }

    IEnumerator CaptureSnapshot()
    {
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        snapshotSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100f);
        targetPhoto.GetComponent<SpriteRenderer>().sprite = snapshotSprite;
    }
}

