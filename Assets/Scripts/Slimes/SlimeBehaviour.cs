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
    Rigidbody RB;

    private void Awake()
    {
        PrepareScript = GetComponent<SmothPass>();
        MoveScript = GetComponent<AbominacjaRobiacaZaPathFind>();
        FightScript = GetComponent<SlimeFight>();
        RB = GetComponent<Rigidbody>();
        ChangeState(CurrentState);
    }

    public void ChangeState(State NewState)
    {
        CurrentState = NewState;
        switch (CurrentState)
        {
            case State.Prepare: 
                {
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
                    PrepareScript.enabled = false;
                    MoveScript.enabled = true;
                    FightScript.enabled = true;
                    RB.isKinematic = false;
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

}
