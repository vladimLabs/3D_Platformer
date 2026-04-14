using UnityEngine;

public interface IReplayObject
{
    SnapshotInfo SaveSnapshot();
    
    void LoadSnapshot(SnapshotInfo snapshotInfo);
}
