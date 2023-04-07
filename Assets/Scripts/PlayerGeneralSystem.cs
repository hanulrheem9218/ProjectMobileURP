using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerGeneralSystem : MonoBehaviour
{
    private MovementJoystick _movementJoyStick;
    public Animator playerAnimator;
    private float inputX;
    private float inputZ;

    public float isPlayerMoving = -1;
    //public float animationSpeed = 0.01f;
    public Transform playerObject;

    [SerializeField]
    public bool isAnimationEnd; //{ private set; get; }
    public bool isAttackEnd;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] public LayerMask targetMask;
    [SerializeField] public float attackRange;
    [SerializeField] Vector3 slopeDirection;
    RaycastHit slopeHit;
    [SerializeField] private float maxSlope;
    [SerializeField] private float playerHeight;
    [SerializeField] private GameObject test;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 playerRotation;
    public bool isInputDisabled;
    public Coroutine combatCoroutine;
    private CapsuleCollider playerCollider;
    private DefaultTouch defaultTouch;
    private ActionControls actionControls;
    [SerializeField] Transform targetObject;

    // Start is called before the first frame update
    void Start()
    {
        isAnimationEnd = true;
        isAttackEnd = true;
        playerObject = transform;
        this._movementJoyStick = FindObjectOfType<MovementJoystick>();
        this.playerAnimator = GetComponent<Animator>();
        this.defaultTouch = FindObjectOfType<DefaultTouch>();
        this.actionControls = FindObjectOfType<ActionControls>();
        //  movementSpeed = 5.5f;
        isInputDisabled = false;

        // setup the health ui here and stamina ui here

    }

    private bool OnSlope()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, (playerHeight * 0.5f + 0.3f)))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlope && angle != 0;
        }
        return false;
    }

    // Update is called once per frame
    private void Update()
    {
        targetObject = defaultTouch.targetObject;
        inputX = _movementJoyStick.inputHorizontal();
        inputZ = _movementJoyStick.inputVertical();
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);
        //   print(": " + isGrounded);
        slopeDirection = Vector3.ProjectOnPlane(transform.forward, slopeHit.normal).normalized;
        transform.GetComponent<Rigidbody>().useGravity = !OnSlope();
        // print("Check Sloop : " + OnSlope());
        actionControls.SetInteraction(targetObject != null && targetObject.gameObject.layer == 10);
        print(targetObject != null && targetObject.gameObject.layer == 10);
    }
    void FixedUpdate()
    {
        if (transform.GetComponent<Rigidbody>().velocity.y > 0)
        {
            transform.GetComponent<Rigidbody>().AddForce(Vector3.down * 80f, ForceMode.Acceleration);
        }
        if (!isInputDisabled)
        {
            if ((inputX != 0 || inputZ != 0) && isGrounded && !OnSlope())
            {
                // transform.GetComponent<Rigidbody>().useGravity = true;
                playerRotation = new Vector3(inputX * Time.deltaTime, 0, inputZ * Time.deltaTime);
                playerObject.transform.rotation = Quaternion.LookRotation(playerRotation);
                transform.GetComponent<Rigidbody>().AddForce(playerObject.transform.forward.normalized * 100f * movementSpeed, ForceMode.Acceleration);
                playerAnimator.SetBool("isRun", true);
            }
            else if ((inputX != 0 || inputZ != 0) && isGrounded && OnSlope())
            {
                // print("YES ON SLOPE");
                //  transform.GetComponent<Rigidbody>().useGravity = false;
                playerRotation = new Vector3(inputX * Time.deltaTime, 0, inputZ * Time.deltaTime);
                playerObject.transform.rotation = Quaternion.LookRotation(playerRotation);
                transform.GetComponent<Rigidbody>().AddForce(slopeDirection * 100f * movementSpeed, ForceMode.Acceleration);
                playerAnimator.SetBool("isRun", true);
            }
            else
            {
                playerAnimator.SetBool("isRun", false);
            }
        }
    }

    public bool isPushForward;
    public Coroutine waitForPushForward;
    public void PushForward(float moveAmount)
    {
        isPushForward = true;
        if (waitForPushForward != null)
        {
            StopCoroutine(waitForPushForward);
        }
        waitForPushForward = StartCoroutine(MovingForward(moveAmount));
    }

    public void TEST()
    {
        transform.GetComponent<Rigidbody>().AddForce(playerObject.transform.forward * 100f, ForceMode.Impulse);
    }
    public IEnumerator MovingForward(float moveAmount)
    {

        while (isPushForward)
        {
            // isInputDisabled = true;
            transform.GetComponent<Rigidbody>().AddForce(playerObject.transform.forward * 100f * moveAmount, ForceMode.Acceleration);
            print("keep starting");
            yield return null;

        }
    }
    public void DisableInput(float combatDelaySecond)
    {
        if (combatCoroutine != null)
        {
            StopCoroutine(combatCoroutine);
        }
        combatCoroutine = StartCoroutine(InputCooldown(combatDelaySecond));
    }
    private IEnumerator InputCooldown(float delaySecond)
    {
        isInputDisabled = true;

        yield return new WaitForSeconds(delaySecond);
        //   print("player Mode DISABLED");
        isInputDisabled = false;
        isPushForward = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "hr")
        {
            FindObjectOfType<InGamePlaySystemUI>().ShowCinematic();
            Invoke(nameof(temp), 0.8f);
            // playerAnimator.SetTrigger("sheatSword");
        }
        else if (col.gameObject.name == "hr2")
        {
            FindObjectOfType<InGamePlaySystemUI>().ShowCinematic();
            Invoke(nameof(temp2), 0.8f);
            // playerAnimator.SetTrigger("sheatSword");
        }
        else if (col.gameObject.name == "chatBox")
        {
            FindObjectOfType<ConversationManager>().StartConversationTask(0);
            // playerAnimator.SetTrigger("sheatSword");
        }
        else if (col.gameObject.name == "Jack")
        {
            FindObjectOfType<ConversationManager>().StartConversationTask(1);
            //  playerAnimator.SetTrigger("sheatSword");

        }
        else if (col.gameObject.name == "receiveDamage")
        {
            print("damage Player");
            Utility.StartShakeCamera(this, 10f, 0.2f);
            PlayerStatus.receiveDamage(50);
        }
    }

    public void SendDamage()
    {
        // print("sned");
        FindObjectOfType<ObjectDamageSender>().StartDamage();
    }
    public void EndDamage()
    {
        //    print("receive");
        FindObjectOfType<ObjectDamageSender>().EndDamage();
    }
    public void AttackStart()
    {
        isAttackEnd = false;
    }
    public void AttackEnd()
    {
        isAttackEnd = true;
    }
    public void AnimationStart()
    {
        isAnimationEnd = false;
    }

    public void AnimationEnd()
    {
        isAnimationEnd = true;
    }
    //Player HealthUI and player Stamina rate;


    void temp()
    {
        FindObjectOfType<DialogueManager>().SetDialogueTask(0);
    }

    void temp2()
    {
        FindObjectOfType<CinematicManager>().SetCinematicTask(0);
    }
    void OnTriggerExit(Collider col)
    {
        // FindObjectOfType<InGamePlaySystemUI>().ShowGamePlay();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(-transform.up, transform.up + new Vector3(0, 0, 0));
    }
}
