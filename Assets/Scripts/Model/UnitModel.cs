/// <summary> THIS IS THE FIRST CLASS IMPRESSION ABOUT THE ZEUS UNITE MONSTER DATABASE ASSET 
/// COMING TO UNITY ASSET STORE WINTER 2022
/// 
/// The Monsters Database will be registered to the Unity Project
/// Once Registered it will unlock additional Features from other Zeus Unite Assets
/// that are working with this Extension.
/// 
/// The DamageType gets added to the Zeus Unite - Item Database Management System
/// If you have the Item Database Management it will also unlock additinal Features
/// such as LootTable on the Monsters.
/// 
/// It enables to add Loot to Enemies inside the Zeus Unite Editor.
/// You also can create your own LootTable and Item System and use it instead.
/// It's also possible to combine this with other Unity Assets from other Studios.
/// More Information:
/// https://zeusunite.stussegames.com ** https://zeusunite.idm.stussegames.com
/// </summary>

namespace ZeusUnite.Monsters
{
    [System.Serializable]
    public class UnitModel
    {
        [UnityEngine.Header("Unit Database Settings")]

        public int UnitId;
        public string UnitName;
        public string UnitDescription;

        public int Experience;
        public NPCType NPCType;

        [UnityEngine.Header("Unit Game Settings")]
        public HealthModel HealthSystem;
        public UnityEngine.Transform BodyExplosionPrefab;
        public UnityEngine.Transform ExplosionPrefab;

#if ZU_Inventory //Requires Zeus Unite - Item Database Management
        public Attributes[] UnitAttributes;

        [UnityEngine.Header("Unit Loot Settings")]
        public LootType LootType;
        public LootTables[] Loot;

#else //Without ZU - IDM the System will use own Properties to work with.
        public int GrantMoney;
        public float UnitSpeed;

        public float UnitDamage;
        public DamageType DamageType;
#endif

        public UnityEngine.GameObject WorldObject;

        public AudioShot dieSound;
    }
}

    public enum NPCType
    {
        Monster,
        Boss,
        Shop,
        Quest,
        ShopAndQuest,
    }

    public enum DamageType
    {
        Physical,
        Fire,
        Frost,
        Water,
        Acid,
        Dark,
        Light,
    }

