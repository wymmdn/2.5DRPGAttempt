using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FireBearStates;

public class FireBear : Enemy,IStoryActor
{
    [SerializeField] private CaveKey caveKey;
    public AnimationCurve curve;
    public float chaseRadius;
    //public float alertRadius;
    public float secondAttackRadius;
    public float closeRadius;
    //public float attackRadius;
    public string actorName { get; set; }
    public Conversation conversation { get; set; }
    
    private float duration = 0.5f;
    private float maxHeight = 1.0f;
    private Weapon secondWeapon;
    private EnemyState chase1State;
    private EnemyState secondAttackState;
    private EnemyState chase2State;

    protected override void Awake()
    {
        base.Awake();
        //init states
        idleState = new FireBearIdleState();
        chase1State = new FireBearChase1State();
        secondAttackState = new FireBearSecondAttackState();
        chase2State = new FireBearChase2State();
        attackState = new FireBearAttackState();
        states.Add(stateType.idle, idleState);
        states.Add(stateType.chase1, chase1State);
        states.Add(stateType.secondAttack, secondAttackState);
        states.Add(stateType.chase2, chase2State);
        states.Add(stateType.attack, attackState);

        //equip secondWeapon
        secondWeapon = Instantiate((GameObject)Resources.Load(GloblePath.fireBoom), transform).GetComponent<Weapon>();
        secondWeapon.master = this;
        secondWeapon.isPickable = false;
        secondWeapon.transform.position = this.transform.position + secondWeapon.positionOffset;

        //
        player = GameObject.FindGameObjectWithTag(tagtag.player);
        chaseRadius = alertRadius + 1.0f;
        secondAttackRadius = secondWeapon.attackRadius;
        closeRadius = 1f;
        actorName = roleName.fireBear;
    }
    public IEnumerator startPerform()
    {
        isTalking = true;
        float timeCnt = 0f;
        yield return new WaitForSeconds(1.0f);
        while (timeCnt < 2.0f)
        {
            var speed = moveSpeed;
            moveSpeed = 0.5f;
            movement(player.transform.position);
            moveSpeed = speed;
            timeCnt += Time.deltaTime;
            yield return null;
        }
        stopMove();
        openDialogue();
    }
    public void endPerform()
    { 
        isTalking = false;
    }
    protected override void UpdateContent() // called in update
    {

    }
    public override void attack()
    {
        rb.velocity = Vector2.zero;
        weapon.attack();
    }
    public void secondAttack()
    {
        rb.velocity = Vector2.zero;
        secondWeapon.attack();
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)this.transform.position, alertRadius);
        Gizmos.DrawWireSphere((Vector2)this.transform.position, attackRadius);
        Gizmos.DrawWireSphere((Vector2)this.transform.position, chaseRadius);
        Gizmos.DrawWireSphere((Vector2)this.transform.position, secondAttackRadius);
    }
    public override void toDead()
    {
        //EventHandler.CallTreeMonster(StoryManager.dead);
        Item item = Instantiate(caveKey, this.transform.parent);
        Vector3 generatePoint = Random.insideUnitCircle * 0.4f;
        generatePoint = (Mathf.Abs(generatePoint.x) > 0.1f || Mathf.Abs(generatePoint.x) > 0.1f) ? generatePoint : new Vector2(0.1f, 0f);
        StartCoroutine(Curve(transform.position, transform.position + generatePoint, item.transform));
        var transforms = secondWeapon.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.name.StartsWith("boob"))
            {
                t.parent = this.transform.parent;
            }
        }
        base.toDead();
    }
    public void openDialogue()
    {
        conversation = StoryManager.instance.GetConversation(actorName);
        EventHandler.CallShowDialogueEvent(conversation);
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
        IPickable ip = tf.GetComponent<IPickable>();
        if (ip != null) ip.isPickable = true;
    }
}
