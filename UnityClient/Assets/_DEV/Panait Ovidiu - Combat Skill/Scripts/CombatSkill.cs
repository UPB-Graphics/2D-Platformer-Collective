using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Attack/CombatSkill")]
public class CombatSkill : ScriptableObject
{
    private float startX;
    private bool isLeft;

    new public string name = "Skill name";
   public bool isStatic = false;
    public float hitDamage = 10f;
    public float speed = 5f;
    public float range = 5f;
    public float cooldownTime = 1.75f;

    public bool hasAudio;
    public AudioClip audioClip;

    [SerializeField] GameObject projectilePrefab;

    public virtual void Activate(Player player)
    {
        Debug.Log("Activate " + name);
        Vector3 position = Vector3.zero;

        if (player.transform.localScale.x == -1)
        {
            position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y, 0);
            GameObject newProjectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            newProjectile.GetComponent<SpriteRenderer>().flipX = true;
            newProjectile.GetComponent<Projectile>().SetParent(player.gameObject.name);
            newProjectile.GetComponent<Projectile>().SetDirection(true);
            newProjectile.GetComponent<Projectile>().SetSpeed(speed);
            newProjectile.GetComponent<Projectile>().SetRange(range);

            if (isStatic)
            {
            newProjectile.GetComponent<Projectile>().SetStatic();
            }
            if (hasAudio)
            {
                SoundManager.instance.PlaySound(audioClip);
            }
        }
        else
        {
            position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y, 0);
            GameObject newProjectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            newProjectile.GetComponent<SpriteRenderer>().flipX = false;
            newProjectile.GetComponent<Projectile>().SetParent(player.gameObject.name);
            newProjectile.GetComponent<Projectile>().SetDirection(false);
            newProjectile.GetComponent<Projectile>().SetSpeed(speed);
            newProjectile.GetComponent<Projectile>().SetRange(range);

            if (isStatic)
            {
                newProjectile.GetComponent<Projectile>().SetStatic();
            }
            if (hasAudio)
            {
                SoundManager.instance.PlaySound(audioClip);
            }
        }
    }
}