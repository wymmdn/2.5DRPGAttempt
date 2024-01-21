using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeMonsterStates;

public class TreeMonster : Enemy
{
    [SerializeField]private Item freezeSword;
    [SerializeField]private Item deadBody;
    public AnimationCurve curve;
    private float duration = 0.5f;
    private float maxHeight = 1.0f;
    protected override void Awake()
    {
        base.Awake();
        idleState = new TreeMonsterIdleState();
        chaseState = new TreeMonsterChaseState();
        attackState = new TreeMonsterAttackState();
        states.Add(stateType.idle, idleState);
        states.Add(stateType.chase,chaseState);
        states.Add(stateType.attack, attackState);
        this.weapon.attackInterval = 1.5f;
    }

    protected override void Start()
    {
        base.Start();
    }
    public override void toDead()
    {
        EventHandler.CallTreeMonster(StoryManager.dead);
        Item body = Instantiate(deadBody, this.transform.position, new Quaternion(), this.transform.parent);
        Item item = Instantiate(freezeSword, this.transform.parent);

        Vector3 generatePoint = Random.insideUnitCircle * 0.4f;
        generatePoint = (Mathf.Abs(generatePoint.x) > 0.1f || Mathf.Abs(generatePoint.x) > 0.1f) ? generatePoint : new Vector2(0.1f, 0f);
        StartCoroutine(Curve(transform.position, transform.position + generatePoint, item.transform));
        StartCoroutine(fadeOut(body.GetComponent<SpriteRenderer>(), 1f));

        base.toDead();
    }
    public IEnumerator fadeOut(SpriteRenderer sp,float fadeTime)
    {
        var color = sp.color;
        var timeCnt = 0f;
        while (timeCnt < fadeTime)
        {
            timeCnt += Time.deltaTime;
            color = new Color(color.r, color.g, color.b, (timeCnt/fadeTime)*255);
            sp.color = color;
            yield return null;
        }
        sp.color = new Color(color.r, color.g, color.b, 255);
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
