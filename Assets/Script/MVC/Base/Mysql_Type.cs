using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ִ������
/// </summary>
public enum Mysql_Type
{
    MySqlDataReader,//��ȡʱ��
    InsertInto,//��ʼ��
    UpdateInto,//����
    Delete//ɾ��
}

/// <summary>
/// ����
/// </summary>
public enum Mysql_Table_Name
{
    db_monster,
    db_stditems,
    db_magic,
    db_map,
    db_hero,//��׼����
    mo_user_base,//�û�������Ϣ
    mo_user_value,//�û���ֵ��Ϣ
    mo_user_hero

}
