using Spectre.Console;
using System.Globalization;
using System;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

//Method for logging
void Log(string message)
{
    var logPath = Environment.GetEnvironmentVariable("CALCULATOR_LOG_PATH", EnvironmentVariableTarget.Machine);
    string logTimestampFormat = "yyyy-MM-dd HH:mm:ss";
    string timestamp = DateTime.Now.ToString(logTimestampFormat);
    string logMessage = $"{timestamp} - {message}";

    try
    {
        File.AppendAllText(logPath, logMessage + Environment.NewLine);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to write to log file: {ex.Message}");
        Environment.Exit(1);
    }
}

//Method for getting user input
int GetUserInput(String message) //Her er jeg kommet til at lave et muligt endless loop, kan måske fikses med en timeout timer eller et FOR loop med et fastsat antal forsøg
{try
{
    Console.WriteLine(message);
    Log($"User input requested: {message}");
    return Convert.ToInt32(Console.ReadLine());
}
catch (System.Exception)
{
    Log("User input was not a number");
    return GetUserInput("Not a number, try again");
}
}

//Method for getting operator
string GetOperator()
{
    Log("Operator selection requested");
    var operatorPrompt = new SelectionPrompt<string>()
        .Title("Choose operator")
        .AddChoices("+", "-", "*", "/", "average", "exit");
    string selectedOperator = AnsiConsole.Prompt(operatorPrompt);
    Log($"Operator selected: {selectedOperator}");
    return selectedOperator;
}

//Method for addition numbers
int Add(int num1, int num2)
{
    Log($"Adding {num1} and {num2}");
    return num1 + num2;
}

//Method for subtracting numbers
int Subtract(int num1, int num2)
{
    Log($"Subtracting {num2} from {num1}");
    return num1 - num2;
}

//Method for multiplying numbers
int Multiply(int num1, int num2)
{
    Log($"Multiplying {num1} by {num2}");
    return num1 * num2;
}

//Method for dividing numbers
int Divide(int num1, int num2)
{
    Log($"Dividing {num1} by {num2}");
    return num1 / num2;
}

//Method for calculating average
double Average(int[] numbers)
{
    Log($"Calculating average of numbers: {string.Join(", ", numbers)}");
    double sum = 0;
    foreach (int number in numbers)
    {
        sum += number;
    }
    return sum / numbers.Length;
}

//Method for calculating numbers
int Calculate(int num1, int num2, string ope)
{
    Log($"Switching on operator: {ope}");
    switch (ope)
    {
        case "+":
            return Add(num1, num2);
        case "-":
            return Subtract(num1, num2);
        case "*":
            return Multiply(num1, num2);
        case "/":
            return Divide(num1, num2);
        case "average"://Not used
            return 0;   
        default:
            return 0;
    }
}

//Initial Variables
int num1;
int num2;
int num3;
string ope;
double res;
Log("initialised variables: num1, num2, num3, ope, res");
//Starting calculator
while (true)
{
    Log("Starting calculator");
    ope = GetOperator();

    if (ope == "exit")
    {
        Log("Exiting calculator");
        Log("--------------------");
        Console.WriteLine("Exiting calculator...");
        break;
    }

    num1 = GetUserInput("Enter first number:");
    num2 = GetUserInput("Enter second number:");

    if (ope == "average")
    {
        num3 = GetUserInput("Enter third number:");
        Log($"First number entered is {num1}, second number entered is {num2}, third number entered is {num3}, and operator selected is {ope}");
        res = Average(new int[] { num1, num2, num3 });
    }
    else
    {
        Log($"First number entered is {num1}, second number entered is {num2}, and operator selected is {ope}");
        res = Calculate(num1, num2, ope);
    }

    Log($"Calculation result: {res}");
Console.WriteLine("Calculating...");
Thread.Sleep(2000);
Console.WriteLine($"The result is: {res}");
}
