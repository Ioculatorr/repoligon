using UnityEngine;
using UnityEngine.Events;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Transform shootingPointRaycast;

    [SerializeField] private BaseWeaponData currentWeapon;

    [SerializeField] private UnityEvent onShoot;

    void Start()
    {
        // Example: Set the starting weapon (you can do this in the Inspector as well)
        SetCurrentWeapon(ScriptableObject.CreateInstance<PistolData>());
    }

    void Update()
    {
        // Check for user input to trigger shooting
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= currentWeapon.fireRate)
        {
            Shoot();
        }

        ChooseWeapon();
    }

    void Shoot()
    {
        // Call the Shoot method of the current weapon
        currentWeapon.Shoot(shootingPoint, shootingPointRaycast);

        // Invoke the shoot trigger event
        onShoot.Invoke();
    }

    void SetCurrentWeapon(BaseWeaponData newWeapon)
    {
        currentWeapon = newWeapon;
    }

    void ChooseWeapon()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SetCurrentWeapon(ScriptableObject.CreateInstance<PistolData>());
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            SetCurrentWeapon(ScriptableObject.CreateInstance<ShotgunData>());
        }
    }
}
