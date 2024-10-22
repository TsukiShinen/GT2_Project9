
using UnityEngine;

/// <summary>
/// Ne pas modifier cette classe !
/// </summary>
[CreateAssetMenu]
public class GameParameters : ScriptableObject
{
    [Header("Teams")]

    [Tooltip("La couleur de la Team 1")]
    [SerializeField] private Color _team1Color;
    public Color Team1Color { get => _team1Color; }

    [Tooltip("La couleur de la Team 2")]
    [SerializeField] private Color _team2Color;
    public Color Team2Color { get => _team2Color; }

    [Header("Match")]

    [Tooltip("La dur�e en s d'un match")]
    [SerializeField] private float _matchTimeLimit;
    public float MatchTimeLimit { get => _matchTimeLimit; }

    [Header("Tanks")]

    [Tooltip("Le nombre de pv d'un tank")]
    [SerializeField] private float _tankHealth;
    public float TankHealth { get => _tankHealth; }

    [Tooltip("La vitesse en m/s d'un tank")]
    [SerializeField] private float _tankSpeed;
    public float TankSpeed { get => _tankSpeed; }

    [Tooltip("La vitesse de rotation d'un tank en �/s")]
    [SerializeField] private float _tankTurnSpeed;
    public float TankTurnSpeed { get => _tankTurnSpeed; }

    [Tooltip("La vitesse de rotation du canon d'un tank en �/s")]
    [SerializeField] private float _tankCanonTurnSpeed;
    public float TankCanonTurnSpeed { get => _tankCanonTurnSpeed; }

    [Tooltip("Le d�lai en s entre deux tirs cons�cutifs d'un tank")]
    [SerializeField] private float _tankShootDelay;
    public float TankShootDelay { get => _tankShootDelay; }

    [Tooltip("La vitesse en m/s d'un obus de tank")]
    [SerializeField] private float _tankShellSpeed;
    public float TankShellSpeed { get => _tankShellSpeed; }

    [Tooltip("La dur�e de vie en s d'un obus de tank")]
    [SerializeField] private float _tankShellDuration;
    public float TankShellDuration { get => _tankShellDuration; }

    [Tooltip("Les d�g�ts de l'explosion d'un obus de tank")]
    [SerializeField] private float _tankShellDamage;
    public float TankShellDamage { get => _tankShellDamage; }

    [Tooltip("La distance en m apr�s laquelle une explosion d'obus ne fait plus de d�g�t")]
    [SerializeField] private float _tankShellDamageFalloff;
    public float TankShellDamageFalloff { get => _tankShellDamageFalloff; }

    [Header("Zone")]

    [Tooltip("La vitesse en %/s de croissance de la jauge de capture")]
    [SerializeField] private float _zoneCaptureSpeed;
    public float ZoneCaptureSpeed { get => _zoneCaptureSpeed; }

    [Tooltip("La vitesse en %/s de d�croissance de la jauge de capture")]
    [SerializeField] private float _zoneDecaySpeed;
    public float ZoneDecaySpeed { get => _zoneDecaySpeed; }

    [Tooltip("Le d�lai en s avant lequel une zone captur�e commence � perdre de sa jauge de capture s'il n'y a plus de tank")]
    [SerializeField] private float _zoneDecayDelay;
    public float ZoneDecayDelay { get => _zoneDecayDelay; }

    [Tooltip("La distance en m de vision d'un tank, pour le fog of war et la d�tection")]
    [SerializeField] private float _tankVisionRange;
    public float TankVisionRange { get => _tankVisionRange; }

    [Header("Terrain")]

    [Tooltip("La vitesse d'un tank en m/s sur terrain normal")]
    [SerializeField] private float _tankSpeedNormal;
    public float TankSpeedNormal { get => _tankSpeedNormal; }

    [Tooltip("La vitesse d'un tank en m/s sur terrain ralenti")]
    [SerializeField] private float _tankSpeedSlow;
    public float TankSpeedSlow { get => _tankSpeedSlow; }

    [Tooltip("La vitesse d'un tank en m/s sur terrain rapide")]
    [SerializeField] private float _tankSpeedQuick;
    public float TankSpeedQuick { get => _tankSpeedQuick; }

    [Header("Tags")]

    [SerializeField] private string _tagTank;
    public string TagTank { get => _tagTank; }

    [SerializeField] private string _tagShell;
    public string TagShell { get => _tagShell; }

    [SerializeField] private string _tagZone;
    public string TagZone { get => _tagZone; }

    [SerializeField] private string _tagSpawn;
    public string TagSpawn { get => _tagSpawn; }

    [SerializeField] private string _tagHealthPickup;
    public string TagHealthPickup { get => _tagHealthPickup; }

