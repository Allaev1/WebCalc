using WebCalc.Blazor.ViewModels.Components.Calc;
using WebCalc.Blazor.ViewModels.Components.CalcDisplay;

namespace WebCalc.IntegrationTests.Tests
{
    public class CalcComponentTests
    {
        private readonly ILocalStorageService localStorageService;

        public CalcComponentTests()
        {
            localStorageService = Substitute.For<ILocalStorageService>();
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetTestDataForValue), MemberType = typeof(TestDataGenerator))]
        public void TestValue(char[] values, string expected)
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IBackNavigateable, NavigationHistoryStorage>();
            context.Services.AddTransient<IBinaryOperationAppService, BinaryOperationAppService>();
            context.Services.AddSingleton(localStorageService);
            context.Services.AddSingleton<ISettings, Settings>();
            context.Services.AddSingleton<IFormater, Formater>();
            context.Services.AddSingleton<IInputValidationService, InputValidationService>();
            context.Services.AddSingleton<ICalcDisplayViewModel, CalcDisplayViewModel>();
            context.Services.AddSingleton<ICalcViewModel, CalcViewModel>();

            localStorageService.GetItemAsync<bool>(Arg.Is("delimeterOn")).Returns(false);

            var calcComponent = context.RenderComponent<Calc>();
            var buttons = calcComponent.FindAll("button");
            ICalc calc = calcComponent.Instance;

            foreach (var value in values)
            {
                buttons.Single(x => x.Id == value.ToString()).Click(new());
            }

            calc.GetDisplayValue().Should().Be(expected);
            calc.ViewModel.Should().NotBeNull();
            calc.ViewModel!.ClearOperations();
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetTestDataForExpression), MemberType = typeof(TestDataGenerator))]
        public void TestExpression(char[] values, string expected)
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IBackNavigateable, NavigationHistoryStorage>();
            context.Services.AddSingleton<IBinaryOperationAppService, BinaryOperationAppService>();
            context.Services.AddSingleton(localStorageService);
            context.Services.AddSingleton<ISettings, Settings>();
            context.Services.AddSingleton<IInputValidationService, InputValidationService>();
            context.Services.AddSingleton<IFormater, Formater>();
            context.Services.AddSingleton<ICalcDisplayViewModel, CalcDisplayViewModel>();
            context.Services.AddSingleton<ICalcViewModel, CalcViewModel>();

            localStorageService.GetItemAsync<bool>(Arg.Is("delimeterOn")).Returns(false);

            var calcComponent = context.RenderComponent<Calc>();
            ICalc calc = calcComponent.Instance;
            var buttons = calcComponent.FindAll("button");

            foreach (var value in values)
            {
                buttons.Single(x => x.Id == value.ToString()).Click(new());
            }

            calc.GetDisplayExpression().Should().Be(expected);
            calc.ViewModel.Should().NotBeNull();
            calc.ViewModel!.ClearOperations();
        }

        
        [Theory]
        [InlineData(new char[] { Constants.MEMORY_ADD }, "0")]
        [InlineData(new char[] { '1', Constants.MEMORY_ADD }, "1")]
        [InlineData(new char[] { '1', Constants.MEMORY_ADD, '1', Constants.MEMORY_ADD }, "12")]
        public void TestMemory(char[] values, string expected)
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IBackNavigateable, NavigationHistoryStorage>();
            context.Services.AddSingleton<IBinaryOperationAppService, BinaryOperationAppService>();
            context.Services.AddSingleton(localStorageService);
            context.Services.AddSingleton<ISettings, Settings>();
            context.Services.AddSingleton<IFormater, Formater>();
            context.Services.AddSingleton<IInputValidationService, InputValidationService>();
            context.Services.AddSingleton<ICalcDisplayViewModel, CalcDisplayViewModel>();
            context.Services.AddSingleton<ICalcViewModel, CalcViewModel>();

            localStorageService.GetItemAsync<bool>(Arg.Is("delimeterOn")).Returns(false);

            var calcComponent = context.RenderComponent<Calc>();
            var calc = calcComponent.Instance;
            var buttons = calcComponent.FindAll("button");

            foreach (var value in values)
            {
                buttons.Single(x => x.Id == value.ToString()).Click(new());
            }

            calc.GetDisplayMemory().Should().Be(expected);
            calc.ViewModel.Should().NotBeNull();
            calc.ViewModel!.ClearOperations();
        }
    }
}
