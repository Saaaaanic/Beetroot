using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        #region JSON

        //{"firstName":"John", "lastName":"Doe"}
        
        // "employees":[
        // {"firstName":"John", "lastName":"Doe"},
        // {"firstName":"Anna", "lastName":"Smith"},
        // {"firstName":"Peter", "lastName":"Jones"}
        // ]

        #endregion

        #region XML

        // <?xml version="1.0" encoding="UTF-8"?>
        //     <note>
        //     <to>Tove</to>
        //     <from>Jani</from>
        //     <heading>Reminder</heading>
        //     <body>Don't forget me this weekend!</body>
        //     </note>

        // <?xml version="1.0" encoding="UTF-8"?>
        //     <breakfast_menu>
        //     <food>
        //     <name>Belgian Waffles</name>
        //     <price>$5.95</price>
        //     <description>
        //     Two of our famous Belgian Waffles with plenty of real maple syrup
        //     </description>
        //     <calories>650</calories>
        //     </food>
        //     <food>
        //     <name>Strawberry Belgian Waffles</name>
        //     <price>$7.95</price>
        //     <description>
        //     Light Belgian waffles covered with strawberries and whipped cream
        //     </description>
        //     <calories>900</calories>
        //     </food>
        //     <food>
        //     <name>Berry-Berry Belgian Waffles</name>
        //     <price>$8.95</price>
        //     <description>
        //     Belgian waffles covered with assorted fresh berries and whipped cream
        //     </description>
        //     <calories>900</calories>
        //     </food>
        //     <food>
        //     <name>French Toast</name>
        //     <price>$4.50</price>
        //     <description>
        //     Thick slices made from our homemade sourdough bread
        //     </description>
        //     <calories>600</calories>
        //     </food>
        //     <food>
        //     <name>Homestyle Breakfast</name>
        //     <price>$6.95</price>
        //     <description>
        //     Two eggs, bacon or sausage, toast, and our ever-popular hash browns
        //     </description>
        //     <calories>950</calories>
        //     </food>
        //     </breakfast_menu>
        
        #endregion
        
        Person tom = new Person("Tom", 37);
        string json = JsonSerializer.Serialize(tom);
        Console.WriteLine(json);
        Person? restoredPerson = JsonSerializer.Deserialize<Person>(json);
        Console.WriteLine(restoredPerson?.Name); // Tom
        
    }
}

class Person
{
    public string Name { get;}
    public int Age { get; set; }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}