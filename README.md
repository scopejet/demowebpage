# example-aspnet-web-pages-webforms-mvc
https://code.tutsplus.com/courses/aspnet-web-pages-web-forms-and-mvc/lessons/introduction

# Additional Notes #
- [Web Forms, MVC, and Web Pages! Oh my!](https://www.codeproject.com/Articles/665118/Web-Forms-MVC-and-Web-Pages-Oh-my)
- [Why ASP.NET MVC and MVC vs WebForms ? ( Learn MVC 5 series)](https://www.youtube.com/watch?v=bGpBgDDDVlM)

# ASP.NET PAGES #

## 2.2 Introduction To Razor ##
```html
@{
    var errorMessage = "Something went wrong";
}
<!DOCTYPE html>

<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>My Site's Title</title>
        <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
        <style>
            .bold { font-weight: bold;}
            .italic { font-style: italic; }
        </style>
    </head>
    <body>
        
            @if (!string.IsNullOrEmpty(errorMessage)) {
                <h1>@errorMessage</h1>
            }
```
## 2.3 Namespaces and File Paths ##
```xml
﻿<?xml version="1.0" encoding="utf-8" ?>

<people>
    <person>
        <firstName>Jeremy</firstName>
        <lastName>McPeak</lastName>
    </person>
    <person>
        <firstName>Jeffrey</firstName>
        <lastName>Way</lastName>
    </person>
    <person>
        <firstName>John</firstName>
        <lastName>Doe</lastName>
    </person>
    <person>
        <firstName>Johnny</firstName>
        <lastName>Bravo</lastName>
    </person>
</people>
```

```html
﻿@using System.Xml.Linq;

@{
    var path = Server.MapPath("people.xml");

    var root = XElement.Load(path);
}

<!DOCTYPE html>

<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title></title>
    </head>
    <body>
        @{
			// select with anonymous obj
            var people = from el in root.Elements("person")
                select new {
                    FirstName = (string)el.Element("firstName"),
                    LastName = (string)el.Element("lastName")
                };

            foreach (var person in people) {
                <p>@person.FirstName @person.LastName</p>
            }
        }
```

## 2.4 Methods and Code ##
- *App_Code* is a special folder within ASP.NET website, you can drop code in here and the data type in here is global within the application and this is not accessible to the client and is compiled.
- *People.cshtml* is the method style (statically typed webpage) while *People2.cs* is the class style.  The class style is better.
- See lesson 5 folder.

## 2.5 Form and Data Basics ##
- *Lesson 6* simple contact form that post to itself and then use `Request` to get the data and store into the database.
- Also provides a sample database object.

```csharp
    public IEnumerable<dynamic> GetAll() {
        return _db.Query("SELECT * FROM ContactLog ORDER BY DateSent DESC");
    }

	 @using (var repo = new ContactLogRepository("Lesson06")) {
        var counter = 1;
        foreach (var log in repo.GetAll()) {
            var cssClass = counter++ % 2 == 0 ? "even" : "odd";
            <tr class="@cssClass">
                <td>@log.Name</td>
                <td>@log.Subject</td>
                <td>@log.DateSent.ToString("yyyy-MM-dd HH:mm")</td>
            </tr>
        }
    }
```
## 2.6 Validating Form Input ##
- *Lesson 7*
- Interesting way to server side validate.

## 2.7 Security ##
- *Lesson 8*
- CSRF and Line 82-97 ASP.NET auto-captures if a input may have HTML in it.

## 2.8 Using The Query String ##
```csharp
  var id = Request.QueryString["id"];
  var isInvalidId = id == null || !id.IsInt(); // true if ID == null or not integer
  dynamic message = null;
```
## 2.9 Restructuring & Routing For Websites ##
- * Lesson 10 *
- localhost:1111/contact/log/1