using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_collect_vo : Base_VO
{
    /// <summary>
    /// ��Ҫ�ռ���Ʒ������
    /// </summary>
    public string Name;

    /// <summary>
    /// ��Ҫ�ռ���Ʒ������
    /// </summary>
    public string StdMode;

    /// <summary>
    /// �ռ�������ӵ�����
    /// </summary>
    public string bonuses_type;

    /// <summary>
    /// �ռ�������ӵ���������
    /// </summary>
    public string[] bonuses_types;

    /// <summary>
    /// �ռ�������ӵ�����ֵ
    /// </summary>
    public string bonuses_value;

    /// <summary>
    /// �ռ�������ӵ�����ֵ����
    /// </summary>
    public string[] bonuses_values;
    /// <summary>
    /// ��Ʒ�Ƿ��ռ� 0��δ�ռ� 1�����ռ�
    /// </summary>
    public int isCollect = 0;
    public void Init()
    {
        bonuses_types = bonuses_type.Split(' ');
        bonuses_values= bonuses_value.Split(' ');
    }
}
