using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlimeBehaviour : MonoBehaviour
{
    public int PlayerID;
    public enum State { Prepare, Fight, Die }
    State CurrentState = State.Prepare;

    SmothPass PrepareScript;
    AbominacjaRobiacaZaPathFind MoveScript;
    SlimeFight FightScript;
    SlimeHealth HealthScript;
    Rigidbody RB;

    public GameObject healthCanvas;
    public Image healthBar;


    public string UnitID;

    private void Awake()
    {
        PrepareScript = GetComponent<SmothPass>();
        MoveScript = GetComponent<AbominacjaRobiacaZaPathFind>();
        FightScript = GetComponent<SlimeFight>();
        HealthScript = GetComponent<SlimeHealth>();
        RB = GetComponent<Rigidbody>();
        ChangeState(CurrentState);
        healthCanvas.SetActive(false);
    }

    public void ChangeState(State NewState)
    {
        CurrentState = NewState;
        switch (CurrentState)
        {
            case State.Prepare: 
                {
                    HealthScript = GetComponent<SlimeHealth>();
                    HealthScript.Start();
                    HealthScript.Prepare(GetComponent<SlimeLevelsV2>().Health);
                    healthCanvas.SetActive(false);
                    PrepareScript.enabled = true;
                    if (transform.parent != null)
                    { transform.position = transform.parent.position; }
                    MoveScript.enabled = false;
                    FightScript.Respawn();
                    FightScript.enabled = false;
                    RB.isKinematic = true;
                    break; 
                }
            case State.Fight:
                {
                    HealthScript = GetComponent<SlimeHealth>();
                    HealthScript.Prepare(GetComponent<SlimeLevelsV2>().Health);
                    PrepareScript.enabled = false;
                    MoveScript.enabled = true;
                    FightScript.enabled = true;
                    RB.isKinematic = false;
                    HealthScript = GetComponent<SlimeHealth>();
                    break;
                }
            case State.Die:
                {
                    PrepareScript.enabled = false;
                    MoveScript.enabled = false;
                    FightScript.enabled = false;
                    transform.position = Vector3.down * 50.0f;
                    gameObject.SetActive(false);
                    break;
                }
        }
    }


    public void GetDMG(float _dmg)
    {
        HealthScript.GetDMG(_dmg);
    }

}
