using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UI;
using MVC;

/// <summary>
/// Weighted��
/// </summary>
public class WeightedItem
{
    public string prizedraw;
    public int Weight; // Ȩ��
}

public class WeightedRandomPicker
{
    private List<WeightedItem> items;
    private List<int> cumulativeWeights; // Ȩ�ص��ۼƺ�

    public WeightedRandomPicker(List<WeightedItem> items)
    {
        this.items = items;
        this.cumulativeWeights = new List<int>();

        // �����ۼ�Ȩ��
        int cumulativeWeight = 0;
        foreach (var item in items)
        {
            cumulativeWeight += item.Weight;
            cumulativeWeights.Add(cumulativeWeight);
        }
    }

    // ��ȡһ�����ѡ�����Ʒ
    public WeightedItem GetRandomItem()
    {
        int totalWeight = cumulativeWeights[cumulativeWeights.Count - 1]; // ���һ��ֵ��������ƷȨ�ص��ܺ�
        int randomValue = UnityEngine.Random.Range(0, totalWeight); // ����һ��0����Ȩ��֮��������
        // �������ֵѡ����Ʒ
        for (int i = 0; i < cumulativeWeights.Count; i++)
        {
            if (randomValue < cumulativeWeights[i]) // ������ֵС�ڵ�ǰ�ۼ�Ȩ�أ���ѡ�и���Ʒ
            {
                return items[i];
            }
        }
        return null; // ���������ϲ��ᱻ��������ֹ���뾯��
    }
    /// <summary>
    /// ��ʾ����
    /// </summary>
    /// <returns></returns>
    public string Show_Success_rate()
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();
        int totalWeight = cumulativeWeights[cumulativeWeights.Count - 1];
        foreach (var item in items)
        {
            if (!dic.ContainsKey(item.prizedraw))
            { 
                dic.Add(item.prizedraw, item.Weight);
            }
            else dic[item.prizedraw] += item.Weight;
        }
        string str = "";
        foreach (var item in dic.Keys)
        {
            str+= item + ":��ȡ���� " + (dic[item] * 100.0f / totalWeight).ToString("f2") + "%\n";
        }
        return str;
    }
}
