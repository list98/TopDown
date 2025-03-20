using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeScinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.Health;

    public AudioClip damageClip;
    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        baseController = GetComponent<BaseController>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (timeScinceLastChange < healthChangeDelay)
        {
            timeScinceLastChange += Time.deltaTime;
            if(timeScinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change ==0 || timeScinceLastChange < healthChangeDelay)
        {
            return false;
        }
        timeScinceLastChange = 0.0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0? 0 : CurrentHealth;
        
        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);
        if (change < 0)
        {
            animationHandler.Damage();

            if (damageClip != null)
                SoundManager.PlayClip(damageClip);
        }
        if(CurrentHealth <= 0)
        {
            Death();
        }

        return true;
    }
    private void Death()
    {
        baseController.Death();
    }
    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
}
