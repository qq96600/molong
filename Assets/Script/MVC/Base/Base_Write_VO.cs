using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    /// <summary>
    /// д��ģ��
    /// </summary>
    public class Base_Wirte_VO
    {
        public Mysql_Type type;//д������
        public Mysql_Table_Name tableName;//д�����
        public string[] columnNames;//��������
        public string[] columnValues;//������ֵ
        public bool exist = true;//�Ƿ�д��
    }

}
