using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Creature))]
public class Attacker : MonoBehaviour
{
    private UnityEvent onKill;
    public int strengh = 0;

    private void Awake()
    {
        if (onKill == null)
        {
            onKill = new UnityEvent();
        }
    }

    public void addActionOnKill(UnityAction action)
    {
        onKill.AddListener(action);
    }

    public void Hit(Creature target)
    {
        target.TakeDamage(strengh);

        if (!target.IsAlive())
        {
            onKill.Invoke();
        }
    }
}
