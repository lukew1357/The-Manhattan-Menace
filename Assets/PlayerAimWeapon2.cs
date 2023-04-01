using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;
using TMPro;

public class PlayerAimWeapon2 : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    public GameObject bulletPrefab;
    public Transform muzzleTransform;
    public float coolDown = 0.2f;
    public float reloadCoolDown = 1.0f;
    public float reloadHintCoolDown = 10.0f;
    public bool projectileMode = false;
    public Transform playerTransform;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadHintText;

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;

    private float nextFireTime = 0.0f;
    private float reloadHintTime = 0.0f;

    private int magAmmo = 12;
    private int magSize = 12;
    private int ammo = 48;
    private int reloadHintTrigger = 1;

    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private Animator muzzleFlashAnimator;

    public bool laserEquipped;

    DataScript dataScript;
    public GameObject dataHandler;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimGunEndPointTransform = aimTransform.Find("BulletSpawn");
        laserEquipped = true;
        dataHandler = GameObject.FindGameObjectWithTag("DataHandler");
        dataScript = dataHandler.GetComponent<DataScript>();
    }

    private void Update()
    {
        if (!dataScript.Paused)
        {
            HandleAiming();
            HandleShooting();
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = ((mousePosition - playerTransform.position)).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }
        aimTransform.localScale = aimLocalScale;
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime && !dataScript.usingClub)
        {

            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            nextFireTime = Time.time + coolDown;

            Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);

            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
        }
        if (dataScript.usingClub)
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            Instantiate(bulletPrefab, gunPoint.position, Quaternion.identity);

            OnShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });
        }

    }
}
