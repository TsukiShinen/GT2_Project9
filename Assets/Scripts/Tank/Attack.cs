using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
	[SerializeField] private GameParameters parameters;
	[SerializeField] private GameObject bullet;
	[SerializeField] private Transform canon;
	[SerializeField] private Animator animator;

	public Transform Target { get; set; }

	private float _timerShoot;
	private bool _aimed;
	private bool CanShoot => _timerShoot <= 0;

    private void Start()
    {
		_timerShoot = parameters.TankShootDelay;
	}

    private void Update()
	{
		if (_timerShoot > 0)
			_timerShoot -= Time.deltaTime;

		if (Target == null) return;

		Aim();
		ShootUpdate();
	}

    private void Aim()
	{
		Vector2 targetDir = canon.position - Target.position;
		var angle = Vector2.SignedAngle(targetDir, canon.up);
		if (Mathf.Abs(angle) > 1f)
		{
			_aimed = false;
			canon.Rotate(new Vector3(0, 0, (parameters.TankTurnSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
		}
		else
		{
			_aimed = true;
		}
	}

	private void ShootUpdate()
	{
		if(!CanShoot) { return; }
		if(!_aimed) { return; }
		_timerShoot = parameters.TankShootDelay;
		StartCoroutine(Shoot());
	}
	
	private IEnumerator Shoot()
	{
		var newBullet = Instantiate(bullet, canon.position, Quaternion.Euler(canon.eulerAngles) * Quaternion.Euler(0, 0, 180f)).GetComponent<Bullet>();
		newBullet.SetTank(gameObject.GetComponent<Collider2D>());
		newBullet.Init(canon.position, Target.position);
		animator.SetTrigger("Shooting");
		yield return new WaitForSeconds(0.25f);
	}
}
