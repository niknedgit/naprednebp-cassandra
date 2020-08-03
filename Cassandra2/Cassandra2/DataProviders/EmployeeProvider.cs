using Cassandra;
using Cassandra2.QueryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cassandra2.DataProviders
{
    class EmployeeProvider
    {
        public static Employee GetEmployee(string market_id,string jmbg)
        {
            ISession session = SessionManager.GetSession();
            Employee employee = new Employee();

            if (session == null)
                return null;

            Row employeeData = session.Execute("select * from \"Employee\"  where \"market_id\" = '" + market_id + "' and \"jmbg\" = '" + jmbg + "'").FirstOrDefault();

            if (employeeData != null)
            {
                employee.Jmbg = employeeData["jmbg"] != null ? employeeData["jmbg"].ToString() : string.Empty;
                employee.Name = employeeData["name"] != null ? employeeData["name"].ToString() : string.Empty;
                employee.MarketId = employeeData["market_id"] != null ? employeeData["market_id"].ToString() : string.Empty;
                employee.Salary = employeeData["salary"] != null ? employeeData["salary"].ToString() : string.Empty;
                employee.Workplace = employeeData["workplace"] != null ? employeeData["workplace"].ToString() : string.Empty;
                return employee;
            }
            return null;
        }

        public static List<Employee> GetEmployees()
        {
            ISession session = SessionManager.GetSession();
            List<Employee> employees = new List<Employee>();

            if (session == null)
                return null;

            var employeesData = session.Execute("select * from \"Employee\"");

            foreach (var employeeData in employeesData)
            {
                Employee employee = new Employee();
                employee.Jmbg = employeeData["jmbg"] != null ? employeeData["jmbg"].ToString() : string.Empty;
                employee.Name = employeeData["name"] != null ? employeeData["name"].ToString() : string.Empty;
                employee.MarketId = employeeData["market_id"] != null ? employeeData["market_id"].ToString() : string.Empty;
                employee.Salary = employeeData["salary"] != null ? employeeData["salary"].ToString() : string.Empty;
                employee.Workplace = employeeData["workplace"] != null ? employeeData["workplace"].ToString() : string.Empty;
                employees.Add(employee);
            }

            return employees;
        }
        public static List<Employee> GetEmployeesOfOneMarket(string m_id)
        {
            ISession session = SessionManager.GetSession();
            List<Employee> employees = new List<Employee>();

            if (session == null)
                return null;

            var employeesData = session.Execute("select * from \"Employee\"where \"market_id\"= '" + m_id + "'");

            foreach (var employeeData in employeesData)
            {
                Employee employee = new Employee();
                employee.Jmbg = employeeData["jmbg"] != null ? employeeData["jmbg"].ToString() : string.Empty;
                employee.Name = employeeData["name"] != null ? employeeData["name"].ToString() : string.Empty;
                employee.MarketId = employeeData["market_id"] != null ? employeeData["market_id"].ToString() : string.Empty;
                employee.Salary = employeeData["salary"] != null ? employeeData["salary"].ToString() : string.Empty;
                employee.Workplace = employeeData["workplace"] != null ? employeeData["workplace"].ToString() : string.Empty;
                employees.Add(employee);
            }

            return employees;
        }

        public static bool AddEmployee(string jmbg, string name, string market_id, string salary, string workplace)
        {
            ISession session = SessionManager.GetSession();

            if (session == null)
                return false;

            if (GetEmployee(market_id, jmbg) == null)
            {
                RowSet EeployeeData = session.Execute("insert into \"Employee\" (\"jmbg\", name, market_id, salary,workplace)  " +
                "values ('" + jmbg + "', '" + name + "', '" + market_id + "', '" + salary + "', '" + workplace + "')");
                return true;
            }
            return false;
        }

        public static bool DeleteEmployee(string jmbg, string market_id)
        {
            ISession session = SessionManager.GetSession();
            Employee employee = new Employee();

            if (session == null)
                return false;

            if (GetEmployee(market_id, jmbg) != null)
            {
                var employeeData = session.Execute("delete from \"Employee\"  where \"market_id\" = '" + market_id + "' " +
                     "and \"jmbg\" = '" + jmbg + "'");
                return true;
            }
                return false;
        }
    }
}
