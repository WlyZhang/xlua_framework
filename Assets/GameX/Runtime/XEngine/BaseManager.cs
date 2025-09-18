using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseManager
{
    public abstract bool IsLoad { get; set; }
    public abstract UniTask<bool> Init();
    public abstract void Update(float time);
    public abstract void Destroy();

}