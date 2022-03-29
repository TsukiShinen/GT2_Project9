using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Field Modifier", menuName = "Field Modifier")]
public class FieldModifier : ScriptableObject
{
    [SerializeField] protected GameParameters _parameters;

    [SerializeField] protected float _speedModifier;

    public virtual void Activate(Tank tank)
    {
        tank.Speed = _parameters.TankSpeed * _speedModifier;
    }

    public virtual void Desactivate(Tank tank)
    {

    }
}
