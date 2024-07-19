# Library Management, CHI XIE, stuId 2052833

## Prerequisites
1. Entity Framework installed on your machine
    ```shell
    dotnet tool install --global dotnet-ef
    ```
2. A compatible browser to view the web application (e.g., Chrome, Firefox).


## Set up and quick start
1. Open the Library Management folder in the terminal through cd command
2. Delete used database, build new database, and update database
    ```shell
    dotnet ef database drop #delete

    dotnet ef migrations add InitialCreate #build new database

    dotnet ef database update #update
    ```
3. Run the following command to start the application: 
    ```shell
    dotnet run
    ```
4. Copy the URL from the console output and open it in your browser.


## Features
1. Navigation Bar: In the navigation bar, you will see the following buttons:
 - Book
 - Author
 - Library Branch
 - Customer

2. Page Navigation: Clicking on the buttons in the navigation bar will take you to the respective pages:
 - Book page [Readable without login]
 - Author page  [Readable without login]
 - Library Branch page  [Readable without login]
 - Customer page  [Non-readable without login to protect customers' privacy]
 - Register Page, Login Page, Logout Page, Account Manage Page

3. CRUD Functionality
    3.1 **Read** On each page, you can view a list of existing data.
    3.2 **Create** 
    Click the *Create* button to create new data. 
    Fill in the form and click the *Submit* button to save the new data.
    3.3 **Edit** 
    Click the *Edit* button to modify the selected information. 
    Modify the form information and click the *Submit* button to save the changes. 
    If you do not want to modify the information anymore, you can click *back to list* button.
    3.4 **Delete** 
    Click the *Delete* button to delete the data with the corresponding ID. 
    The system will jump to a confirmation window, and you can choose yes and no.
    3.5. **Special Functionality** 
    When creating a Book, enter the Author ID and Branch ID. 
    When reading the book list, the Author Name and Branch Name will be displayed for the convenience of reader.
    3.6 **Error Handling** 
    While creating and editing process, if new id repeats with another existing id, error notice will appear.

4. Return to List
    Click the *Back to List* button to return to the list page of Book, Author, Library Branch, or Customer.

5. Social Media Account Authentication
    The user can register or login through their Facebook or Google account(s). 
    After registering, please click the "comfirm register" button to manually validate e-mail address. This function will be updated in the further version.

6. Return to Home Page
    Click the Library Management title to return to the home page.

