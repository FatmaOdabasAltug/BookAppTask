# BookAppTask
## Endpoints:

1. ***GET /api/Author***: Returns a list of authors and their IDs.
   - Author names are pre-defined. Reason is that whenever a new book is being added, we will show pre-defined set of authors to provide consistent naming convention across the system.
   - Whenever system runs, the author table will be cleaned, and then populated with dummy author names.

2. ***GET /api/Book***: Returns list of information of the books



3. ***POST /api/Book***: Creates the book entity. Authors must be added using their IDs.
   - We have some validation and required fields (e.g., title).
   - Trim check is also implemented.
   - Title is checked for uniqueness.
   - Whenever a new book is created: A new record is added to the Book table. Additionally another record is added to the BookHistories table.
   - For instance; A new record is added to the Book table, then we will add a new record with the same bookId to BookHistories. If an error occurs during adding the record to BookHistories, the record in Book table will be removed (rollback). 



4) ***GET /api/Book/{id}***: Returns the information of book with given id





5) ***PUT /api/Book/{bookId}***: Updates the book information
   - We have some validation and required fields (e.g., title).
   - Trim check is also implemented.
   - Title is checked for uniqueness.
   - Whenever a new book is created: A new record is added to the Book table. Additionally another record is added to the BookHistories table.
   - For instance; A new record is updated to the Book table, then we will add a new record with the same bookId to BookHistories. If an error occurs during updating the record to BookHistories, the record in Book table will be removed (rollback). 
   - Each field that is being updated is saved to the BookHistories. History will contain: time of change, old value, new value, changed property (author, title,...), and description.



6) ***GET /api/BookHistory***: Returns list of filtered book history records.
   - Search options are order by, filter, and group by. Additionally page number and page size can be changed.
   - There are validation controls.
   - By default filter will be applied to title, description, date, and authors
   - Grouping can be done to:
     - Title
     - Description
     - PublishDate
     - Authors

   - After the grouping. Filter will be used only for that group (e.g., group is Title, filter "Hobbit" will only be applied to the titles).



