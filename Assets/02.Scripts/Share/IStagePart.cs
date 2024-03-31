using UnityEngine;

public interface IStageParts
{
    Transform ObjTr { get; }
    void SendPart();
    void AddPartsList();
}