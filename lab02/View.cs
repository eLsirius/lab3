using Newtonsoft.Json;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Linq;
using System.ComponentModel.DataAnnotations;

public class DayOperation
{
    [Key]
    public int Id { get; set; }
    public string? Operation { get; set; }
    public Int32 Year { get; set; }
    public Int32 Month { get; set; }
    public Int32 Day { get; set; }
    public DayOfWeek Result { get; set; }
}

public class IntervalOperation
{
    [Key]
    public int Id { get; set; }
    public string? Operation { get; set; }
    public Int32 Year1 { get; set; }
    public Int32 Year2 { get; set; }
}

public class LeapOperation
{
    [Key]
    public int Id { get; set; }
    public string? Operation { get; set; }
    public Int32 Year { get; set; }
}

public class View
{

    private bool programmRunning;
    private readonly ICalendar calendarInterface = new Calendar();
    public static string saveFormat;

    public void Greet()
    {
        Console.WriteLine("Usage: write the following command (without quotations) to do something: ");
    }

    public void RepeatGreet()
    {
        Console.WriteLine("check to check if year is a leap one");
        Console.WriteLine("calc to calculate interval length");
        Console.WriteLine("day to get the name of day of the week");
        Console.WriteLine("quit to exit");
        Console.WriteLine("save for saving options");
        WriteDashLine();

        string userInput = Console.ReadLine();
        ProcessInput(userInput);
    }
    public void WriteDashLine()
    {
        Console.WriteLine("-------");
    }
    public void Start()
    {
        Greet();
        programmRunning = true;
        while (programmRunning)
            RepeatGreet();
    }

    public void ProcessInput(string input)
    {
        var validInputs = new Dictionary<string, int>()
        {
            { "check", 0},
            { "calc", 1},
            { "day", 2},
            { "quit", 3},
            { "save", 4},
            { "xdd", 5}
        };
        if (!validInputs.ContainsKey(input))
        {
            WriteWrongInputMessage();
            return;
        }
        switch (validInputs[input])
        {
            case 0:
                CheckYearLeap();
                break;
            case 1:
                CalculateIntervalLength();
                break;
            case 2:
                GetNameOfDayOfTheWeek();
                break;
            case 3:
                Quit();
                break;
            case 4:
                SavingOptions();
                break;
            case 5:
                xdding();
                break;
            default:
                WriteNoInputMessage();
                break;
        }
    }

    public void ProcessInput2(string input)
    {
        var validInputs = new Dictionary<string, int>()
        {
            { "json", 0},
            { "xml", 1},
            { "sql", 2},
        };
        if (!validInputs.ContainsKey(input))
        {
            WriteWrongInputMessage();
            return;
        }
        switch (validInputs[input])
        {
            case 0:
                saveFormat = "json";
                break;
            case 1:
                saveFormat = "xml";
                break;
            case 2:
                saveFormat = "sql";
                break;
            default:
                WriteNoInputMessage();
                break;
        }
    }

    void SavingOptions()
    {
        Console.WriteLine("json for saving in json");
        Console.WriteLine("xml for saving in xml");
        Console.WriteLine("sql for saving in sqlite");
        WriteDashLine();

        string userInput = Console.ReadLine();
        ProcessInput2(userInput);
    }

    void WriteNoInputMessage()
    {
        Console.WriteLine("No input.");
    }

