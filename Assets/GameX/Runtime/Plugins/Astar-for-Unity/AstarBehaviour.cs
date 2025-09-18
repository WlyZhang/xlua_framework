using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这个组件挂在【GridFloor】预制体上，通过碰撞检测获取位置坐标，设置不可寻路的网格
/// 注：【GridFloor】预制体上需要加【RigidBody】重力组件
/// </summary>
public class AstarBehaviour : MonoBehaviour
{
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        CheckNavition(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckNavition(collision.gameObject);
    }

    /// <summary>
    /// 检测寻路网格
    /// </summary>
    /// <param name="other"></param>
    private void CheckNavition(GameObject other)
    {
        GameObject go = GridMaster.Instance.notNavList.Find(x => x.Equals(other));
        if (go == null)
            return;

        Node node = GridMaster.Instance.GetNodeFromVector3(go.transform.position);
        if (node == null)
            return;

        GridMaster.Instance.SetNodesWithNoNavition(node);
    }
}