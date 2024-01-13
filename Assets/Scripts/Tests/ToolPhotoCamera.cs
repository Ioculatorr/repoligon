using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToolPhotoCamera : BaseWeapon
{
    
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private AudioSource photoAudio;
    [SerializeField] private AudioClip[] photoClips;
    [SerializeField] private Light photoLight;
    
    [SerializeField] private GameObject targetPhoto;
    [SerializeField] private Transform photoAnimTarget;
    
    
    


    private Sprite snapshotSprite;
    private bool canTakePhoto = true;
    private bool canZoomPhoto = false;

    
    public override void Shoot()
    {
        if (canTakePhoto)
        {
            PhotoSound();
            PhotoReset();
            StartCoroutine(PhotoLight());
            ReadPixelsFromRenderTexture(renderTexture);
            canTakePhoto = false;
            
            StartCoroutine(PhotoWaitTime());
            canZoomPhoto = true;
        }
    }
    
    void ReadPixelsFromRenderTexture(RenderTexture rt)
    {
        targetPhoto.GetComponent<SpriteRenderer>().enabled = true;
        // Create a Texture2D to read pixels into
        Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

        // Set the target render texture
        RenderTexture.active = rt;

        // Read pixels from the render texture to the Texture2D
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();
        
        snapshotSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
        targetPhoto.GetComponent<SpriteRenderer>().sprite = snapshotSprite;
        PhotoPath();

        // Reset the active render texture
        RenderTexture.active = null;
    }

    public override void AltShoot()
    {
        if (canZoomPhoto)
        {
            targetPhoto.transform.DOLocalMove(new Vector3(-0.5f, 0.5f, -0.35f), 1f);
        }
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

    IEnumerator PhotoWaitTime()
    {
        yield return new WaitForSeconds(4f);
        canTakePhoto = true;
        canZoomPhoto = false;
    }

    public override void PlayPrefabEffects()
    {
        weaponAudio.Play();
    }

    void PhotoPath()
    {
        targetPhoto.transform.DOLocalMove(photoAnimTarget.localPosition, 1f);
    }

    void PhotoReset()
    {
        targetPhoto.transform.localPosition = new Vector3(0f, 0f, 0f);
    }
    
    public override void DestroyModel()
    {
        targetPhoto.GetComponent<SpriteRenderer>().enabled = false;
        
        // Access spawnedPrefab from the BaseWeapon class
        base.DestroyModel();
    }


    public override void LifeRestart()
    {}
    public override void LifeChangeMind()
    {}
    public override void BulletEmit(Vector3 hitPoint)
    {}
    public override void SpawnHitParticleEnemy(Vector3 hitPoint)
    {}
    public override void SpawnHitParticle(Vector3 hitPoint)
    {}
}
