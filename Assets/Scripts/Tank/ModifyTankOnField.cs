using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WhatIsGround))]
public class ModifyTankOnField : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;
    
    private Tank _tank;
    private WhatIsGround _ground;

    private void Awake()
    {
        _ground = GetComponent<WhatIsGround>();
        _tank = GetComponent<Tank>();
    }

    private void Update()
    {
        if (_ground.GroundTag == null) { return; }
        var newSpeed = parameters.TankSpeedNormal;
        if (_ground.GroundTag == parameters.TagGroundQuick)
        {
            newSpeed = parameters.TankSpeedQuick;
        }
        if (_ground.GroundTag == parameters.TagGroundNormal)
        {
            newSpeed = parameters.TankSpeedNormal;
        }
        if (_ground.GroundTag == parameters.TagGroundSlow)
        {
            newSpeed = parameters.TankSpeedSlow;
        }

        _tank.Movement.Speed = newSpeed;
    }
}
