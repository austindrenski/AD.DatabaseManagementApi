# DatabaseManagementApi
C# framework for executing queued database management tasks.
## Install from NuGet:
```
PM> Install-Package DatabaseManagementApi
```
## Example:
### Service implementation:
A service class inherits from ```DbManagementService```. The database work should be performed inside of the overridden ```Invoke()``` method. This method should return the number of records that were modified. This number will be written to the standard output.
```C#
using DatabaseManagementApi;

public class Service1 : DbManagementService
{
    public ComtradeMetadataService(DbContext context) : base(context) { }

    protected override int Invoke()
    {
        // Add one record to the context     
        Context.Table1.Add(...);
        
        // Return the number of records affected
        return 1;
    }
}

public class Service2 : Service1 { }
```
### Console implementation:
The ```DbManagementController``` should be used wrapped in a ```using``` block. Services can be added to the controller by calling the ```Message(...)``` method. These services are added to the controller's queue and invoked consecutively when the controller's ```Invoke()``` method is called.
```C#
using System.Data.Entity
using DatabaseManagementApi;

public static void Main()
{
    using (DbManagementController controller = new DbManagementController(new DbContext()))
    {
        // Add one service to the controller
        controller.Message("Execute first update service?", Service1());
        
        // Add a second service to the controller
        controller.Message("Execute second update service?", Service2());
        
        // Invoke the services
        controller.Invoke();
    }
}
```
### Console output:
```
Executing services at 12:00 PM. Please wait...

> Starting service 1 of 2 at 12:00 PM : Service1.
> Service1 completed with 1 record affected in 1 minute.

> Starting service 2 of 2 at 12:01 PM : Service2.
> Service2 completed with 1 record affected in 1 minute.

Completed 2 services in 2 minutes. Press enter to exit...
```
