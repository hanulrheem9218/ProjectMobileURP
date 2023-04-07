using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DefaultTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // public bool buttonPressed;


    [SerializeField] private MovementJoystick joyStick;
    [SerializeField] private PlayerGeneralSystem player;
    // private PlayerGeneralSystem player;
    [SerializeField] private Animator playerAnimator;
    private AnimatorStateInfo playerInfo;
    private float touchCounts = 0f;
    private bool isAnimationEnd;
    private bool movingAndAttack;
    private Coroutine waitForAttack;
    private bool[] isTouchStart;
    private bool[] isTouchHeld;
    private Coroutine[] waitForHeld;
    public Coroutine waitForDefault;
    private bool isDefaultAnimation;
    private string[] animationNames;

    public Transform targetObject;

    void Start()
    {
        GetActionControls();
        InvokeRepeating(nameof(CheckInRange), 0, 0.5f);
    }
    void LateUpdate()
    {
        playerInfo = playerAnimator.GetCurrentAnimatorStateInfo(1);
        if (isTouchHeld[0] && !isTouchHeld[1])
        {
            waitForHeld[1] = StartCoroutine(CombatRepeat(1f, playerAnimator));
        }
        //print(playerAnimator.GetCurrentAnimatorStateInfo(1).IsName("SwordIdle"));
        // print(joyStick.isDragging);

    }

    private void GetActionControls()
    {
        //  ActionTouch[] touches = FindObjectsOfType<ActionTouch>();
        playerAnimator = FindObjectOfType<PlayerGeneralSystem>().playerAnimator;
        joyStick = FindObjectOfType<MovementJoystick>();
        player = FindObjectOfType<PlayerGeneralSystem>();
        animationNames = new string[] { "SwordAttack1", "SwordAttack2", "SwordAttack3" };

        isTouchHeld = new bool[4];
        isTouchStart = new bool[4];
        waitForHeld = new Coroutine[4];
        for (int i = 0; i < 4; i++)
        {

            isTouchStart[i] = false;
            isTouchHeld[i] = false;
            waitForHeld[i] = null;
        }


    }
    private void AttackTarget()
    {
        if (targetObject == null)
        {
            playerAnimator.SetTrigger("Attack");
            if (waitForHeld[0] != null)
            {
                StopCoroutine(waitForHeld[0]);
            }
            waitForHeld[0] = StartCoroutine(isKeyHeld(1f));
            player.DisableInput(1f);
        }
        else if (targetObject != null)
        {
            playerAnimator.SetTrigger("Attack");
            if (waitForHeld[0] != null)
            {
                StopCoroutine(waitForHeld[0]);
            }
            Vector3 targetPostition = new Vector3(targetObject.position.x, player.transform.position.y, targetObject.position.z);
            player.transform.LookAt(targetPostition);
            waitForHeld[0] = StartCoroutine(isKeyHeld(1f));
            player.DisableInput(1f);
        }
    }
    public void TouchInput()
    {
        // print(playerInfo.IsName(animationNames[0]) + " : " + playerInfo.IsName(animationNames[1]) + ": " + playerInfo.IsName(animationNames[2]) + " : ||" + playerInfo.IsName("SwordIdle") + " : " + playerInfo.IsName("SwordRun") + " :TOUCH" + joyStick.isDragging);
        if (!isTouchHeld[0])
        {
            if (!playerAnimator.GetBool("drawSword") && !isTouchHeld[2])
            {
                playerAnimator.SetTrigger("drawSword");
                isTouchHeld[2] = true;
                player.DisableInput(1f);
            }
            else if (playerInfo.IsName("SwordRun"))
            {
                playerAnimator.ResetTrigger("Move");
                AttackTarget();
            }
            else if (playerInfo.IsName("SwordIdle"))
            {
                playerAnimator.ResetTrigger("Move");
                AttackTarget();
            }

            else if (!playerInfo.IsName(animationNames[0]) && !playerInfo.IsName(animationNames[1]) && !playerInfo.IsName(animationNames[2]) && !playerInfo.IsName("SwordIdle") && !playerInfo.IsName("SwordRun"))
            {
                playerAnimator.ResetTrigger("Move");
                AttackTarget();
            }

            else if (isTouchHeld[2] && playerAnimator.GetComponent<PlayerGeneralSystem>().isAttackEnd)
            {
                playerAnimator.ResetTrigger("Move");
                AttackTarget();
            }

        }


    }
    private void DefaultAnimation()
    {
        isDefaultAnimation = true;
        if (waitForDefault != null)
        {
            StopCoroutine(waitForDefault);
        }
        // check milliseconds.
        waitForDefault = StartCoroutine(CheckAnimationEnds(0.1f));
    }
    private void TouchInputUp()
    {

        // print(playerInfo.IsName(animationNames[0]) + " : " + playerInfo.IsName(animationNames[1]) + ": " + playerInfo.IsName(animationNames[2]) + " : ||" + playerInfo.IsName("SwordIdle") + " : " + playerInfo.IsName("SwordRun") + ": ANY TIME " + playerInfo.length);
        // BUG FIXED!
        if (!playerInfo.IsName(animationNames[0]) && !playerInfo.IsName(animationNames[1]) && !playerInfo.IsName(animationNames[2]) && !playerInfo.IsName("SwordIdle") && !playerInfo.IsName("SwordRun"))
        {
            // playerAnimator.SetTrigger("Move");

            DefaultAnimation();

        }

        else if (playerInfo.IsName("SwordRun"))
        {
            DefaultAnimation();

        }
        else if (playerInfo.IsName("SwordIdle"))
        {

            DefaultAnimation();

        }

        // if(playerInfo.IsName(animationNames[0]){

        // })
        isTouchHeld[0] = false;
        isTouchHeld[1] = false;
        //   StopCoroutine(player.combatCoroutine);
        for (int i = 0; i < waitForHeld.Length; i++)
        {

            if (waitForHeld[i] != null)
            {
                StopCoroutine(waitForHeld[i]);
            }
        }
        for (int j = 0; j < animationNames.Length; ++j)
        {
            //   print(animationNames[j]);
            if (playerInfo.IsName(animationNames[j]))
            {
                DefaultAnimation();
            }
        }



    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TouchInput();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TouchInputUp();
    }


    IEnumerator CheckAnimationEnds(float delay)
    {
        while (isDefaultAnimation)
        {
            if (playerAnimator.GetComponent<PlayerGeneralSystem>().isAnimationEnd || (playerAnimator.GetComponent<PlayerGeneralSystem>().isAnimationEnd && playerAnimator.GetComponent<PlayerGeneralSystem>().isAttackEnd))
            {
                if (waitForDefault != null)
                {
                    playerAnimator.SetTrigger("Move");
                    //  playerAnimator.GetComponent<PlayerGeneralSystem>().isAnimationEnd = true;
                    StopCoroutine(waitForDefault);

                    waitForDefault = null;
                    isDefaultAnimation = false;
                    //StopCoroutine(player.combatCoroutine);
                    // print("ANIMATION DEAD");

                }

            }
            // print("still loading ?");
            yield return new WaitForSeconds(delay);
        }
    }


    IEnumerator CombatRepeat(float delay, Animator playerAnimator)
    {
        isTouchHeld[1] = true;
        //   print("IT IS HELD!!");
        while (isTouchHeld[1])
        {
            if (playerAnimator.GetComponent<PlayerGeneralSystem>().isAttackEnd)
            {
                playerAnimator.SetTrigger("Attack");
                // joyStick.DisableInput(1f);

            }
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator isKeyHeld(float delay)
    {
        yield return new WaitForSeconds(delay);
        isTouchHeld[0] = true;
    }
    void InputTouchDragging()
    {

    }
    void CheckInRange()
    {
        float attackDistance = Mathf.Infinity;
        Collider[] targets = Physics.OverlapSphere(player.playerObject.transform.position, player.attackRange, player.targetMask);
        GameObject nearestTarget = null;
        foreach (Collider target in targets)
        {
            float distanceToTarget = Vector3.Distance(player.playerObject.transform.position, target.transform.position);
            if (distanceToTarget < attackDistance)
            {
                attackDistance = distanceToTarget;
                nearestTarget = target.gameObject;
            }
        }

        if (nearestTarget != null && attackDistance <= player.attackRange)
        {
            targetObject = nearestTarget.transform;
        }
        else
        {
            targetObject = null;
        }
    }
}
