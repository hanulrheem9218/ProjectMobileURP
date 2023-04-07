using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator upgradeAnimator;
    void Start()
    {
        upgradeAnimator = transform.GetComponent<Animator>();
        transform.gameObject.SetActive(false);
    }

    public void ShowUpgrade()
    {
        if (!transform.gameObject.activeSelf)
        {
            transform.gameObject.SetActive(true);
        }
        upgradeAnimator.SetTrigger("UpgradeOpen");
        upgradeAnimator.ResetTrigger("UpgradeClose");
    }

    public void HideUpgrade()
    {
        FindObjectOfType<InGamePlaySystemUI>().ShowGamePlay();
        upgradeAnimator.SetTrigger("UpgradeClose");
        upgradeAnimator.ResetTrigger("UpgradeOpen");
    }
    void Update()
    {

    }
}
