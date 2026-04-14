using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Replay Container ")]
public class ReplayContainer : ScriptableObject
{
    [SerializeField] 
    private List<SnapshotData> _snapshots;
    
    public void Init()
    {
        _snapshots = new List<SnapshotData>();
    }

    public void AddSnapshot(SnapshotData snapshot)
    {
        _snapshots.Add(snapshot);
        
    }

    public bool GetSnapshot(int index, out SnapshotData data)
    {
        if (index >= _snapshots.Count)
        {
            data = new SnapshotData(-1);
            return false;
        }

        if (index < 0)
        {
            data = new SnapshotData(-1);
            return false;
        }
        
        data = _snapshots[index];
        
        return true;
    }
}
