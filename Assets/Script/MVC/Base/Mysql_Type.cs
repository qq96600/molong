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
    db_setting,//��׼������Ϣ
    db_artifact,//��׼����
    mo_user_base,//�û�������Ϣ
    mo_user_value,//�û���Դ��Ϣ
    mo_user_hero,//�û�Ӣ����Ϣ
    mo_user_setting,//�û�������Ϣ
    mo_user_artifact,//�û�������Ϣ
    mo_user,//�û�������Ϣ
    loglist,//��־
    user_login,//�û���¼
    db_pass,//ͨ��֤
    mo_user_pass,//�û�ͨ��֤

}
