using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public GameParameters parameters;
    private Tank _myTank;
    public float LifePoints;
    public bool IsAlive => LifePoints > 0;

    private SpriteRenderer _spriteRenderer;
    
    private static readonly int Fill = Shader.PropertyToID("_Fill");

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _myTank = GetComponentInParent<Tank>();
        LifePoints = parameters.TankHealth;
    }

    public void TakeDamage(float damage)
    {
        LifePoints -= damage;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.position = _myTank.transform.position + new Vector3(0, 2, 0);
        UpdateParams();
        if (IsAlive || _myTank.isDead) { return; }
        StartCoroutine(_myTank.TankDeath());
    }

    private void UpdateParams()
    {
        var fill = LifePoints / parameters.TankHealth;
        _spriteRenderer.material.SetFloat(Fill, fill);
    }

}
