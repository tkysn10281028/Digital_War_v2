namespace DigitalWar.Project.Common.Enums
{
    public enum TileTypes
    {
        // Wall系：100XXXX
        // プレイヤーが上から入る場合の障害物 + その他背景に使用
        Wall01 = 1000001,
        Wall02 = 1000002,
        Wall03 = 1000003,
        Wall04 = 1000004,

        // Floor系：101XXXX
        // 障害物ではない. プレイヤーが移動できるタイルは全てこれ.
        Floor01 = 1010001,
        Floor02 = 1010002,
        Floor03 = 1010003,
        Floor04 = 1010004,
        Floor05 = 1010005,

        // Stair系：102XXXX
        // 一部障害物. 特定の方向は通れない.
        Stair01 = 1020001,
        Stair02 = 1020002,
        Stair03 = 1020003,
        Stair04 = 1020004,
        Stair05 = 1020005,
        Stair06 = 1020006,

        // Lava系：103XXXX
        // 障害物. 基本通れない.
        Lava01 = 1030001,
        Lava02 = 1030002,
        Lava03 = 1030003,
        Lava04 = 1030004,
        Lava05 = 1030005,

        // ItemBox系：104XXXX
        // 障害物で、当たったらイベント（選択肢）
        ItemBox01 = 1040001,

        // Door系：105XXXX
        // 上から入る 踏んだらイベント
        Door01 = 1050001,
        Door02 = 1050002,
    }
}