using Common;
using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bag_seed_vo : Base_VO
{
    /// <summary>
    /// 0���� 1����ʱ�� 2Ч��
    /// </summary>
    private List<(string,List<string>)> seedList;
    /// <summary>
    /// �䷽�б�
    /// </summary>
    public string formula_value;
    /// <summary>
    /// ʹ������
    /// </summary>
    public string use_value;
    /// <summary>
    /// �䷽ 0���� 1�ȼ� 2�������
    /// </summary>
    private List<(string, List<string>)> formulalist;
    /// <summary>
    /// ʹ���б� ���� 0���� 1�ۻ�ֵ
    /// </summary>
    private List<(string, List<int>)> useList;

    public void Init()
    { 
        seedList = new List<(string,List<string>)>();
        formulalist= new List<(string, List<string>)>();
        useList = new List<(string, List<int>)>();
        string[] splits = user_value.Split('&');
        for (int i = 0; i < splits.Length; i++)
        {
            if (splits[i] != "")
            { 
                string[] split = splits[i].Split(',');
                seedList.Add((split[0], new List<string>()));
                for (int j = 1; j < split.Length; j++)
                { 
                    seedList[i].Item2.Add(split[j]);
                }
            }
        }

        string[] formula_splits = formula_value.Split('&');
        for (int i = 0; i < formula_splits.Length; i++)
        {
            if (formula_splits[i] != "")
            {
                string[] split = formula_splits[i].Split(',');
                formulalist.Add((split[0], new List<string>()));
                for (int j = 1; j < split.Length; j++)
                {
                    formulalist[i].Item2.Add(split[j]);
                }
            }
        }

        string[] use_value_splits = use_value.Split('&');
        for (int i = 0; i < use_value_splits.Length; i++)
        {
            if (use_value_splits[i] != "")
            {
                string[] split = use_value_splits[i].Split(',');
                if (split.Length > 1)
                {
                    useList.Add((split[0], new List<int>()));
                    string[] split1 = split[1].Split('|');
                    for (int j = 0; j < split1.Length; j++)
                    { 
                        useList[i].Item2.Add(int.Parse(split1[j]));
                    }
                }
            }
        }

    }
    /// <summary>
    /// ��ȡ��ҩ�б�
    /// </summary>
    /// <returns></returns>
    public List<(string, List<string>)> GetSeedList()
    { 
      return seedList;
    }
    /// <summary>
    /// ��ȡ�䷽�б�
    /// </summary>
    /// <returns></returns>
    public List<(string, List<string>)> Getformulalist()
    {
        return formulalist;
    }
    /// <summary>
    /// ��ȡʹ���б�
    /// </summary>
    /// <returns></returns>
    public List<(string, List<int>)> GetuseList()
    { 
        return useList;
    }
    /// <summary>
    /// �����Ʒ
    /// </summary>
    /// <param name="split"></param>
    public void Set( List<string> split)
    {
        (string,List<string>) temp = (split[0], new List<string>());
        for (int j = 1; j < split.Count; j++)
        {
            temp.Item2.Add(split[j]);
        }
        seedList.Add(temp);
    }
    /// <summary>
    /// д�뵤��
    /// </summary>
    /// <param name="split"></param>
    public void Setformula((string, List<string>) split)
    {
        formulalist.Add(split);
    }

    /// <summary>
    /// �Ƴ���ҩ
    /// </summary>
    /// <param name="split">ֵ</param>
    /// <param name="index">���� 1 ��ҩ2����</param>
    public void usedata((string, List<string>) split,int index=1)
    {
        if (index == 2)
        {
            seedList.Remove(split);
        }else  
        formulalist.Remove(split);
    }

    /// <summary>
    /// ʹ�õ�ҩ
    /// </summary>
    /// <param name="split"></param>
    public void Setuse((string, List<int>) split)
    {
        for (int i = 0; i < useList.Count; i++)
        {
            if (useList[i].Item1 == split.Item1)
            {
                (string, List<int>) temp = (useList[i].Item1, new List<int>());
                for (int j = 0; j < split.Item2.Count; j++)
                { 
                    temp.Item2.Add(useList[i].Item2[j] + split.Item2[j]);
                }
                useList[i] = temp;
                return;
            }
        }
    }
    /// <summary>
    /// д�뵤ҩ
    /// </summary>
    private string DataSet()
    { 
        string value="";
        for (int i = 0; i < seedList.Count; i++)
        {
            value += (value == "" ? "" : "&") + seedList[i].Item1;
            for (int j = 0 ; j < seedList[i].Item2.Count; j++)
            {
                value+= "," + seedList[i].Item2[j];
            }
        }
        return value;
    }
    /// <summary>
    /// д��ʹ��
    /// </summary>
    /// <returns></returns>
    private string DataSetuse()
    {
        string value = "";
        for (int i = 0; i < useList.Count; i++)
        {
            value += (value == "" ? "" : "&") + useList[i].Item1+",";
            for (int j = 0; j < useList[i].Item2.Count; j++)
            {
                value += (j == 0 ? "" : "|") + useList[i].Item2[j];
            }
        }
        return value;
    }
    /// <summary>
    /// д���䷽
    /// </summary>
    /// <returns></returns>
    private string DataSetformula()
    {
        string value = "";
        for (int i = 0; i < formulalist.Count; i++)
        {
            value += (value == "" ? "" : "&") + formulalist[i].Item1;
            for (int j = 0; j < formulalist[i].Item2.Count; j++)
            {
                value += "," + formulalist[i].Item2[j];
            }
        }
        return value;
    }

    public override string[] Get_Update_Character()
    {
        return new string[] { 
            "user_value" ,
            "formula_value",
            "user_use_value"

        };
    }

    public override string[] Set_Uptade_String()
    {
        return new string[] { 
            GetStr(DataSet()),
            GetStr(DataSetformula()),
            GetStr(DataSetuse())
        };
    }

    public override string[] Set_Instace_String()
    {
        return 
            new string[]
            {
                GetStr(0),
                GetStr(SumSave.crt_user.uid),
                GetStr(user_value),
                GetStr(formula_value),
                GetStr(use_value)
            };
    }
}
