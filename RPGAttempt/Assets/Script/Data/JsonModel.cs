using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Conversation
{
    public string actorName { get; set; }
    public string index { get; set; }
    public List<ConvStep> convSteps { get; set; }

}

public class ConvStep
{
    public int index { get; set; }
    public int nextIndex { get; set; }
    public string speaker { get; set; }
    public string type { get; set; }
    public string words { get; set; }
    public string branch { get; set; }
}
