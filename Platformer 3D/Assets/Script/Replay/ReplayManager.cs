using System;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    [SerializeField] private ReplayContainer _replayContainer;
    
    private List<IReplayObject> replayObjects = new List<IReplayObject>();
    
    public static ReplayManager instance;
    
    public State currentState;
    
    public float snapshotDelta;

    private float _snapshotDeltaTotal;
    
    private int _snapshotIndex = 0;
    
    public enum State
    {
        Idle,
        Record,
        Playback
    }
    
    private void Awake()
    {
        instance = this;
        _replayContainer.Init();
    }

    public void Register(IReplayObject replayObject)
    {
        if (replayObject == null)
        {
            replayObjects = new List<IReplayObject>();
        }
        replayObjects.Add(replayObject);
        print(replayObject);
    }

    public void StartRecording()
    {
        currentState = State.Record;
    }

    public void StartPlayback()
    {
        currentState = State.Playback;
    }

    public void StopRecording()
    {
        currentState = State.Idle;
    }

    public void FixedUpdate()
    {
        if (currentState == State.Record)
        {
            _snapshotDeltaTotal += Time.fixedDeltaTime;
            if (_snapshotDeltaTotal >= snapshotDelta)
            { 
                TakeSnapshot();
                
                _snapshotDeltaTotal -= snapshotDelta;
            }
        }
    }

    private float _timer;
    private void TakeSnapshot()
    {
        
        _timer += Time.fixedDeltaTime;
        
        SnapshotData data = new SnapshotData(_timer);

        foreach (IReplayObject replayObject in replayObjects)
        {
            data.AddObjectSnapshot(((UnityEngine.Object)replayObject).name, replayObject.SaveSnapshot());
        }
        
        _replayContainer.AddSnapshot(data);
        _snapshotIndex++;
    }
}
