namespace Tool.Localization
{
    internal interface ILocalizable
    {
        string ID { get; set; }
        CustomText CustomText { get; set; }
    }
}