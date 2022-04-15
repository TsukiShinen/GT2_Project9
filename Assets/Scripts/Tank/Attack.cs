using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
	[SerializeField] private GameParameters parameters;
	public GameObject bullet;
	public Transform canon;
	[SerializeField] private Animator _shootAnim;

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

	public void Aim()
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

	public void ShootUpdate()
	{
		if(!CanShoot) { return; }
		if(!_aimed) { return; }
		_timerShoot = parameters.TankShootDelay;
		StartCoroutine(Shoot());
	}
	
	private IEnumerator Shoot()
	{
		GameObject newBullet = Instantiate(bullet, canon.position, Quaternion.Euler(canon.eulerAngles) * Quaternion.Euler(0, 0, 180f));
		newBullet.gameObject.GetComponent<Bullet>().SetTank(gameObject.GetComponent<Collider2D>());
		_shootAnim.SetTrigger("Shooting");
		yield return new WaitForSeconds(0.25f);
	}
}
