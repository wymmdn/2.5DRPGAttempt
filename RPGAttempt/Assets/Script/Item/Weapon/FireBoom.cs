using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoom : Weapon
{
    public Boob boob;
    public GameObject projectile;
    public AnimationCurve curve;
    private float duration = 0.6f;
    private float maxHeight = 1.0f;

    protected override void playAttack()
    {
        if (transform.childCount < 10)
        {
            for (int i = 0; i < 3; i++)
            {
                Transform boobTf = Instantiate(boob, transform).GetComponent<Transform>();
                StartCoroutine(Curve(transform.position, attackTarget.position + (Vector3)Random.insideUnitCircle * 0.1f, boobTf));
            }
        }
        master.animatorManager.attackAnimation(attackDir);
        GameObject go = Instantiate(projectile, transform.position, new Quaternion(), transform);
        if(attackTarget != null)
            go.GetComponent<FireBoomProjectile>()?.launch(attackTarget.gameObject);
    }
    public IEnumerator Curve(Vector3 start, Vector3 finish, Transform tf)
    {
        var timeCnt = 0f;
        while (timeCnt < duration)
        {
            timeCnt += Time.deltaTime;
            var linearTime = timeCnt / duration;
            var heightTime = curve.Evaluate(linearTime);
            var height = Mathf.Lerp(0f, maxHeight, heightTime);
            tf.position = new Vector3(0f, height, 0f) + Vector3.Lerp(start, finish, linearTime);
            yield return null;
        }
    }
}
