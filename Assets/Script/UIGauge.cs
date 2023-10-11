using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    public Image hpGauge;
    public Image staminaGauge;

    public void HPRefresh(float _hp, float _maxHp)
    {
        hpGauge.fillAmount = _hp / _maxHp;
    }

    public void StaminaRefresh(float _stamina, float _maxStamina)
    {
        staminaGauge.fillAmount = _stamina / _maxStamina;
    }
}