    void WriteWrongInputMessage()
    {
        Console.WriteLine("Wrong input.");
        WriteDashLine();
    }
    private bool CheckYearLeap()
    {
        Console.WriteLine("Input year: ");
        bool validInput = Int32.TryParse(Console.ReadLine(), out Int32 year);
        if (!validInput)
        {
            WriteWrongInputMessage();
            return false;
        }
        if (calendarInterface.LeapCheck(year))
        {
            Console.WriteLine("The year is leap!");
            WriteDashLine();
            return true;
        }
        Console.WriteLine("The year is not leap.");
        WriteDashLine();

        var LeapOperation = new LeapOperation
        {
            Operation = "Leap",
            Year = year,
        };

        switch (saveFormat)
        {
            case "json":
                var filePath = @"C:\Users\imaxi\source\repos\lab02\lab02\bin\Debug\net6.0\CalendarOperations.json";
                var jsonData = System.IO.File.ReadAllText(filePath);
                var operationsList = JsonConvert.DeserializeObject<List<LeapOperation>>(jsonData)
                              ?? new List<LeapOperation>();
                operationsList.Add(LeapOperation);
                jsonData = JsonConvert.SerializeObject(operationsList);
                System.IO.File.WriteAllText(filePath, jsonData);
                break;
            case "xml":

                var filePathh = "C:\\Users\\imaxi\\source\\repos\\lab02\\lab02\\bin\\Debug\\net6.0\\CalendarOperations.xml";
                var xmlDoc = System.Xml.Linq.XDocument.Load(filePathh);
                var parentElement = new System.Xml.Linq.XElement("CheckOperation");
                var yearElement = new System.Xml.Linq.XElement("Year", year);

                parentElement.Add(yearElement);


                var rootElement = xmlDoc.Element("Session");
                rootElement?.Add(parentElement);

                xmlDoc.Save(filePathh);
                break;
            case "sql":
                var db = new BloggingContext();
                Random random = new Random();
                Console.WriteLine($"Database path: {db.DbPath}.");
                db.Add(new LeapOperation
                {
                    Id = random.Next(),
                    Operation = "CheckOperation",
                    Year = year,
                });
                db.SaveChanges();
                break;
        }

        return false;
    }
    private int CalculateIntervalLength()
    {
        Console.WriteLine("Input first year: ");
        bool validInput1 = Int32.TryParse(Console.ReadLine(), out Int32 year1);
        Console.WriteLine("Input second year: ");
        bool validInput2 = Int32.TryParse(Console.ReadLine(), out Int32 year2);
        if (validInput1 && validInput2)
        {
            Console.WriteLine($"The gap is: {calendarInterface.GetYearIntervalLength(year1, year2)} years");
            WriteDashLine();

            Random random2 = new Random();
            var IntervalOperation = new IntervalOperation
            {
                Id = random2.Next(),
                Operation = "Calc",
                Year1 = year1,
                Year2 = year2,
            };

            switch (saveFormat)
            {
                case "json":
                    var filePath = @"C:\Users\imaxi\source\repos\lab02\lab02\bin\Debug\net6.0\CalendarOperations.json";
                    var jsonData = System.IO.File.ReadAllText(filePath);
                    var operationsList = JsonConvert.DeserializeObject<List<IntervalOperation>>(jsonData)
                                  ?? new List<IntervalOperation>();
                    operationsList.Add(IntervalOperation);
                    jsonData = JsonConvert.SerializeObject(operationsList);
                    System.IO.File.WriteAllText(filePath, jsonData);
                    break;
                case "xml":

                    var filePathh = "C:\\Users\\imaxi\\source\\repos\\lab02\\lab02\\bin\\Debug\\net6.0\\CalendarOperations.xml";
                    var xmlDoc = System.Xml.Linq.XDocument.Load(filePathh);
                    var parentElement = new System.Xml.Linq.XElement("CalcOperation");
                    var year1Element = new System.Xml.Linq.XElement("Year1", year1);
                    var year2Element = new System.Xml.Linq.XElement("Year2", year2);

                    parentElement.Add(year1Element);
                    parentElement.Add(year2Element);


                    var rootElement = xmlDoc.Element("Session");
                    rootElement?.Add(parentElement);

                    xmlDoc.Save(filePathh);
                    break;
                case "sql":
                    var db = new BloggingContext();
                    Random random = new Random();
                    Console.WriteLine($"Database path: {db.DbPath}.");
                    db.Add(new IntervalOperation
                    {
                        Id = random.Next(),
                        Operation = "CalcOperation",
                        Year1 = year1,
                        Year2 = year2,
                    });
                    db.SaveChanges();
                    break;
            }

            return calendarInterface.GetYearIntervalLength(year1, year2);
        }
        WriteWrongInputMessage();



        return 0;
    }

