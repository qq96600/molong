using MVC;
using System.Collections.Generic;

public class db_lv_vo : Base_VO
{
    /// <summary>
    /// ��ҵȼ�������
    /// </summary>
    public List<long> hero_lv_list;
    /// <summary>
    /// С������������
    /// </summary>
    public List<(string, int)> world_lv_list;
    /// <summary>
    /// С��������Ч��
    /// </summary>
    public List<int> world_offect_list ;
    /// <summary>
    /// С�������洢ֵ
    /// </summary>
    public List<int> word_lv_max_value;
}
