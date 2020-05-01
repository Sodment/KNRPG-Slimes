using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBahaviourV2 : MonoBehaviour
{
    public int PlayerID;
    public enum State { Prepare, Fight, Die, Stun};

    [SerializeField]
    private State currentState = State.Prepare;

    private HealthCallback healthScript;
    private FightCallback fightScript;
    private PrepareCallback prepareScript;
    private MovmentCallback movmentsScript;
    private Rigidbody rigidbody;

    public GameObject healthCanvas;
    public Image healthBar;

    private void Start()
    {
        healthScript = GetComponent<HealthCallback>();
        fightScript = GetComponent<FightCallback>();
        prepareScript = GetComponent<PrepareCallback>();
        movmentsScript = GetComponent<MovmentCallback>();
        rigidbody = GetComponent<Rigidbody>();

        ChangeState(State.Prepare);
    }


    public void ChangeState(State _newState)
    {
        currentState = _newState;
        switch (_newState)
        {
            case State.Prepare: 
                {
                    ClearMods();
                    ClearEffects();
                    healthBar.fillAmount = 1;
                    healthCanvas.SetActive(false);
                    prepareScript.enabled = true;
                    fightScript.enabled = false;
                    movmentsScript.enabled = false;
                    healthScript.enabled = false;
                    rigidbody.isKinematic = true;
                    break; 
                }
            case State.Fight:
                {
                    healthScript = GetComponent<HealthCallback>();
                    fightScript = GetComponent<FightCallback>();
                    prepareScript = GetComponent<PrepareCallback>();
                    movmentsScript = GetComponent<MovmentCallback>();
                    prepareScript.enabled = false;
                    fightScript.enabled = true;
                    movmentsScript.enabled = true;
                    healthScript.enabled = true;
                    rigidbody.isKinematic = false;
                    break;
                }
            case State.Stun:
                {
                    prepareScript.enabled = false;
                    fightScript.enabled = false;
                    movmentsScript.enabled = false;
                    healthScript.enabled = true;
                    rigidbody.isKinematic = false;
                    break;
                }
            case State.Die:
                {
                    prepareScript.enabled = false;
                    fightScript.enabled = false;
                    movmentsScript.enabled = false;
                    healthScript.enabled = false;
                    rigidbody.isKinematic = true;
                    healthBar.fillAmount = 1;
                    healthCanvas.SetActive(false);
                    gameObject.SetActive(false);
                    break;
                }
        }
    }

    void ClearMods()
    {
        Mods[] mods = GetComponents<Mods>();
        for(int i=0; i<mods.Length; i++)
        {
            Destroy(mods[i]);
        }
    }
    void ClearEffects()
    {
        Effect[] effects= GetComponents<Effect>();
        for (int i = 0; i < effects.Length; i++)
        {
            Destroy(effects[i]);
        }
    }

    public State GetState()
    {
        return currentState;
    }
}
