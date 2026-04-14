using UnityEngine;

[System.Serializable]
public class SnapshotInfo
{
    public string id;
}

[System.Serializable]
public class PlayerSnapshotInfo: SnapshotInfo
{
    public Vector3 velocity;
    public Vector3 angularVelocity;
}
