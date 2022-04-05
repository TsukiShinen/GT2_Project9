using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
	[SerializeField] private GameParameters parameters;
	public GameObject bullet;
	public Transform canon;
	
	public Transform Target { get; set; }

	private float _timerShoot;
	private bool _aimed;
	private bool CanShoot => _timerShoot <= 0;

	private void Update()
	{
		if (_timerShoot > 0)
			_timerShoot -= Time.deltaTime;
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
		GameObject.Instantiate(bullet, canon.position, Quaternion.Euler(canon.eulerAngles) * Quaternion.Euler(0, 0, 180f));
		Debug.Log("boom");
		yield return null;
	}
}
