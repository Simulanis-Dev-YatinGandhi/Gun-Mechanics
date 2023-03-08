using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class GunManager : MonoBehaviour
{
    //Gun Specs
    public float damage = 20f;
    public float fireRate = 1f;
    public int ScopeZoom = 25;

    //float delay;
    float shotdelay=0.25f;

    float a;
    float previousShot=0;
    float timeGap;
    
    
    public Camera fpscam;
   
     public GameObject scope;
    //public Camera scopecam;
    public ParticleSystem muzzle;
    public GameObject damageEffect;
    Animator atr;
  
    public int ammo;
    int ammoCapacity;
    
    //  public ParticleSystem damageEffect;// also works

    float range=100f;  //if i didnt added the float value raycast was not getting ON

    bool isReloaded = true;
    bool isScopeOn;
    bool isFirePressed = false;//should be removed find substitue
    bool isRateOfFire = true;
    private void Awake()
    {
        scope= GameObject.FindGameObjectWithTag("Scope");
    }

    // Update is called once per frame
    private void Start()
    {
        scope.SetActive(false); // for keeping scope off on start

        atr = GetComponent<Animator>();

        ammoCapacity = ammo;//getting the total ammo in start
        
        isScopeOn = true;
    }
    private void Update()
    {
        //if(Input.GetButtonDown("Fire1")) 
        if(Input.GetMouseButton(0)) 
           shoot();
       
        ProcessReload();

        ScopeOpen();
       
      
      if (Input.GetKeyDown(KeyCode.C))
        {
            //
        }
      
    }
    private void ProcessReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloaded = true;
            ammo = ammoCapacity;
        }
    }
    void ScopeOpen()
    {
        
        //tap tap logic created
        // no Extra Camera needed
        if (Input.GetKeyDown(KeyCode.G)||Input.GetMouseButtonDown(1))
        {
            if (isScopeOn)
            {
                //scopecam.gameObject.SetActive(true);
                scope.SetActive(true);
                scope.GetComponent<Camera>().fieldOfView = ScopeZoom;
                isScopeOn = false;
            }
            else
            {
                scope.SetActive(false);
                // scope.GetComponent<Camera>().fieldOfView = 60;
                isScopeOn = true;
            }
        }
    }

    private void shoot()    
    {
        FireRate(a = Time.time);

        RaycastHit GunRay; 
        int countFire = 1;

       // AmmoHandler(countFire);

        atr.SetBool("recoil", false);// called in float so as to set recoil bool to false and hence
        //no animation is played and when bool is true animation will play
        
        //if (Input.GetMouseButton(0)&&isReloaded)
        if ( isReloaded&&isRateOfFire)
        {
            

            AmmoHandler(countFire);
            isFirePressed = true;
         
                if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out GunRay))
                {
                    Debug.DrawLine(fpscam.transform.position, fpscam.transform.forward, Color.red);
                    //Debug.Log(GunRay.transform.name);
                    muzzle.Play();

                    //using bool for animator, will play recoil animation when true
                    atr.SetBool("recoil",true);

                    // very imp way to get the script reference thorough ray cast 
                    Targget tgt = GunRay.transform.GetComponent<Targget>();
                    //

                    tgt.score(damage);

                    //Debug.Log(GunRay.transform.position);
                    if (GunRay.transform.tag == "crate")
                        hitEffect(GunRay);
                  
            }
            
        }
        isFirePressed = false;
    }

    private void AmmoHandler(int countFire)
    {
        //ammo calculater
        if (isFirePressed)
        {
            ammo -= countFire;
            //Debug.Log(ammo);
            if (ammo <= 0)
            {
                isReloaded = false;

            }
            UiManager.instance.AmmoCount(ammo, isReloaded);
        }
        else
            UiManager.instance.AmmoCount(ammo, isReloaded);
    }

    private void hitEffect(RaycastHit GunRay)
    {
        Debug.Log("effect");
       GameObject dE= Instantiate(damageEffect, GunRay.transform.position, Quaternion.identity);
        //Instantiate(damageEffect, GunRay.point, Quaternion.identity); also can be used .point to get position in world space
        //damageEffect.Play(); no need to play just check play on awake and autometically played on instanstiate

        Destroy(dE, 2f);

        //Debug.Log("played");

    }
    void FireRate(float nextShot)
    {
        timeGap=nextShot - previousShot;
        if(shotdelay<timeGap)
        {
            isRateOfFire = true;
            Debug.Log("fire="+timeGap);

            previousShot = nextShot;//interseting logic if u think in seconds
        }
        else
        {
            isRateOfFire = false;
            Debug.Log("hold="+timeGap);
        }
       
    }
}
