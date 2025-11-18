namespace test;

[TestClass]
[TestCategory("Console")]
public class ConsoleTest
{
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
        Locale.SetLocale();

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
            // If delta different from 0 and output is a double
            if (config.Delta != 0.0 && double.TryParse(output, out double numberToCheck))
            {
                System.Diagnostics.Debug.WriteLine(output);
                System.Diagnostics.Debug.WriteLine(numberToCheck);

                double lowerBound = numberToCheck - config.Delta;
                double upperBound = numberToCheck + config.Delta;

                // Get a list of all numbers between the lower and upper bounds, up to 2 decimals
                List<string> numbers = new();
                for (double i = lowerBound; i <= upperBound; i += 0.01)
                {
                    // Add the number with 0, 1 and 2 decimals so all possible outputs are covered
                    numbers.Add(i.ToString("n0"));
                    numbers.Add(i.ToString("n1"));
                    numbers.Add(i.ToString("n2"));
                }

                bool containsAny = numbers.Any(n => fullOutput.Contains(n));
                Assert.IsTrue(containsAny, $"~Een getal tussen {lowerBound} en {upperBound} kon niet worden gevonden in jouw output.~{fullOutput}");
                continue;
            }

            // Assert
            StringAssert.Contains(fullOutput, output, $"~De verwachte output kon niet worden gevonden in jouw output.~{fullOutput}~{output}", StringComparison.InvariantCulture);
        }
    }
}