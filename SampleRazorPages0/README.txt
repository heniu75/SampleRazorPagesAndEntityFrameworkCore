This is my own variation of walking through the following youtube video
see https://www.youtube.com/watch?v=yyBijyCI5Sk&t=529s


Notice the use of the .NET 2.0 standard Nuget package named
- Microsoft.AspNetCore.All

This is a meta package and includes 
- Microsoft.EntityFrameworkCore

Note that for this app, we are using an in memory DB. This is not to be done
in production but can be done in tests

For an example on how to do this in a test see

http://fiyazhasan.me/faking-with-in-memory-database-in-asp-net-core-2-0/
