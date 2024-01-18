using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ToolPhotoCamera : BaseWeapon
{
    
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private AudioSource photoAudio;
    [SerializeField] private AudioClip[] photoClips;
    [SerializeField] private Light photoLight;
    
    [SerializeField] private Transform photoAnimTarget;



    [SerializeField] private RawImage rawPhoto;
    [SerializeField] private Canvas canvasPhoto;
    
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

        // Create a new Texture2D
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height);
        
        // Read the pixels from the RenderTexture into the Texture2D
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = null;

        // Assign the captured frame to the RawImage
        rawPhoto.texture = texture2D;

        PhotoPath();
    }

    public override void AltShoot()
    {
        if (canZoomPhoto)
        {
            canvasPhoto.transform.DOLocalMove(new Vector3(-0.5f, 0.5f, -0.35f), 1f);
        }
        else
        {
            canvasPhoto.transform.DOLocalMove(new Vector3(-0.5f, -0.5f, 0.35f), 1f);
        }

        canZoomPhoto = !canZoomPhoto;

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
        canvasPhoto.transform.DOLocalMove(photoAnimTarget.localPosition, 1f);
    }

    void PhotoReset()
    {
        canvasPhoto.transform.localPosition = new Vector3(0f, 0f, 0f);
    }
    
    public override void DestroyModel()
    {
        rawPhoto.texture = null;
        
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
