<h4>
My attempt to learn ASP.NET API Development following a course Teddy Smith(https://www.youtube.com/playlist?list=PL82C6-O4XrHde_urqhKJHH-HTUfTK6siO).
I learnt creating APIs in ASP.NET using the Repository Pattern. Seperating Data from Operations. 
</h4>

SignalR is included in the project but was not really the focus. - To test, the signalR test is in Index -Copy of Home View.
``` C#
builder.Services.AddSignalR();

;;;
app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<ChatHub>("/chat");
});
```
Turns out if you do not name your views folder according to your Controller,it won't know (by default) which view you want to display. The 
views name should also match the name of the function in the Controller. If it does not match, it won't know which one to return 
after the function has run. 

In the controller, `IActionResult` is always the return type, if async, `Task<IActionResult>` is the return type. 
The return value is View(), which will be inherited from `Asp.NetCore.Mvc.Controller` by the controller, 
or a RedirectToAction() function can also be returned.  

Added Authentication to the App, Using `Asp.NetCore.Identity` package. The user model inherited from `IdentityUser` so that we can use the 
boilercode made by microsoft for authentication, logging in, and authorisation (Using Roles). The API uses cookie authentication. 
``` C#

builder.Services.AddIdentity<AppUser, IdentityRole>()
		.AddEntityFrameworkStores<AppDBContext>();
;;;
builder.Services.AddSession(); //cookie authentication.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie();
;;;
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
```
The Database used is SQL. Managed or third-party viewable from SQL Server Management Studio. The AppDbContext (the store), inherited
from `EntityFrameWorkCore.DbContext` and returned `DbSet`. When Identity was added, the inheritance moved to 
`Asp.NetCore.Identity.EntityFrameworkCore.IdentityDbContext<TUser> where TUser : IdentityUser`. 
To interact with DB data, 
```C#
builder.Services.AddDbContext<AppDBContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```

When changing Models that already used by Entityframework to create a database, it is not so straightforward to remove or add a new field.
To make model updates and to update Db Constraints, we have to migrate and update the database. 
** 
1. Make your changes to the model
2. Migrate the Database
		`Add-Migration NameOfMigration`
3. Update databse
		`Update-Database`
_In the package console_
**



~Remove idk cloud~, !cloud details?
