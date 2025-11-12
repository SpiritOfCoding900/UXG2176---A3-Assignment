using UnityEngine;
using static TurrentScript;

public class TurrentGun : ExAutomaticGun
{
    protected override void AutomaticGun()
    {
        if (TurrentScript.Instance.turrentState == TurrentStates.Shoot)
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
                        AudioManager.Instance.PlaySound(SoundID.TurrentShot);
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
}
