using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AgentUI : MonoBehaviour
{
    public TextMeshProUGUI stepValue;
    public TextMeshProUGUI rewardValue;

    public void SetStepValue(int _value)
    {
        stepValue.text = _value.ToString();
    }

    public void SetRewardValue(float _value)
    {
        rewardValue.text = _value.ToString("N2");
    }
}
