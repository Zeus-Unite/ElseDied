using System;
using System.Collections;
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && move.x != 0 && !IsDashing)
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
        if (RightHalf())
        {
                transform.eulerAngles = new Vector3(0,90,0);
                playerModel.lastLookRight = true;
        }
        else if (LeftHalf())
        {
                transform.eulerAngles = new Vector3(0,-90,0);
                playerModel.lastLookRight = false;
        }
    }

    bool RightHalf()
    {
        return Input.mousePosition.x > Screen.width / 2.0f;
    }

    bool LeftHalf()
    {
        return Input.mousePosition.x < Screen.width / 2.0f;
    }

    IEnumerator DirectionDash(bool left)
    {
        float startTime = Time.time;
        Vector3 playerVel = left == true ? Vector3.left: Vector3.right;
        IsDashing = true;
        playerModel.dashSound.Play(this.transform.position);

        while (Time.time < startTime + playerModel.dashTime)
        {
            controller.Move(playerVel * playerModel.dashPower * Time.deltaTime);
            yield return null;
            continue;
        }

        yield return new WaitForSeconds(playerModel.dashTime / 2);

        IsDashing = false;
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
            StopAllCoroutines();
            var sim = Simulation.Schedule<EndLevel>();
            sim.forcedEnd = false;
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
