using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MovmentMods
{
    public override float Calculate(float _inputValue)
    {
        return _inputValue*3.0f;
    }
}