    private DayOfWeek GetNameOfDayOfTheWeek()
    {

        Console.WriteLine("Input year: ");
        bool validInput1 = Int32.TryParse(Console.ReadLine(), out Int32 year);
        if (!validInput1)
        {
            WriteWrongInputMessage();
            throw new InvalidOperationException();
        }
        Console.WriteLine("Input month: ");
        bool validInput2 = Int32.TryParse(Console.ReadLine(), out Int32 month);
        if (!validInput2)
        {
            WriteWrongInputMessage();
            throw new InvalidOperationException();
        }
        Console.WriteLine("Input day: ");
        bool validInput3 = Int32.TryParse(Console.ReadLine(), out Int32 day);
        if (!validInput3)
        {
            WriteWrongInputMessage();
            throw new InvalidOperationException();
        }

        WriteDashLine();
        Console.WriteLine("The day of the week is: " + calendarInterface.GetNameOfDayOfTheWeek(year, month, day) + ".");
        WriteDashLine();

        Random random1 = new Random();
        var DayOperation = new DayOperation
        {
            Id = random1.Next(),
            Operation = "Day",
            Year = year,
            Month = month,
            Day = day,
            Result = calendarInterface.GetNameOfDayOfTheWeek(year, month, day)
        };

        switch (saveFormat)
        {
            case "json":
                var filePath = @"C:\Users\imaxi\source\repos\lab02\lab02\bin\Debug\net6.0\CalendarOperations.json";
                var jsonData = System.IO.File.ReadAllText(filePath);
                var operationsList = JsonConvert.DeserializeObject<List<DayOperation>>(jsonData)
                              ?? new List<DayOperation>();
                operationsList.Add(DayOperation);
                jsonData = JsonConvert.SerializeObject(operationsList);
                System.IO.File.WriteAllText(filePath, jsonData);
                break;
            case "xml":

                var filePathh = "C:\\Users\\imaxi\\source\\repos\\lab02\\lab02\\bin\\Debug\\net6.0\\CalendarOperations.xml";
                var xmlDoc = System.Xml.Linq.XDocument.Load(filePathh);
                var parentElement = new System.Xml.Linq.XElement("DayOperation");
                var yearElement = new System.Xml.Linq.XElement("Year", year);
                var monthElement = new System.Xml.Linq.XElement("Month", month);
                var dayElement = new System.Xml.Linq.XElement("Day", day);
                var resultElement = new System.Xml.Linq.XElement("Result", calendarInterface.GetNameOfDayOfTheWeek(year, month, day));

                parentElement.Add(yearElement);
                parentElement.Add(monthElement);
                parentElement.Add(dayElement);
                parentElement.Add(resultElement);

                var rootElement = xmlDoc.Element("Session");
                rootElement?.Add(parentElement);

                xmlDoc.Save(filePathh);
                break;
            case "sql":
                var db = new BloggingContext();
                Random random = new Random();
                Console.WriteLine($"Database path: {db.DbPath}.");
                db.Add(new DayOperation {
                    Id = random.Next(),
                    Operation = "Day",
                    Year = year,
                    Month = month,
                    Day = day,
                    Result = calendarInterface.GetNameOfDayOfTheWeek(year, month, day)
                });
                db.SaveChanges();
                break;
        }
        return calendarInterface.GetNameOfDayOfTheWeek(year, month, day);
    }

    private void xdding()
    {
        for (int i = 0; i < 60000; i++)
        {
            Console.Write("xdd");
        }
    }

    private void Quit()
    {
        programmRunning = false;
    }
}