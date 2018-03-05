using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for ClassName
/// </summary>
public class People2
{
    public static IEnumerable<Person> GetPeople(string path) {

        var root = XElement.Load(path);

        return from el in root.Elements("person")
                select new Person() {
                    FirstName = (string)el.Element("firstName"),
                    LastName = (string)el.Element("lastName")
                };
    }
}
