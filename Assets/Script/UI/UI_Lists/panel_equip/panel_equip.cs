using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
/// <summary>
/// չʾװ���Ľ���
/// </summary>
public class panel_equip : Panel_Base
{

    /// <summary>
    /// �洢��ǰ��Ʒ �ʹ�����Ʒ
    /// </summary>
    private bag_item crt_bag, crt_equip;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Show()
    {
        base.Show();
    }
    /// <summary>
    /// ���ݲ���
    /// </summary>
    /// <param name="bag"></param>
    public void Init(bag_item bag)
    { 
        crt_bag = bag;
    }
}
