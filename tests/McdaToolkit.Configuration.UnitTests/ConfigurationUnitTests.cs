using FluentAssertions;

namespace McdaToolkit.Configuration.UnitTests;

public class ConfigurationUnitTests
{
    [Fact]
    public void GetGenericOption_ThatNotExistsInConfiguration_ShouldThrowInvalidOperationException()
    {
        var configuration = new ToolkitConfiguration();

        Action act = () =>
        {
            configuration.GetOption<string>(string.Empty);
        };

        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void GetGenericOption_WhenKeyIsCorrectButValueHaveIncorrectType_ShouldThrowInvalidOperationException()
    {
        var key = "test";
        var incorrectValue = 15;
        var newConfigOption = new ConfigOption<int>(key,incorrectValue);
        var configuration = new ToolkitConfiguration();
        configuration.AddOption(newConfigOption);

        Action act = () => configuration.GetOption<string>(key+"12321");
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void GetGenericOptionOrDefault_ThatNotExistsInConfiguration_ShouldReturnNull()
    {
        var configuration = new ToolkitConfiguration();
        var obj = configuration.GetOptionOrDefault<string>(string.Empty);
        obj.Should().BeNull();
    }
    
    [Fact]
    public void GetGenericOptionOrDefault_WhenKeyIsCorrectButValueHaveIncorrectType_ShouldReturnDefault()
    {
        var key = "test";
        var incorrectValue = 15;
        var newConfigOption = new ConfigOption<int>(key,incorrectValue);
        var configuration = new ToolkitConfiguration();
        configuration.AddOption(newConfigOption);

        var option = configuration.GetOptionOrDefault<string>(key);
        option.Should().BeNull();
    }
    
    [Fact]
    public void AddOption_ThatExistsInConfiguration_ShouldReturnResultFail()
    {
        var key = "test";
        var value = "testable";
        var newConfigOption = new ConfigOption<string>(key,value);
        var configuration = new ToolkitConfiguration();
        configuration.AddOption(newConfigOption);
        
        var result = configuration.AddOption(newConfigOption);

        result.IsFailed.Should().BeTrue();
    }
    
    [Fact]
    public void AddRangeOfOptions_WhenSomeOfOptionsExistsInConfiguration_ShouldAddOnlyTheNewConfigOption()
    {
        var newConfigOption1 = new ConfigOption<string>("test","testable");
        var newConfigOption2 = new ConfigOption<string>("test1","testable1");
        var newConfigOption3 = new ConfigOption<string>("test","testable");
        var configuration = new ToolkitConfiguration();
        configuration.AddOption(newConfigOption1);
        
        configuration.AddRange(new[] { newConfigOption2, newConfigOption3 });

        configuration.GetOptions().Should().HaveCount(2);
    }
}