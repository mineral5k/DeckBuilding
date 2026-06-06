using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "DelayAttack", menuName = "Data/Effect/DelayAttack")]

public class DelayAttack : AttackEffect
{
    [SerializeField] private int delay;

    public override async void Excute(Damagable target, int value)
    {
        await Task.Delay(delay);
        GameManager.Instance.Player.Status.Attack(target, value);
    }
    
}
