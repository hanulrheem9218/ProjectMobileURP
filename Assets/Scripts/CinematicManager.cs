using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CinematicTask[] cinematicTasks;
    void Awake()
    {
        cinematicTasks = GetComponentsInChildren<CinematicTask>();
        foreach (CinematicTask task in cinematicTasks)
        {
            task.transform.gameObject.SetActive(false);
        }
    }
    public void SetCinematicTask(int i)
    {
        cinematicTasks[i].gameObject.SetActive(true);
        cinematicTasks[i].StartCinematic();
    }
}
