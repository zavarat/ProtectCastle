﻿using UnityEngine;
using System.Collections;

public class PlanetSkillManager : MonoBehaviour {

    [SerializeField]
    private UIButton btn_Shield;
    [SerializeField]
    private UILabel shieldCoolTime;
    [SerializeField]
    private GameObject shieldEffect;
    [SerializeField]
    private UIButton btn_Healing;
    [SerializeField]
    private UILabel healingCoolTime;
    [SerializeField]
    private GameObject helaingEffect;
    [SerializeField]
    private CoinManager coinMgr;
    [SerializeField]
    private PlanetManager planetMgr;

    private bool isShieldEnable = true;
    private bool isHealingEnable = true;

    public void Skill_Shield()
    {
        if(isShieldEnable)
        {
            btn_Shield.isEnabled = false;
            isShieldEnable = false;
            StartCoroutine(ShieldProcess());
        }
    }
    public void Skill_Healing()
    {
        if(isHealingEnable)
        {
            btn_Healing.isEnabled = false;
            isHealingEnable = false;
            StartCoroutine(HealingProcess());
        }
    }
	
    private IEnumerator ShieldProcess()
    {
        shieldEffect.SetActive(true);
        float coolTime = 20.0f;
        shieldCoolTime.text = coolTime.ToString();
        while(coolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            coolTime--;
            shieldCoolTime.text = coolTime.ToString();
            if (coolTime < 10.0f)
            {
                shieldEffect.SetActive(false);
            }
        }
        btn_Shield.isEnabled = true;
        isShieldEnable = true;
        shieldCoolTime.text = "Shield";
    }
    private IEnumerator HealingProcess()
    {
        helaingEffect.SetActive(true);
        float coolTime = 20.0f;
        healingCoolTime.text = coolTime.ToString();
        while (coolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            coolTime--;
            healingCoolTime.text = coolTime.ToString();
        }
        btn_Healing.isEnabled = true;
        isHealingEnable = true;
        healingCoolTime.text = "Healing";
    }
}