    [SerializeField] private string _tagShield;
    public string TagShield { get => _tagShield; }

    [SerializeField] private string _tagGroundNormal;
    public string TagGroundNormal { get => _tagGroundNormal; }

    [SerializeField] private string _tagGroundSlow;
    public string TagGroundSlow { get => _tagGroundSlow; }

    [SerializeField] private string _tagGroundQuick;
    public string TagGroundQuick { get => _tagGroundQuick; }

    [Header("Layers")]

    [SerializeField] private string _layerBackground;
    public string LayerBackground { get => _layerBackground; }
    public int LayerBackgroundAsLayer { get => LayerMask.NameToLayer(LayerBackground); }
    public LayerMask LayerBackgroundAsMask { get => LayerMask.GetMask(LayerBackground); }


    [SerializeField] private string _layerNavigationObstacle;
    public string LayerNavigationObstacle { get => _layerNavigationObstacle; }
    public int LayerNavigationObstacleAsLayer { get => LayerMask.NameToLayer(LayerNavigationObstacle); }
    public LayerMask LayerNavigationObstacleAsMask { get => LayerMask.GetMask(LayerNavigationObstacle); }


    [SerializeField] private string _layerVisionObstacle;
    public string LayerVisionObstacle { get => _layerVisionObstacle; }
    public int LayerVisionObstacleAsLayer { get => LayerMask.NameToLayer(LayerVisionObstacle); }
    public LayerMask LayerVisionObstacleAsMask { get => LayerMask.GetMask(LayerVisionObstacle); }


    [SerializeField] private string _layerObstacle;
    public string LayerObstacle { get => _layerObstacle; }
    public int LayerObstacleAsLayer { get => LayerMask.NameToLayer(LayerObstacle); }
    public LayerMask LayerObstacleAsMask { get => LayerMask.GetMask(LayerObstacle); }


    [SerializeField] private string _layerTank;
    public string LayerTank { get => _layerTank; }
    public int LayerTankAsLayer { get => LayerMask.NameToLayer(LayerTank); }
    public LayerMask LayerTankAsMask { get => LayerMask.GetMask(LayerTank); }


    [SerializeField] private string _layerShell;
    public string LayerShell { get => _layerShell; }
    public int LayerShellAsLayer { get => LayerMask.NameToLayer(LayerShell); }
    public LayerMask LayerShellAsMask { get => LayerMask.GetMask(LayerShell); }


    [SerializeField] private string _layerShield;
    public string LayerShield { get => _layerShield; }
    public int LayerShieldAsLayer { get => LayerMask.NameToLayer(LayerShield); }
    public LayerMask LayerShieldAsMask { get => LayerMask.GetMask(LayerShield); }


    [SerializeField] private string _layerZone;
    public string LayerZone { get => _layerZone; }
    public int LayerZoneAsLayer { get => LayerMask.NameToLayer(LayerZone); }
    public LayerMask LayerZoneAsMask { get => LayerMask.GetMask(LayerZone); }


    [SerializeField] private string _layerSpawn;
    public string LayerSpawn { get => _layerSpawn; }
    public int LayerSpawnAsLayer { get => LayerMask.NameToLayer(LayerSpawn); }
    public LayerMask LayerSpawnAsMask { get => LayerMask.GetMask(LayerSpawn); }


    [SerializeField] private string _layerPickup;
    public string LayerPickup { get => _layerPickup; }
    public int LayerPickupAsLayer { get => LayerMask.NameToLayer(LayerPickup); }
    public LayerMask LayerPickupAsMask { get => LayerMask.GetMask(LayerPickup); }



    [SerializeField] private string _layerGroundNormal;
    public string LayerGroundNormal { get => _layerGroundNormal; }
    public int GroundNormalAsLayer { get => LayerMask.NameToLayer(LayerGroundNormal); }
    public LayerMask GroundNormalAsMask { get => LayerMask.GetMask(LayerGroundNormal); }


    [SerializeField] private string _layerGroundQuick;
    public string LayerGroundQuick { get => _layerGroundQuick; }
    public int LayerGroundQuickAsLayer { get => LayerMask.NameToLayer(LayerGroundQuick); }
    public LayerMask LayerGroundQuickAsMask { get => LayerMask.GetMask(LayerGroundQuick); }


    [SerializeField] private string _layerGroundSlow;
    public string LayerGroundSlow { get => _layerGroundSlow; }
    public int LayerGroundSlowAsLayer { get => LayerMask.NameToLayer(LayerGroundSlow); }
    public LayerMask LayerGroundSlowAsMask { get => LayerMask.GetMask(LayerGroundSlow); }
}
