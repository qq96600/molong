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
    mo_user_plant,//�û���ֲ��Ϣ
    db_plant,//��ֲ��Ϣ
    db_pet,//������Ϣ
    mo_user_pet_hatching,//�û����������Ϣ
    db_pet_explore,//����̽����Ϣ
    mo_user_world,
    db_lv,//�ȼ���Ϣ
    user_rank,//���а�
    db_hall,//������Ϣ
    db_seed,//����������Ϣ
    mo_user_seed,//�û�����������Ϣ
}
