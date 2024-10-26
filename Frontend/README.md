Create a backend component in ASP.NET Core with swagger endpoints that allows displaying the history of changes of book entities with pagination, filtering, ordering and optionally grouping.

The book entity should have 
-	An id
-	A title
-	A short description
-	Publish date
-	Authors

An example of changes could be a title was changed, an author was added, etc. It should also be possible to add books.
In the list of changes it should display at least the time of change and a description of what was changed (e.g. Title was changed to “The Hobbit”)
You can use any type of storage for the history that makes sense to you.
Purpose of this is to get to know your code style, so its not necessary to write unit tests and long documentation. You can make use of open source and free UI components as you see fit.

Since that task is not confidential, you can host the project in any public git repository or send the code to use in another way you prefer, but it should include the commits.
