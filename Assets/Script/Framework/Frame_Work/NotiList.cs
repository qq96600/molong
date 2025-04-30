
public class NotiList
{
# region 主控制器

    public const string Read_Instace = "获取初始化数据";
    public const string User_Login = "登录";
    public const string Execute_Write = "执行写入指令";
    public const string Delete = "删除";
    public const string loglist = "日志";
    public const string Refresh_User_Setting = "刷新设置";
    public const string Refresh_Max_Hero_Attribute = "刷新属性";

    public const string Read_World = "读取小世界";
    public const string Read_User_Ranks = "读取排行榜";
    public const string Read_Crate_Uid = "进入游戏";
    public const string Read_Obtain_Par = "读取服务器列表";
    public const string Read_Message_Window = "读取飘窗消息";
    public const string Read_Huser_MessageWindow = "写入飘窗消息";

    #endregion

    /*
    #region 主场景数据 
    public const string Success = "成功";

    public const string Delete = "删除";

    public const string Show_Crate_HeroStage = "显示角色创建面板";

    public const string Show_Main = "显示主面板";

    public const string User_Delete = "删除全部数据";

    public const string Obtain_Moeny = "获取铜币";

    public const string Use_Moeny = "消耗铜币";

    public const string Refresh_User_Physical = "消耗体力";

    public const string Refresh_User_Setting = "刷新设置";

    public const string Read_Mysql_Base_Time = "读取网络时间";

    public const string Refresh_Data_CrtSign = "刷新签到数据";

    public const string Refresh_User_Trade = "刷新跑商数据";

    public const string Refresh_User_Task = "刷新任务数据";

    public const string Obtain_Purchasing = "充值信息";

    public const string Read_Mysql_Store = "读取商店信息";

    public const string BargainDiamonds = "消费元宝";

    public const string Refresh_Store = "刷新交易商店物品";

    public const string Crate_Store = "创建商品";

    public const string Read_MySql_WorldBoss = "读取世界boss";

    public const string Refresh_MySql_WorldBoss = "刷新世界boss";

    public const string Mysql_Crate_Skill = "创建技能";

    public const string Refresh_Crate_Skill = "刷新创建技能";

    public const string Refresh_User_Achieves = "刷新成就";

    public const string Refresh_Demons_Assets = "刷新炼妖";

    public const string Data_Log = "记录信息";

    public const string ReductionWing = "还原坐骑材料";

    #endregion

    #region 场景信息

    public const string PLAY_LEVEL = "加载场景";

    public const string LEVEL_LOADED = "加载场景完毕";

    public const string UNLOAD_LEVEL = "卸载场景";

    public const string Read_Locality = "本地数据";

    public const string Refresh_PanelBattle = "刷新战斗属性";

    public const string Refresh_Info_Base = "基础属性";

    #endregion

    public const string Read_Data = "读取自身数据";

    public const string Read_House = "读取仓库";

    public const string EquipUseHouse = "取出物品";

    public const string EquipWareHouse = "存入物品";

    public const string Write_Hero = "写入角色数据";

    public const string Refresh_Hero = "刷新角色数据";

    public const string Refresh_Hero_Transfer = "刷新转修数据";

    public const string Refresh_WearEquip = "刷新穿戴装备数据";

    public const string Delete_Wear_Equip = "删除穿戴装备数据";

    public const string Refresh_BagEquip = "刷新背包装备数据";

    public const string Write_One_Equip = "写入单个装备";

    public const string Batch_Equip = "批量写入数据";

    public const string data_Equip_Advances = "写入套装属性";

    public const string Delete_Equip = "删除数据";

    public const string Batch_Resources = "批量写入材料数据";

    public const string Refresh_Hero_Sum_Attribute = "刷新总属性";

    public const string Bag_Equip_Sale = "装备出售";

    public const string Bag_Equip_Sales = "批量出售装备";

    public const string Write_Hero_Instace = "获取初始化装备";

    public const string Hero_Equip_Take = "脱下装备";

    public const string Bag_Equip_Wear = "穿上装备";

    public const string Write_Skill = "写入角色技能";

    public const string Refresh_Skill = "刷新技能数据";

    public const string Delete_Skill = "删除技能数据";

    public const string Write_Equip = "写入角色装备";

    public const string Write_DemonSpirit = "写入妖灵";

    public const string Refresh_DemonSpirit = "刷新妖灵";

    public const string MySql_Exchange = "礼包";

    public const string Refresh_Space_Instace = "刷新初始化妖灵";

    public const string Refresh_Space = "刷新妖灵空间属性";

    public const string Crate_Space = "写入创新妖灵空间";

    public const string Read_Space = "读取初始化设定";

    public const string Refresh_Books = "刷新图鉴";

    public const string Read_Gm_Space = "读取空间数据";

    public const string Refresh_Gm_Assets = "刷新祝福空间数据";

    public const string Refresh_Shop = "刷新珍宝阁";

    public const string Refresh_Gold = "刷新金币福利";

    public const string Refresh_Offline = "刷新离线信息";

    public const string Crate_title = "创建称号";

    public const string Refresh_Title = "刷新称号";

    public const string Read_Base_Carte = "创造技能标准值";

    public const string Open_File = "开启存档";

    public const string Download_Archive = "下载存档";

    public const string Read_Activitys = "读取活动列表";

    public const string Refresh_Activitys = "刷新领取";


    # region 宠物

    public const string Read_Pet = "读取宠物初始设定";

    public const string Refresh_Pet = "刷新宠物数据";

    public const string crate_pet = "创造宠物";

    public const string Read_DemonSpirits = "读取抓宠数据";

    public const string Refresh_Demon_Pet = "刷新抓虫数据";

    public const string Delete_Pet = "删除宠物";

    public const string Read_Bullying = "读取豪横掉落";

    public const string Refresh_Bullying = "刷新豪横掉落";

    #endregion

    #region 雇佣兵


    public const string Read_Mercenary = "读取雇佣兵";

    public const string Crate_Mercenary = "创建雇佣兵";

    public const string Refresh_Mercenary = "刷新雇佣兵";

    public const string Dismiss_mercenary = "解雇";

    public const string Read_Mercenary_lock_State = "检查状态";

    public const string Read_Battlefields = "读取角色";

    #endregion


    #region 宝石


    public const string Read_Gemstone = "读取宝石设定";

    public const string Read_Box = "读取宝箱设定";

    public const string Crate_Box = "创建宝箱";

    public const string Refresh_Box = "刷新宝箱";

    public const string Box_Books = "宝箱图鉴奖励";

    public const string Batch_Gemstone = "批量获得宝石";

    public const string Refresh_Gemstone_Console = "刷新控制台";

    public const string Wear_Gemstone = "穿戴宝石";

    #endregion

    #region

    public const string Read_military = "读取军功";

    public const string Refresh_military = "刷新军功";

    public const string Refresh_base_military = "获取军功";

    public const string Read_Military_Store = "读取军功商店";

    #endregion

    #region 神器

    public const string Refresh_User_Artifact = "刷新神器主列表";

    public const string Crate_Data_Artifact = "创建角色神器";

    public const string Refresh_Data_Artifact = "刷新角色神器";

    #endregion
    */
}
