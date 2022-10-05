# CustomerManagement

List of Assumptions:

 - The Item - Product relationship is one to one. Therefore, the two entities can be combined to one database table (in this case "Item")
 
 - The API class is the Controller.
 
 - Each table i.e. Customers, Orders and Items have self-generating / self-incrementing IDs as primary keys. It is assumed that the user will not try to manually insert an ID.

 - Legal/Illegal values for each Create, Read, Update, Delete operation are described in the API through XML documentation.

 - Repository, Unit of work and CQRS patterns have been implemented.

 - Also a generic repository patttern has been used rather than a seperate repository for each database table. 

 - Unit tests have not been developed. I did not have the time.
 
 - The Program.cs file contains some test values for the application.