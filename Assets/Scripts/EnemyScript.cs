using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform enemyTransform;
    public Quaternion enemyRotation;
    public Vector3 pos;
    // UI platform 
    private Transform statusUI;
    private Camera mainCamera;
    private bool isDead;
    //variabke

    public GameObject floatingStatusUI;
    void Awake()
    {
        if (transform.name == "Enemy")
        {
            statusUI = transform.Find("StatusUI").transform;
            Utility.setStatusUI(500, "2B", transform);
        }
        else if (transform.name == "Enemy2")
        {
            statusUI = transform.Find("StatusUI").transform;
            Utility.setStatusUI(500, "Boycie", transform);
        }
    }
    void Start()
    {
        enemyTransform = transform;
        DeactiveRagdolls(transform.Find("Armature"), true);
        transform.GetComponent<CapsuleCollider>().enabled = true;
    }

    void Update()
    {
        Utility.ShowStatusUI(transform);
        if (statusUI.GetComponentInChildren<Slider>().value <= 0f && !isDead)
        {

            transform.GetComponent<CapsuleCollider>().enabled = false;
            transform.GetComponent<Rigidbody>().AddForce(transform.up * 500f, ForceMode.Acceleration);
            transform.gameObject.layer = 0;
            ActiveRagDolls(transform.Find("Armature"), false);
            isDead = true;
        }
    }
    void DeactiveRagdolls(Transform ragdoll, bool isEnabled)
    {
        ragdoll.transform.GetComponent<Animator>().enabled = isEnabled;
        Rigidbody[] rigidBodies = ragdoll.GetComponentsInChildren<Rigidbody>(true);
        print(rigidBodies);
        foreach (Rigidbody rigid in rigidBodies)
        {
            rigid.isKinematic = true;
            if (rigid.GetComponent<CapsuleCollider>() != null)
            {
                rigid.GetComponent<CapsuleCollider>().enabled = false;
            }
            else if (rigid.GetComponent<SphereCollider>() != null)
            {
                rigid.GetComponent<SphereCollider>().enabled = false;
            }
            else if (rigid.GetComponent<BoxCollider>() != null)
            {
                rigid.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
    void ActiveRagDolls(Transform ragdoll, bool isEnabled)
    {
        ragdoll.transform.GetComponent<Animator>().enabled = isEnabled;
        Rigidbody[] rigidBodies = ragdoll.GetComponentsInChildren<Rigidbody>(true);
        print(rigidBodies);
        foreach (Rigidbody rigid in rigidBodies)
        {
            rigid.isKinematic = false;
            if (rigid.GetComponent<CapsuleCollider>() != null)
            {
                rigid.GetComponent<CapsuleCollider>().enabled = true;
            }
            else if (rigid.GetComponent<SphereCollider>() != null)
            {
                rigid.GetComponent<SphereCollider>().enabled = true;
            }
            else if (rigid.GetComponent<BoxCollider>() != null)
            {
                rigid.GetComponent<BoxCollider>().enabled = true;
            }
        }
        Invoke(nameof(disable), 5f);
    }

    private void disable()
    {
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<CapsuleCollider>().enabled = false;
        DeactiveRagdolls(transform.Find("Armature"), false);

        UtilityResource.InstatiateResourceObject(this, "Currency", 5, transform.Find("EnemyItemSpawn"));

    }
    void ShowFloatingText()
    {
        var go = Instantiate(floatingStatusUI, transform.position + new Vector3(0, 6f, 0), Quaternion.identity, transform);
        go.GetComponent<TextMeshProUGUI>().text = statusUI.GetComponentInChildren<Slider>().value.ToString();
    }
    public void test(float gain)
    {
        // Debug.Log("got it ?");
        //        Utility.StatusChangeAmount(this, gain, statusUI);
        Utility.StartShakeCamera(this, 10f, 0.2f);
        statusUI.GetComponentInChildren<Slider>().value -= gain;
        ShowFloatingText();
    }

}
