using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenager : MonoBehaviour
{
    public TestConnect Test;
    float PrepareTime=3.0f;
    private void Update()
    {
        if (Test.Ready())
        {
            Test.PlayerName.text = Mathf.RoundToInt(PrepareTime).ToString();
            PrepareTime -= Time.deltaTime;
            if (PrepareTime <= 0) { SceneManager.LoadScene("NetworkingTestScene"); }
        }
    }
}
