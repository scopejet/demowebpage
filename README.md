# example-aspnet-web-pages-webforms-mvc
https://code.tutsplus.com/courses/aspnet-web-pages-web-forms-and-mvc/lessons/introduction

# Additional Notes #
- [Web Forms, MVC, and Web Pages! Oh my!](https://www.codeproject.com/Articles/665118/Web-Forms-MVC-and-Web-Pages-Oh-my)
- [Why ASP.NET MVC and MVC vs WebForms ? ( Learn MVC 5 series)](https://www.youtube.com/watch?v=bGpBgDDDVlM)

# ASP.NET PAGES #
- Done in WebMatrix2 (deprecated) and Visual Studio
- Web Pages framework, Microsoft's version of dumbing-down ASP.NET
- Use to create simple old school websites.
- Web Page framework is a modern take of mixing markup and server side code together.


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

![2.6](https://github.com/sarn1/example-aspnet-web-pages-webforms-mvc/blob/master/Images/2.6.png)

## 2.7 Security ##
- *Lesson 8*
- CSRF and Line 82-97 ASP.NET auto-captures if a input may have HTML in it.

## 2.8 Using The Query String ##
```csharp
  var id = Request.QueryString["id"];
  var isInvalidId = id == null || !id.IsInt(); // true if ID == null or not integer
  dynamic message = null;
```
## 2.9 Restructuring & Routing For Pages ##
- * Lesson 10 *
- Simple page that allows input and show records from a table.
- localhost:1111/contact/log/1

![2.9](https://github.com/sarn1/example-aspnet-web-pages-webforms-mvc/blob/master/Images/2.9.png)

## 2.10 Razor Layouts ##
```html
<!-- /contact/success.chtml -->
@{
    Layout = "~/_Layout.cshtml";
    Page.Title = "Thank You!";
}

<p>Thanks for submitting your message. <a href="contact.cshtml">Contact us again!</a></p>


<!-- /contact/log.chtml -->
@{
    Layout = "~/_Layout.cshtml";
    Page.Title = "Contact Log";
    var id = UrlData[0];
}

@if (string.IsNullOrEmpty(id)) {
    @RenderPage("logdisplay.cshtml", id);
} else {
    Page.Title = "Contact Message Detail";
    @RenderPage("contactdetail.cshtml", id);
}
```

```html
<!--  Layout.chtml -->
<!DOCTYPE html>

<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>@Page.Title</title>
        <style>
            .field-validation-error { color:  red; }
            .even { background-color: #e1f3ff;}
            .odd { background-color:  white; }        
        </style>
    </head>
    <body>
        @RenderBody()
    </body>
</html>

```

## 2.11/2.12 ASP.NET Memberships And Roles For Pages ##
- *Lesson 12* and *Lesson 13*
- Simple registration, login, logout and protected areas.
```csharp
if (WebSecurity.IsAuthenticated) {
    <p>Welcome, @WebSecurity.CurrentUserName | <a href="/logout">Logout</a></p>
}
```
- _AppStart.cshtml contains the database schema and ASP.NET will create the table appropriately.
```razor
﻿@{
    WebSecurity.InitializeDatabaseConnection("Lesson12", "UserProfile", "UserID", "Email", false); 
}

@if(Roles.IsUserInRole("Admin")) {
    <p>This is super protected information.</p>
}
```
![2.11](https://github.com/sarn1/example-aspnet-web-pages-webforms-mvc/blob/master/Images/2.11-2.12.png)

## 2.13 Intro To Visual Studio ##
- *New Project* and *New Web Sites* are essentially the same thing in that they are ASP.NET application.  However they are handling different during run-time.  A project is compiled, and you put the compiled binary on the server.  The server has to just run the binary and perhaps compile a few other files, the binaries are in memory for about 20 minutes then shutdown and release from memory.  A Web Site is not compiled.  The server compiles and then put in the memory.  And at a low about 20 minutes then release from memoery.
- Projects are faster but Web Sites allows for flexibility to drop in a file.
- Web Pages can be found in project.  You just see Web Forms and MVC.
- Web Pages are apart of Web Sites.  ASP.NET Web Site (Razor v2)
- *Lesson 14* has a sample Web Sites created from Visual Studio.

![2.13](https://github.com/sarn1/example-aspnet-web-pages-webforms-mvc/blob/master/Images/2.13.png)

# Web Forms #
- Done in Visual Studio
- ======== SKIPPED FOR NOW ========



# MVC #
- Done in Visual Studio
- MVC prioneer by Xerox
- Easier to maintain and test in today's TDD environment.
- Model - data representation, its not the data itself - that's the data store, its not the data-access - that's the data-access layer.  It does represent the data.  Model is used by the View to show data to the user.
- View - The View knows about the Model, but the Model does not know about the View, also responsible of getting data from the user.  Serves as both input and output data.
- Controller - Glues the Model and View together.  Gets request from the View and gets what it needs from the Model onto the View.  Sometimes even straight to the data-access layer.
- ASP.NET MVC 4 > Basic
- Unlike *Web Pages* & *Web Sites* the routing is central to MVC.  And the 2 predecessor methods, the routing is based on the physical file on the file server that handles the request.  MVC app is not file based.  The response from that is sent back to the browser is the result of calling a method on a class.  
- Below in RouteConfig.cs, we have one route specified named Default with 3 pattern parameters
	- controller - class
	- action - method on that controller class
	- id - any other parameters

```csharp
// matches: localhost.home/index/10
// look for HomeController the look for index method and then pass value of 10
routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }	// if nothing is specified for controller/action/id then use these default and id is optional
);
```

