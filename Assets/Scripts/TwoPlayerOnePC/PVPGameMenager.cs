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


    public GameObject InfoCanvas;
    public Text Info;

    public GameObject EQPlayer1;
    public GameObject EQPlayer2;

    public int TureDruation = 5;

    int[] WinsPlayer = new int[2];
    bool Match = true;


    public GameObject PauseCanvas;

    private void Start()
    {
        CameraTransform = Camera.main.transform.parent.transform;
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
            EQPlayer1.SetActive(true);
            for (int i = TureDruation; i >= 0; i--)
            {
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            //Obrót o 180 stopni
            EQPlayer1.GetComponent<IventoryV5>().FastPut();
            CurrentStage = Stage.Rotate;
            EQPlayer1.SetActive(false);
            for (int i = 0; i < 180; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation*Quaternion.Euler(new Vector3(0, 1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Tura drugiego gracza
            CurrentStage = Stage.Player2;
            EQPlayer2.SetActive(true);
            for (int i = TureDruation; i >= 0; i--)
            {
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            //Obrót o -90 stopni
            EQPlayer2.GetComponent<IventoryV5>().FastPut();
            CurrentStage = Stage.Rotate;
            EQPlayer2.SetActive(false);
            for (int i = 0; i < 90; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation * Quaternion.Euler(new Vector3(0, -1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Bitwa
            CurrentStage = Stage.Battle;
            List<GameObject> FightingSlimes = new List<GameObject>();

            foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
            {
                k.ChangeState(SlimeBehaviour.State.Fight);
                FightingSlimes.Add(k.gameObject);
            }
            
            for (int i = TureDruation*10; i >= 0; i--)
            {
                int Player1UnitsCount = 0;
                int Player2UnitsCount = 0;

                foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
                {
                    if (k.PlayerID == 1) { Player1UnitsCount++; }
                    if (k.PlayerID == 2) { Player2UnitsCount++; }
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

            foreach (GameObject k in FightingSlimes)
            {
                k.SetActive(true);
                k.GetComponent<SlimeBehaviour>().ChangeState(SlimeBehaviour.State.Prepare);
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
            EQPlayer2.SetActive(true);
            for (int i = TureDruation; i >= 0; i--)
            {
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            //Obrót o -180 stopni
            EQPlayer2.GetComponent<IventoryV5>().FastPut();
            CurrentStage = Stage.Rotate;
            EQPlayer2.SetActive(false);
            for (int i = 0; i < 180; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation * Quaternion.Euler(new Vector3(0, -1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Tura pierwszego gracza
            CurrentStage = Stage.Player1;
            EQPlayer1.SetActive(true);
            for (int i = TureDruation; i >= 0; i--)
            {
                Timer.text = i.ToString();
                yield return new WaitForSeconds(1);
            }

            //Obrót o 90 stopni
            EQPlayer1.GetComponent<IventoryV5>().FastPut();
            CurrentStage = Stage.Rotate;
            EQPlayer1.SetActive(false);
            for (int i = 0; i < 90; i++)
            {
                CameraTransform.rotation = CameraTransform.rotation * Quaternion.Euler(new Vector3(0, 1, 0));
                yield return new WaitForSeconds(0.01f);
            }

            //Bitwa
            CurrentStage = Stage.Battle;
            FightingSlimes.Clear();

            foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
            {
                k.ChangeState(SlimeBehaviour.State.Fight);
                FightingSlimes.Add(k.gameObject);
            }

            for (int i = TureDruation * 10; i >= 0; i--)
            {
                int Player1UnitsCount = 0;
                int Player2UnitsCount = 0;

                foreach (SlimeBehaviour k in GameObject.FindObjectsOfType<SlimeBehaviour>())
                {
                    if (k.PlayerID == 1) { Player1UnitsCount++; }
                    if (k.PlayerID == 2) { Player2UnitsCount++; }
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

            foreach (GameObject k in FightingSlimes)
            {
                k.SetActive(true);
                k.GetComponent<SlimeBehaviour>().ChangeState(SlimeBehaviour.State.Prepare);
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
