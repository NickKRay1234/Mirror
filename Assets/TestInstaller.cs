using UnityEngine;
using Zenject;
using ZenjectProject1;
using ZenjectProject2;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello World");
        Container.Bind<Greeter>().AsSingle().NonLazy();
        Container.Bind<ISoundManager>().To<AdvancedSoundManager>().AsSingle();
        //Container.Bind<IInputHandler>().To<MouseInputHandler>().AsTransient();
        Container.Bind<IInputHandler>().To<KeyboardInputHandler>().AsTransient();
    }

    public class Greeter
    {
        public Greeter(string message) => Debug.Log(message);
    }
}