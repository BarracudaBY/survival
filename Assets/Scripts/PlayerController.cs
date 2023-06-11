using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float MoveSpeed;
    public float SprintSpeed = 10f;
    public float DefaultSpeed;

    public Animator Anim;
    public float PickUpRange = 1.5f;

    private bool _facingRight = true;
    //public Weapon ActiveWeapon;

    public float BenzakTimer = 1f;
    private float BenzaTimeFlaing ;

    public List<Weapon> UnassignetWeapon, AssignetWeapon;

    public int MaxWeapons = 3;

    public SpriteRenderer spriteRenderer;
    private GhostController _ghostController;

    [HideInInspector]
    public List<Weapon> FullyLeveledWeapons = new List<Weapon>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _ghostController = GetComponent<GhostController>();
        _ghostController.enabled = false;

        if (AssignetWeapon.Count == 0)
        {
            AddWeapon(Random.Range(0, UnassignetWeapon.Count));

        }

        MoveSpeed = PlayerStatsController.Instance.MoveSpeed[0].Value;
        PickUpRange = PlayerStatsController.Instance.pickUpRange[0].Value;
        MaxWeapons = Mathf.RoundToInt(PlayerStatsController.Instance.MaxWeapons[0].Value);

        BenzaTimeFlaing = BenzakTimer;
        
    }

    private void Update()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        Sprint();

        transform.position += moveInput * MoveSpeed * Time.deltaTime;

        if(moveInput != Vector3.zero)
        {
            Anim.SetBool("IsMoving", true);
            _ghostController.enabled = true;

            if(moveInput.x > 0 && _facingRight)
            {
                Flip();
            }
            if(moveInput.x < 0 && !_facingRight)
            {
                Flip();
            }
        }
        else
        {
            Anim.SetBool("IsMoving", false);
            _ghostController.enabled = false;
        }

    }

    public void AddWeapon(int weaponNumber)
    {
        if(weaponNumber < UnassignetWeapon.Count)
        {
            AssignetWeapon.Add(UnassignetWeapon[weaponNumber]);
            UnassignetWeapon[weaponNumber].gameObject.SetActive(true);
            UnassignetWeapon.RemoveAt(weaponNumber);

        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);
        AssignetWeapon.Add(weaponToAdd);
        UnassignetWeapon.Remove(weaponToAdd);
    }

    public void Sprint()
    {
        if(BenzakController.Instance.CurrentBenzak > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                BenzaTimeFlaing -= Time.deltaTime;
                MoveSpeed = SprintSpeed;

                if(BenzaTimeFlaing <= 0)
                {
                    BenzakController.Instance.SpendBenzak(1);
                    BenzaTimeFlaing = BenzakTimer;
                }
            }
            else
            {
                // MoveSpeed = PlayerStatsController.Instance.MoveSpeed[0].Value;
                PlayerStatsController.Instance.GoToDefaultMoveSpeed();
            }
        }
        else
        {
            //MoveSpeed = PlayerStatsController.Instance.MoveSpeed[0].Value;
            PlayerStatsController.Instance.GoToDefaultMoveSpeed();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }
}
