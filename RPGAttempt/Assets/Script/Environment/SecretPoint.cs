using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPoint : MonoBehaviour,IInteraction
{
    [SerializeField]private List<Item> items = new List<Item>();
    private Dictionary<Item,bool> content = new Dictionary<Item,bool>();
    public AnimationCurve curve;
    private float duration = 0.5f;
    private float maxHeight = 1.0f;

    private void Start()
    {
        foreach (var i in items)
        {
            content.Add(i, true);
        }
    }
    public void interact(Role role)
    {
        foreach (var i in items)
        {
            if (content[i] == true)
            { 
                Item item = Instantiate(i,this.transform.parent);
                StartCoroutine(Curve(transform.position, transform.position + (Vector3)Random.insideUnitCircle * 0.5f, item.transform));
                content[i] = false;
            }
        }
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
