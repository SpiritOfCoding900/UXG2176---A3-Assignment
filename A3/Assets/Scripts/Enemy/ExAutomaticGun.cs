using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAutomaticGun : MonoBehaviour
{
    public static ExAutomaticGun Instance;

    public GameObject Bullet;

    public float TimePerShot = 0.3f;//time to wait after a bullet
    public int ShotsPerBurst = 3;//fire 3 shots in a burst
    public float TimePerBurst = 0.5f;//wait this time after a burst
    public int MagazineSize = 20;//how many bullets the gun can store
    public float ReloadTime = 2;//how much time to reload a magazine

    public static float timer_;
    public static int bulletsLeft_;
    public static int currentShot_;

    //public ParticleSystem muzzleFlash;

    protected void Awake()
    {
        Instance = this;
    }

    protected void OnDestroy()
    {
        Instance = null;
    }

    void Start()
    {
        bulletsLeft_ = MagazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.HasOpened) return;

        AutomaticGun();
    }

    protected virtual void AutomaticGun()
    {
        timer_ -= Time.deltaTime;
        if (timer_ <= 0)
        {
            if (bulletsLeft_ > 0)//still have bullet to fire
            {
                if (currentShot_ < ShotsPerBurst)
                {
                    //Fire
                    //muzzleFlash.Play();
                    Instantiate(Bullet, transform.position, transform.rotation);
                    currentShot_++;
                    bulletsLeft_--;
                    timer_ = TimePerShot;
                }
                else //a full round is fired
                {
                    timer_ = TimePerBurst;
                    currentShot_ = 0;
                }
            }
            else //no more bullet, need to reload
            {
                timer_ = ReloadTime;
                currentShot_ = 0;//reset the burst
                bulletsLeft_ = MagazineSize;
            }
        }
    }
}