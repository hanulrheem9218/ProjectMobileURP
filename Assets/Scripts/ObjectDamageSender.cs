using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamageSender : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool isDamageable;
    [SerializeField] List<GameObject> targets;
    [SerializeField] float objectLength;
    [SerializeField] float damageAmount;
    PlayerGeneralSystem player;
    DefaultTouch defaultAttack;
    [SerializeField] Transform defaultTarget;
    void Start()
    {
        defaultAttack = FindObjectOfType<DefaultTouch>();
        player = FindObjectOfType<PlayerGeneralSystem>();

        isDamageable = false;

        targets = new List<GameObject>();
    }

    // Update is called once per frame
    // will use this for detecting mutlipe attacks
    void FixedUpdate()
    {
        defaultTarget = defaultAttack.targetObject;
        // if (isDamageable)
        // {

        //     if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, objectLength, player.targetMask))
        //     {
        //         if (!targets.Contains(hit.transform.gameObject))
        //         {

        //             //   hit.transform.SendMessage("test", 200f);
        //             Debug.Log("receving attack");
        //             targets.Add(hit.transform.gameObject);
        //         }
        //     }
        // }
    }

    public void StartDamage()
    {

        //  targets.Clear();
        isDamageable = true;

    }

    public void EndDamage()
    {
        if (defaultTarget != null)
        {
            defaultTarget.SendMessage("test", 200f);
        }

        isDamageable = false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * objectLength);
    }
}
