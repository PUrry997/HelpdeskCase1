using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace HelpdeskDAL
{
    public class DepartmentDAO
    {
        private IRepository repo;
        public DepartmentDAO()
        {
            repo = new HelpdeskRepository();
        }

        public Department GetById(string objectId)
        {
            Department dep = null;
            var builder = Builders<Department>.Filter;
            var filter = builder.Eq("Id", new ObjectId(objectId));

            try
            {
                dep = repo.GetOne<Department>(filter);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetById");
            }
            return dep;
        }

        public List<Department> GetAll()
        {
            List<Department> dep = null;

            try
            {
                dep = repo.GetAll<Department>();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetById");
            }
            return dep;
        }

        public Department Create(Department dep)
        {
            try
            {
                repo.Create<Department>(dep);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Create");
            }
            return dep;
        }

        public long Delete(string objectId)
        {
            try
            {
                return repo.Delete<Department>(objectId);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Create");
                return 0;
            }
        }

        public UpdateStatus Update(Department dep)
        {
            UpdateStatus status = UpdateStatus.Failed;
            try
            {
                var filter = Builders<Department>.Filter.Eq("Id", dep.Id) & Builders<Department>.Filter.Eq("Version", dep.Version);
                var update = Builders<Department>.Update
                    .Set("DepartmentName", dep.DepartmentName)
                    .Inc("Version", 1);

                status = repo.Update(dep.Id.ToString(), filter, update);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Update");
            }
            return status;
        }
    }
}
