using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private GameObject[] waterballs;
    [SerializeField] private GameObject[] windballs;
    [SerializeField] private GameObject[] electricballs;
    [SerializeField] private GameObject[] steamballs;
    [SerializeField] private GameObject[] iceballs;

    private int loadedSpells;
    private int currentSpell;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        /*switch (Input.GetKeyDown(KeyCode))
        {
            default:
                break;
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadSpell(1);
            Debug.Log(currentSpell);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadSpell(2);
            Debug.Log(currentSpell);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadSpell(3);
            Debug.Log(currentSpell);
        }
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        { 
            Attack();
            loadedSpells = 0;
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        switch (currentSpell)
        {
            case 1:
                {
                    anim.SetTrigger("attack");
                    cooldownTimer = 0;

                    fireballs[FindFireball()].transform.position = firePoint.position;
                    fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                }
                break;
            case 2:
                {
                    anim.SetTrigger("attack");
                    cooldownTimer = 0;

                    waterballs[FindWaterball()].transform.position = firePoint.position;
                    waterballs[FindWaterball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                }
                break;
            case 3:
                {
                    anim.SetTrigger("attack");
                    cooldownTimer = 0;

                    windballs[FindWindball()].transform.position = firePoint.position;
                    windballs[FindWindball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                }
                break;
            case 4:
                {
                    anim.SetTrigger("attack");
                    cooldownTimer = 0;

                    electricballs[FindElectricball()].transform.position = firePoint.position;
                    electricballs[FindElectricball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                }
                break;
            case 5:
                {
                    anim.SetTrigger("attack");
                    cooldownTimer = 0;

                    steamballs[FindSteamball()].transform.position = firePoint.position;
                    steamballs[FindSteamball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                }
                break;
            case 6:
                {
                    anim.SetTrigger("attack");
                    cooldownTimer = 0;

                    iceballs[FindIceball()].transform.position = firePoint.position;
                    iceballs[FindIceball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                }
                break;
            default:
                {
                    anim.SetTrigger("attack");
                    cooldownTimer = 0;

                    fireballs[FindFireball()].transform.position = firePoint.position;
                    fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
                }
                break;
        }

        
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private int FindWaterball()
    {
        for (int i = 0; i < waterballs.Length; i++)
        {
            if (!waterballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private int FindWindball()
    {
        for (int i = 0; i < windballs.Length; i++)
        {
            if (!windballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private int FindElectricball()
    {
        for (int i = 0; i < electricballs.Length; i++)
        {
            if (!electricballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private int FindSteamball()
    {
        for (int i = 0; i < steamballs.Length; i++)
        {
            if (!steamballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private int FindIceball()
    {
        for (int i = 0; i < iceballs.Length; i++)
        {
            if (!iceballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void LoadSpell(int element) //1-fire, 2-water, 3-wind, 4-electic, 5-steam, 6-ice
    {
        switch (loadedSpells)
        {
            case 0: 
                {
                    currentSpell = element;
                    loadedSpells = 1;
                }; break;
            case 1: 
                { 
                    switch (currentSpell)
                    {
                        case 1:
                            {
                                switch (element)
                                {
                                    case 1:
                                        currentSpell = 1; break;
                                    case 2:
                                        currentSpell = 5; break;
                                    case 3:
                                        currentSpell = 4; break;
                                    default:
                                        break;
                                }
                            } break;
                        case 2:
                            {
                                switch (element)
                                {
                                    case 1:
                                        currentSpell = 5; break;
                                    case 2:
                                        currentSpell = 2; break;
                                    case 3:
                                        currentSpell = 6; break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case 3:
                            {
                                switch (element)
                                {
                                    case 1:
                                        currentSpell = 4; break;
                                    case 2:
                                        currentSpell = 6; break;
                                    case 3:
                                        currentSpell = 3; break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    loadedSpells = 2;
                }
                break;
            case 2: break;
            default:
                break;
        }
    }
}
