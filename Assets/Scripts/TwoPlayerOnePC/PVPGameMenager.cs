using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PVPGameMenager : MonoBehaviour
{
    public enum Stage { Player1, Rotate, Player2, Battle}
    public Stage CurrentStage = Stage.Player1;
    Transform CameraTransform;
    public Text Timer;

    public GameObject HudPlayer1;
    public GameObject HudPlayer2;

    public GameObject InfoCanvas;
    public Text Info;

    public GameObject EQPlayer1;
    public GameObject EQPlayer2;

    DragMenager Cleaner;

    public int TureDruation = 5;

    int[] WinsPlayer = new int[2];
    bool Match = true;


    public GameObject PauseCanvas;

    private void Start()
    {
        CameraTransform = Camera.main.transform.parent.transform;
        Cleaner = GameObject.FindObjectOfType<DragMenager>();
        InfoCanvas.SetActive(false);
        WinsPlayer[0] = 0;
        WinsPlayer[1] = 0;
        StartCoroutine(GameProcedure());
    }


    IEnumerator GameProcedure()
    {
        while (Match)
        {
            //Tura pierwszego gracza
            CurrentStage = Stage.Player1;
            HudPlayer1.SetActive(true);
            EQPlayer1.SetActive(true);
            for (int i = TureDruation; i >= 0; i--)
            {
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            //Obrót o 180 stopni
            if (Cleaner != null)
            { Cleaner.FastDrop(); }
            else { EQPlayer1.GetComponent<IventoryV5>().FastPut(); }
            CurrentStage = Stage.Rotate;
            HudPlayer1.SetActive(false);
            EQPlayer1.SetActive(false);
            for (int i = 0; i < 180; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation*Quaternion.Euler(new Vector3(0, 1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Tura drugiego gracza
            CurrentStage = Stage.Player2;
            HudPlayer2.SetActive(true);
            EQPlayer2.SetActive(true);
            for (int i = TureDruation; i >= 0; i--)
            {
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            //Obrót o -90 stopni
            if (Cleaner != null)
            { Cleaner.FastDrop(); }
            else { EQPlayer2.GetComponent<IventoryV5>().FastPut(); }
            CurrentStage = Stage.Rotate;
            HudPlayer2.SetActive(false);
            EQPlayer2.SetActive(false);
            for (int i = 0; i < 90; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation * Quaternion.Euler(new Vector3(0, -1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Bitwa
            CurrentStage = Stage.Battle;
            List<GameObject> FightingSlimes = new List<GameObject>();
            if (Cleaner != null)
            {
                foreach (SlimeMovement k in GameObject.FindObjectsOfType<SlimeMovement>())
                {
                    k.enabled = true;
                    k.GetComponent<DragSlime>().enabled = false;
                    FightingSlimes.Add(k.gameObject);
                }
            }
            else
            {
                foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
                {
                    k.ChangeState(SlimeBehaviour.State.Fight);
                    FightingSlimes.Add(k.gameObject);
                }
            }
            for (int i = TureDruation*10; i >= 0; i--)
            {
                int Player1UnitsCount = 0;
                int Player2UnitsCount = 0;
                if (Cleaner != null)
                {
                    foreach (SlimeMovement k in GameObject.FindObjectsOfType<SlimeMovement>())
                    {
                        if (k.PlayerID == 1) { Player1UnitsCount++; }
                        if (k.PlayerID == 2) { Player2UnitsCount++; }
                    }
                }
                else
                {
                    foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
                    {
                        if (k.PlayerID == 1) { Player1UnitsCount++; }
                        if (k.PlayerID == 2) { Player2UnitsCount++; }
                    }
                }
                if ((Player1UnitsCount == 0 || Player2UnitsCount == 0)&&i>3)
                {
                    if (Player1UnitsCount == 0) { WinsPlayer[1]++; }
                    else { WinsPlayer[0]++; }
                    if(WinsPlayer[0]>=5 || WinsPlayer[1] >= 5)
                    {
                        Match = false;
                    }
                    i = 3; 
                }
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }
            if (Cleaner != null)
            {
                foreach (GameObject k in FightingSlimes)
                {
                    k.SetActive(true);
                    k.GetComponent<SlimeMovement>().enabled = false;
                    k.GetComponent<DragSlime>().enabled = true;
                    //k.GetComponent<SlimeFightTmp>().Refresh();
                }
            }
            else
            {
                foreach (GameObject k in FightingSlimes)
                {
                    k.SetActive(true);
                }
                foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
                {
                    k.ChangeState(SlimeBehaviour.State.Prepare);
                }
            }

            if (!Match) { break; }

            //Obrót o 90 stopni
            CurrentStage = Stage.Rotate;
            for (int i = 0; i < 90; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation * Quaternion.Euler(new Vector3(0, 1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Tura drugiego gracza
            CurrentStage = Stage.Player2;
            HudPlayer2.SetActive(true);
            EQPlayer2.SetActive(true);
            for (int i = TureDruation; i >= 0; i--)
            {
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            //Obrót o -180 stopni
            if (Cleaner != null)
            { Cleaner.FastDrop(); }
            else { EQPlayer2.GetComponent<IventoryV5>().FastPut(); }
            CurrentStage = Stage.Rotate;
            HudPlayer2.SetActive(false);
            EQPlayer2.SetActive(false);
            for (int i = 0; i < 180; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation * Quaternion.Euler(new Vector3(0, -1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Tura pierwszego gracza
            CurrentStage = Stage.Player1;
            HudPlayer1.SetActive(true);
            EQPlayer1.SetActive(true);
            for (int i = TureDruation; i >= 0; i--)
            {
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            //Obrót o 90 stopni
            if (Cleaner != null)
            { Cleaner.FastDrop(); }
            else { EQPlayer1.GetComponent<IventoryV5>().FastPut(); }
            CurrentStage = Stage.Rotate;
            HudPlayer1.SetActive(false);
            EQPlayer1.SetActive(false);
            for (int i = 0; i < 90; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation * Quaternion.Euler(new Vector3(0, 1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Bitwa
            CurrentStage = Stage.Battle;
            FightingSlimes.Clear();
            if (Cleaner != null)
            {
                foreach (SlimeMovement k in GameObject.FindObjectsOfType<SlimeMovement>())
                {
                    k.enabled = true;
                    k.GetComponent<DragSlime>().enabled = false;
                    FightingSlimes.Add(k.gameObject);
                }
            }
            else
            {
                foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
                {
                    k.ChangeState(SlimeBehaviour.State.Fight);
                    FightingSlimes.Add(k.gameObject);
                }
            }
            for (int i = TureDruation * 10; i >= 0; i--)
            {
                int Player1UnitsCount = 0;
                int Player2UnitsCount = 0;
                if (Cleaner != null)
                {
                    foreach (SlimeMovement k in GameObject.FindObjectsOfType<SlimeMovement>())
                    {
                        if (k.PlayerID == 1) { Player1UnitsCount++; }
                        if (k.PlayerID == 2) { Player2UnitsCount++; }
                    }
                }
                else
                {
                    foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
                    {
                        if (k.PlayerID == 1) { Player1UnitsCount++; }
                        if (k.PlayerID == 2) { Player2UnitsCount++; }
                    }
                }
                if ((Player1UnitsCount == 0 || Player2UnitsCount == 0) && i > 3)
                {
                    if (Player1UnitsCount == 0) { WinsPlayer[1]++; }
                    else { WinsPlayer[0]++; }
                    if (WinsPlayer[0] >= 5 || WinsPlayer[1] >= 5)
                    {
                        Match = false;
                    }
                    i = 3;
                }
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }
            if (Cleaner != null)
            {
                foreach (GameObject k in FightingSlimes)
                {
                    k.SetActive(true);
                    k.GetComponent<SlimeMovement>().enabled = false;
                    k.GetComponent<DragSlime>().enabled = true;
                    //k.GetComponent<SlimeFightTmp>().Refresh();
                }
            }
            else
            {
                foreach (GameObject k in FightingSlimes)
                {
                    k.SetActive(true);
                }
                foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
                {
                    k.ChangeState(SlimeBehaviour.State.Prepare);
                }
            }

            if (!Match) { break; }

            //Obrót o -90 stopni
            CurrentStage = Stage.Rotate;
            for (int i = 0; i < 90; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation * Quaternion.Euler(new Vector3(0, -1, 0));
                yield return new WaitForSeconds(0.01f);
            }
        }

        if(WinsPlayer[0]>=5) { Info.text = "Gracz 1 wygrał."; }
        else { Info.text = "Gracz 2 wygrał."; }
        InfoCanvas.SetActive(true);

    }


    public void Pause()
    {
        Time.timeScale = 0;
        PauseCanvas.SetActive(true);
        foreach(IventoryV5 k in GameObject.FindObjectsOfType<IventoryV5>())
        {
            k.enabled = false;
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
        foreach (IventoryV5 k in GameObject.FindObjectsOfType<IventoryV5>())
        {
            k.enabled = true;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
