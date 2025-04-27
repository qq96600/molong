using UnityEngine;
using UnityEditor;

public class PrefabFinder 
{
    public Transform searchRoot; // 搜索的起始根节点

    // 根据名称查找预制体实例
    public GameObject FindPrefabInstanceByName(string targetName)
    {
        return FindRecursive(searchRoot.gameObject, targetName);
    }

    private GameObject FindRecursive(GameObject current, string targetName)
    {
        if (current == null) return null;

        // 检查名称是否匹配，并且是预制体实例
        if (current.name == targetName && IsPrefabInstance(current))
        {
            return current;
        }

        // 遍历所有子物体
        foreach (Transform child in current.transform)
        {
            GameObject result = FindRecursive(child.gameObject, targetName);
            if (result != null) return result;
        }

        return null;
    }

    // 判断物体是否为预制体实例（仅在编辑器下有效）
    private bool IsPrefabInstance(GameObject obj)
    {
#if UNITY_EDITOR
        PrefabInstanceStatus status = PrefabUtility.GetPrefabInstanceStatus(obj);
        return status == PrefabInstanceStatus.Connected || status == PrefabInstanceStatus.Disconnected;
#else
        return false; // 运行时无法检测
#endif
    }
}