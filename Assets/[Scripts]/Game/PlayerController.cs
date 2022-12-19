using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using EKTemplate;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public bool _Creative = false;
    public static int coinValue = 1;
    public float speedstrech = 20f;
    public Transform MidPlayer;
    public float runSpeed = 10f;
    public Material hairRed,hairblue;
    public Material ayakkabired,ayakkabýblue;
    public Material omuzlukred,omuzlukblue;
    public Material pantolonred,pantolonblue;
    public Material pelerinred,pelerinblue;
    public Material tshirtred,tshirtblue;
    public Material yelekred, yelekblue;
    public GameObject hair;
    public GameObject body;
    public Transform spawnPos;
    public GameObject armor1, armor2, armor3, armor4;
    [HideInInspector] public SplineFollower sF;
    [HideInInspector] public bool canMove = false;
    [HideInInspector] public bool isDead = false;
    public bool isSwerve = false;
    public float fireRate = 0.6f;
    public bool onLeft = false;
    #region Singleton
    public static PlayerController instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    private void Start()
    {
        sF = GetComponent<SplineFollower>();
        LevelManager.instance.startEvent.AddListener(OnGameStart);
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(fireRate);
        //transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Fire");

        //if (CharController.instance.isWater)
        //{
        //    Instantiate(Resources.Load("Waterball"), PlayerController.instance.spawnPos.position, Quaternion.identity);
        //}
        //else if (CharController.instance.isFire)
        //{
        //    Instantiate(Resources.Load("Fireball"), PlayerController.instance.spawnPos.position, Quaternion.identity);
        //}
        StartCoroutine(delay());
    }
    private void OnGameStart()
    {
        canMove=true;
        StartCoroutine(delay());
        transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Run");
        transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk"+Random.Range(1,5));
    }
    public int armorLevel = 0;
    public void CharacterArmors()
    {
        switch (armorLevel)
        {
            case 0:
                return;
            case 1:
                armor1.SetActive(true);
                return;
            case 2:
                armor2.SetActive(true);
                return;
            case 3:
                armor3.SetActive(true);
                return;
            case 4:
                armor4.SetActive(true);
                return;
        }
    }
    private void Update()
    {
        CharacterArmors();
        if (canMove == true)
        {
            if (!_Creative)
            {
                if (!isSwerve)
                {
                    Vector3 temp = MidPlayer.localPosition;
                    temp.x += InputManager.instance.input.x * Time.deltaTime * speedstrech;
                    temp.x = Mathf.Clamp(temp.x, -11f, 11f);
                    MidPlayer.localPosition = temp;

                    if (InputManager.instance.input.x > 0f && Input.GetMouseButton(0))
                    {
                        MidPlayer.transform.DOLocalRotate(new Vector3(0, 45, -0), 0.4f).SetEase(Ease.Linear);
                    }
                    else
                    {
                        MidPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.4f).SetEase(Ease.Linear);
                    }
                    if (InputManager.instance.input.x < 0f && Input.GetMouseButton(0))
                    {
                        MidPlayer.transform.DOLocalRotate(new Vector3(0, -30, 0), 0.4f).SetEase(Ease.Linear);
                    }
                    else
                    {
                        MidPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.4f).SetEase(Ease.Linear);
                    }
                }
            }
            else
            {
                Vector3 temp = MidPlayer.localPosition;
                temp.x -= InputManager.instance.input.x * Time.deltaTime * speedstrech;
                temp.x = Mathf.Clamp(temp.x, -11f, 11f);
                MidPlayer.localPosition = temp;

                if (InputManager.instance.input.x > 0f && Input.GetMouseButton(0))
                {
                    MidPlayer.transform.DOLocalRotate(new Vector3(0, -45, -0), 0.4f).SetEase(Ease.Linear);
                }
                else
                {
                    MidPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.4f).SetEase(Ease.Linear);
                }
                if (InputManager.instance.input.x < 0f && Input.GetMouseButton(0))
                {
                    MidPlayer.transform.DOLocalRotate(new Vector3(0, 30, 0), 0.4f).SetEase(Ease.Linear);
                }
                else
                {
                    MidPlayer.transform.DOLocalRotate(new Vector3(0, 0, 0f), 0.4f).SetEase(Ease.Linear);
                }
            }
            sF.follow = true;
        }
        else
        {
            sF.follow = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            StartCoroutine(Delay());
            transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Trap");
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(0.75f);
                transform.GetChild(0).GetComponentInChildren<Animator>().SetTrigger("Sk" + Random.Range(1, 5));
            }
        }
    }
 
   
    
}