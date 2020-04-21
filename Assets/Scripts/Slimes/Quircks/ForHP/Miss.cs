using UnityEngine;
public class Miss : SlimeHealth
{
    public override void GetDMG(float _dmg)
    {
        if (Random.Range(0, 100) < 60) { base.GetDMG(_dmg); }
    }
}