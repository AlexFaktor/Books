# Description
In this project, you can add books to the database in bulk using csv. You can also filter them and get them into another file.

## Task 1
```
You have two options to implement:
Option 1: Add Books from File
The user provides a file path via console input.
The program parses the file and stores the data in the database.
When the program is run with the same file twice, duplicate entries should not be created.
Option 2: Search Books
Implement book search functionality using filters.
Introduce a new class called Filter, with optional fields to specify filtering criteria. These filter settings should be specified in the appSettings.
Display the count of books matching the query and list the titles of those books in the console.
Save the output to a uniquely named file (using the date/time of saving) containing a list of books matching the query in the same format as the original input file.

class Filter
{
  public string? Title {get;set}
  public string? Genre {get;set} 
  public string? Author {get;set} 
  public string? Publisher {get;set} 
  public int? MoreThanPages {get;set;} 
  public int? LessThanPages {get;set;} 
  public DateTime? Published Before {get;set;} 
  public DateTime? PublishedAfter{get; set;}
}

[File with a list of books in the csv format will be provided]

[Prerequisites]
Console app - .net 7 SQL Server DB
Entity Framework Core, code first approach

[Steps to follow]
1. Create a console app 
2. Add the following classes:

[Book]
Id: Guid
Title: String
Pages: Int
Genreld: Guid
Authorld: Guid
Publisherld: Guid
ReleaseDate: DateTime

[Genre]
Id: Guid
Name: String

[Author]
Id: Guid
Name: String

[Publisher]
Id: Guid
Name: String

3. Add Entity Framework and create a DB context.
4. Add a migration which will create a DB based on the previously created classes.
```

# Contacts:
- **Email**: [mykhailo.honchar5@gmail.com](mailto:mykhailo.honchar5@gmail.com)
- **Telegram**: [WhyAlexFaktor](https://t.me/WhyAlexFaktor)
