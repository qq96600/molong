using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class db_store_vo : Base_VO
{
    /// <summary>
    /// �̵����� 1�������̵� 2���޹��̵� 3�������̵�
    /// </summary>
    public int store_Type;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public string ItemName;
    /// <summary>
    /// ��Ʒ�۸�
    /// </summary>
    public int ItemPrice;
    /// <summary>
    /// ���������
    /// </summary>
    public int ItemMaxQuantity;

}
