using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace HelpdeskDAL
{
    public class EmployeeDAO
    {
        private IRepository repo;
        public EmployeeDAO()
        {
            repo = new HelpdeskRepository();
        }
        public Employee GetByLastname(string name)
        {
            Employee emp = null;
            var builder = Builders<Employee>.Filter;
            var filter = builder.Eq("Lastname", name);

            try
            {
                emp = repo.GetOne<Employee>(filter);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetByLastname");
            }
            return emp;
        }
        public Employee GetById(string objectId)
        {
            Employee emp = null;
            var builder = Builders<Employee>.Filter;
            var filter = builder.Eq("Id", new ObjectId(objectId));

            try
            {
                emp = repo.GetOne<Employee>(filter);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetById");
            }
            return emp;
        }

        public List<Employee> GetAll()
        {
            List<Employee> emp = null;

            try
            {
                emp = repo.GetAll<Employee>();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetById");
            }
            return emp;
        }
        public Employee Create(Employee emp)
        {
            try
            {
                repo.Create<Employee>(emp);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Create");
            }
            return emp;
        }
        public long Delete(string objectId)
        {
            try
            {
                return repo.Delete<Employee>(objectId);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Create");
                return 0;
            }
        }
        public UpdateStatus Update(Employee emp)
        {
            UpdateStatus status = UpdateStatus.Failed;
            try
            {
                var filter = Builders<Employee>.Filter.Eq("Id", emp.Id) & Builders<Employee>.Filter.Eq("Version", emp.Version);
                var update = Builders<Employee>.Update
                    .Set("DepartmentId", emp.GetDepartmentIdAsString())
                    .Set("Email", emp.Email)
                    .Set("Firstname", emp.Firstname)
                    .Set("Lastname", emp.Lastname)
                    .Set("Phoneno", emp.Phoneno)
                    .Set("Title", emp.Title)
                    .Inc("Version", 1);

                status = repo.Update(emp.Id.ToString(), filter, update);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Update");
            }
            return status;
        }
    }
}
