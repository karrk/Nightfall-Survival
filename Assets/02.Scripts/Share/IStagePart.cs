using UnityEngine;

public interface IStageParts
{
    public Transform StagePartTransform { get; }
    public void SendPart();
    public void AddPartsList();
}