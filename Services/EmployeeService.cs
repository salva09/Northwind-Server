using sample.DataAccess;
using System;
using System.Linq;

class EmployeeService : NorthwindService
{
    public Employee GetEmployeeById(int id)
    {
        if (!Exists(id)) return null;

        var employee = DbContext.Employees.First(e => e.EmployeeId == id);

        return new Employee 
        {
            id = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };
    }

    public void UpdateEmployee(int id, UpdateEmployee newState)
    {
        if (!Exists(id)) return;
        
        var employee = DbContext.Employees.First(e => e.EmployeeId == id);

        employee.FirstName = newState.FirstName;
        DbContext.SaveChanges();
    }

    public void AddEmployee(NewEmployee employee)
    {
        DbContext.Add(new sample.DataAccess.Employee
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName
        });
        DbContext.SaveChanges();
    }

    public void DeleteEmployeeById(int id)
    {
        if (!Exists(id)) return;

        var employee = DbContext.Employees.First(e => e.EmployeeId == id);
        var employeeOrders = DbContext.Orders.Where(e => e.EmployeeId == id).ToList();
        var employeeTerritories = DbContext.EmployeeTerritories.Where(e => e.EmployeeId == id).ToList();
        var employeesReporting = DbContext.Employees.Where(e => e.ReportsTo == id).ToList();

        employeeOrders.ForEach(e => e.EmployeeId = null);
        employeeTerritories.ForEach(e => DbContext.EmployeeTerritories.Remove(e));
        employeesReporting.ForEach(e => e.ReportsTo = null);

        DbContext.Employees.Remove(employee);
        DbContext.SaveChanges();
    }

    public bool Exists(int id)
    {
        try
        {
            DbContext.Employees.First(e => e.EmployeeId == id);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
