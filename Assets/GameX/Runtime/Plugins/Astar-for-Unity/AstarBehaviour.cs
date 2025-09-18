using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������ڡ�GridFloor��Ԥ�����ϣ�ͨ����ײ����ȡλ�����꣬���ò���Ѱ·������
/// ע����GridFloor��Ԥ��������Ҫ�ӡ�RigidBody���������
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
    /// ���Ѱ·����
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