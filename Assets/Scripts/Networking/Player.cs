using UnityEngine;

public class Player : MonoBehaviour {
    
    public static Player Instance;
    public string PlayerName { get; private set; }
    
    void Awake() {
        Instance = this;

        PlayerName = "Default#" + Random.Range(1000, 9999);
    }
}
