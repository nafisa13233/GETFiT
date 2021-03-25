# GETFiT
 GETFiT is the most convenient way to connect users with a local personal trainer. Whether users are looking to simply get healthier or get into a specific fitness activity, we will match them with a one-on-one or virtual trainer who can customize each session to meet their fitness goals. Once users have found the right trainer, they can sign up for in-person training sessions on their profile page. Sign up, get in contact with their personal trainer, feel confident in their own decision. Now that users have a personal trainer in their corner, it's time to get to work. Their 1-on-1 or virtual trainer will guide them during workouts and set them up for success outside of your training sessions.
To run the project in visual studio 2017 or above is needed.

To create the database open "Web.config" and modify the following line

 <connectionStrings>
    <add name="DefaultConnection" connectionString="Data Source= YOUR_SQLSERVER_NAME; Initial Catalog = GETFiT; Integrated Security=True;" providerName="System.Data.SqlClient" />
 </connectionStrings>
 
 Replace YOUR_SQLSERVER_NAME with the Name of SQL Server installed in your computer.

After first run database named "GETFiT" will be created. Then insert the following data into AspNetRoles table of database.

Id	Name
1	Admin
2	Doctor
3	Patient

After inserting data run the project from Admin/AdminAccount/AdminLogin page.
