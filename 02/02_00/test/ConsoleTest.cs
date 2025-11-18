namespace test;

[TestClass]
[TestCategory("Console")]
public class ConsoleTest
{
    // Last updated: 2023-08-09 15:11

    private static readonly string _filename = "ioConfig.json";

    public static IEnumerable<Object[]> IoConfigs
    {
        get
        {
            var json = File.ReadAllText(_filename);
            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<IoConfig>(json);
            foreach (var item in config.IoConfigIoConfig)
            {
                yield return new Object[] { item };
            }
        }
    }

    [TestMethod]
    [DynamicData(nameof(IoConfigs))]
    public void InputOutputTest(IoConfigElement config)
    {
        if (config is null)
        {
            throw new ArgumentNullException(nameof(config), "The 'IoConfigs' parameter can't be null.");
        }

        // Arrange
        var writer = new StringWriter();
        Console.SetOut(writer);

        var inputString = string.Join("\n", config.Inputs);
        var textReader = new StringReader(inputString);
        Console.SetIn(textReader);

        // Act
        var args = new string[0];
        var entrypoint = typeof(Program).Assembly.EntryPoint!;
        entrypoint.Invoke(null, new object[] { args });
        var sb = writer.GetStringBuilder();
        var fullOutput = sb.ToString();
        var outputAsLines = fullOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        // Write to debug what the input for the test is
        System.Diagnostics.Debug.WriteLine(inputString);

        foreach (var output in config.Outputs)
        {
            if (config.IgnoreCulture)
            {
                var changedOutput = output.Replace(".", ",");
                StringAssert.Contains(fullOutput, output, $"~De verwachte output kon niet worden gevonden in jouw output.~{fullOutput}~{changedOutput}", StringComparison.InvariantCulture);
                continue;
            }

            // Assert
            StringAssert.Contains(fullOutput, output, $"~De verwachte output kon niet worden gevonden in jouw output.~{fullOutput}~{output}", StringComparison.InvariantCulture);
        }
    }
}