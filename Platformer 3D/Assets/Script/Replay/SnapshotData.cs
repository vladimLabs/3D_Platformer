using System;
using System.Collections.Generic;

[System.Serializable]
public struct SnapshotData
{
    public float frameTime;

    public List<string>  name;
    public  List<SnapshotInfo> snapshots;

    public SnapshotData(float frameTime)
    {
        this.frameTime = frameTime;
        snapshots = new List<SnapshotInfo>();
        name = new List<string>();
    }

    public void AddObjectSnapshot(string id, SnapshotInfo data)
    {
        name.Add(id);
        snapshots.Add(data);
    }
}
