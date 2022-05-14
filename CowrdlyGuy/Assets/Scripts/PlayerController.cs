using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private Animator animator;

    [SerializeField] 
    private DynamicJoystick dyJoystick;

    private float inputX;
    private float inputZ;
    private Vector3 movement;

    [SerializeField]
    private float moveSpeed;

    public int health = 3;

    public bool isAttack;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = dyJoystick.Horizontal;
        inputZ = dyJoystick.Vertical;

        if (!isAttack && health != 0)
        {

            if (inputX == 0 && inputZ == 0 && GameManager.instance.liveEnemiesCount > 0)
            {
                IdleAnimation();
            }
            if (inputX != 0 && inputZ != 0 && GameManager.instance.liveEnemiesCount > 0)
            {
                RunAnimation();
            }
            if (GameManager.instance.liveEnemiesCount == 0)
            {
                StartCoroutine(CelebrateWinning());
            }
        }
        if (isAttack && health != 0)
        {
            AttackAnimation();
        }
        if (health == 0)
        {
            DeadAnimation();
            GameManager.instance.playerIsLive = false;
        }
        Debug.Log(health);

    }

    private void FixedUpdate()
    {
        if(GameManager.instance.liveEnemiesCount > 0 && !isAttack && health != 0)
        {
            movement = new Vector3(inputX * moveSpeed, 0, inputZ * moveSpeed);
            characterController.Move(movement);

            // mesh rotate

            if (inputX != 0 || inputZ != 0)
            {
                Vector3 lookDir = new Vector3(movement.x, 0, movement.z);
                transform.rotation = Quaternion.LookRotation(lookDir);
            }
        }
    }

    private void RunAnimation()
    {
        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
    }

    private void IdleAnimation()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
    }

    private void AttackAnimation()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
    }

    private void DeadAnimation()
    {
        animator.SetBool("Dead", true);
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
    }

    IEnumerator CelebrateWinning()
    {
        yield return new WaitForSeconds(1f);

        animator.SetBool("Dance", true);
    }
}
