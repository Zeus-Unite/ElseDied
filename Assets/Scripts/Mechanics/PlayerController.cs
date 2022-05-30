using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHealth
{
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        transform.Rotate(0, 90, 0, Space.Self);
    }

    public bool groundedPlayer;

    public bool IsDashing = false;
    public bool BuildingOpen = false;

    public bool controlsDisabled = true;

    readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();

    CharacterController controller;
    Vector3 playerVelocity;
    Vector3 move;
    bool walkRight;
    bool canDash = true;

    void Update()
    {
        if (controlsDisabled)
            return;

        bool ground = controller.isGrounded;

        groundedPlayer = ground;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        bool facingLeft = move.x < 0 ? true : false;

        if (Input.GetKeyDown(KeyCode.LeftShift) && move.x != 0 && canDash)
        {
            StartCoroutine("DirectionDash", facingLeft);
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerModel.jumpSound.Play(this.transform.position);
            playerVelocity.y += Mathf.Sqrt(playerModel.jumpHeight * -3.0f * playerModel.gravityValue);
        }

        if (Input.GetButtonUp("Jump") && !groundedPlayer && playerVelocity.y > 0)
        {
            playerVelocity.y = .2f;
            groundedPlayer = false;
        }

        CheckLookDirection();
    }

    void CheckLookDirection()
    {
        if (move.x > 0)
        {
            if (!playerModel.lastLookRight && !walkRight)
            {
                walkRight = true;
                transform.Rotate(0, -180, 0, Space.World);
            }

            playerModel.lastLookRight = true;
            return;
        }

        if (playerModel.lastLookRight && walkRight)
        {
            walkRight = false;
            transform.Rotate(0, 180, 0, Space.World);
        }

        if (move.x < 0)        
            playerModel.lastLookRight = false;

        //This feels a bit weird, that is one of the moments u usually want to use a else if statement, which kinda makes sense.
        //The following Code is 
        //  if (move.x > 0)
        //      playerModel.lastLookRight = true;
        //  else if (move.x < 0)
        //      playerModel.lastLookRight = false;
        // I would use this Version aswell, but this Project is completly focused about no else!
    }

    IEnumerator DirectionDash(bool left)
    {
        float startTime = Time.time;
        Vector3 playerVel = left == true ? Vector3.left: Vector3.right;
        canDash = false;
        IsDashing = true;
        playerModel.dashSound.Play(this.transform.position);
        playerModel.DashFillImage.fillAmount = 0;

        while (Time.time < startTime + playerModel.dashTime)
        {
            controller.Move(playerVel * playerModel.dashPower * Time.deltaTime);
            yield return null;
            continue;
        }

        yield return new WaitForSeconds(playerModel.dashTime / 2);


        IsDashing = false;

        //Cooldown, Very Clear, Break the Loop if the Objective is reached
        float cooldown = 0;

        while (!canDash)
        {
            cooldown += Time.deltaTime;

            if(cooldown > 2)
            {
                playerModel.DashFillImage.fillAmount = 1;
                canDash = true;
                yield return null;
                break;
            }

            playerModel.DashFillImage.fillAmount = cooldown / 2;
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if (controlsDisabled)
            return;

        if (move != Vector3.zero)
            controller.Move(move * Time.fixedDeltaTime * playerModel.playerSpeed);

        playerVelocity.y += playerModel.gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);



        CheckForHeadCollision();
    }

    void CheckForHeadCollision()
    {
        int layerMask = 1 << 10;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, playerModel.head, layerMask))
        {
            if (hit.distance < playerModel.head)
            {
                playerVelocity.y = -.5f;
                groundedPlayer = false;
            }
        }
    }

    public void HandleHealth(int amount)
    {
        playerModel.healthSystem.actualHealth += amount;
        playerModel.healthSystem.TriggerOnHealth();

        if (playerModel.healthSystem.actualHealth <= 0)
        {
            //Unit Death
            canDash = true;
            StopAllCoroutines();
            Simulation.Schedule<EndLevel>();
            return;
        }

        Simulation.Schedule<PlayerKill>();
    }

    public void Teleport(Vector3 pos)
    {
        playerVelocity = Vector3.zero;
        move = Vector3.zero;
        this.transform.position = pos;
    }


    public void KillPlayer(Action Dashed)
    {
        if(IsDashing)
        {
            Dashed.Invoke();
            return;
        }

        playerModel.dieSound.Play(this.transform.position);
        HandleHealth(-1);
    }

    void RotatePlayerAround()
    {
        if (transform.rotation.y < 0)
        {
            walkRight = true;
            transform.Rotate(0, 180, 0, Space.World);
            return;
        }

        walkRight = false;
        transform.Rotate(0, -180, 0, Space.World);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Enemy")
        {
            hit.transform.TryGetComponent(out EnemyController em);
            if (em != null)
            {
                KillPlayer(() => em.KillEnemy());
            }

            return;
        }
    }
}
