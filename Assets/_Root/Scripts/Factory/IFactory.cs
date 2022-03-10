namespace Game.Factory
{
    internal interface IFactory<out T>
    {
        T Create();
    }
}