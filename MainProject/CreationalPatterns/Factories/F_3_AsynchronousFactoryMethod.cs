using System.Threading.Tasks;

// This solve async object init
namespace MainProject.CreationalPatterns.Factories
{
    internal class Foo
    {
        private Foo()
        {
            
        }

        private async Task<Foo> InitAsync()
        {
            await Task.Delay(1000); // do any async stuff
            return this;
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }
    public static class F3AsynchronousFactoryMethod
    {
        public static async void Run()
        {
            var foo = await Foo.CreateAsync();
        }
    }
}