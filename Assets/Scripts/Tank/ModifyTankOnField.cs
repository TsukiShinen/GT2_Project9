using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WhatIsGround))]
public class ModifyTankOnField : MonoBehaviour
{
    [SerializeField] private GameParameters _parameters;
    [SerializeField] private Tank _tank;

    private WhatIsGround _ground;

    private void Awake()
    {
        _ground = GetComponent<WhatIsGround>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_ground.GroundTag == null) { return; }
        float newSpeed = _parameters.TankSpeedNormal;
        if (_ground.GroundTag == _parameters.TagGroundQuick)
        {
            newSpeed = _parameters.TankSpeedQuick;
        }
        if (_ground.GroundTag == _parameters.TagGroundNormal)
        {
            newSpeed = _parameters.TankSpeedNormal;
        }
        if (_ground.GroundTag == _parameters.TagGroundSlow)
        {
            newSpeed = _parameters.TankSpeedSlow;
        }
        Debug.Log(_ground.GroundTag + "   " + newSpeed);

        _tank.Speed = newSpeed;
    }
}
