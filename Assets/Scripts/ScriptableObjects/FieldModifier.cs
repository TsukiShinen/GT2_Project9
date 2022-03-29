using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Field Modifier", menuName = "Field Modifier")]
public class FieldModifier : ScriptableObject
{
    [SerializeField] public GameParameters Parameters;

    [SerializeField] private float _speedModifier;

    public virtual void Activate(Tank tank)
    {
        tank.Speed = Parameters.TankSpeed * _speedModifier;
    }

    public virtual void Desactivate(Tank tank)
    {

    }
}
