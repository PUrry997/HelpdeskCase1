using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace HelpdeskDAL
{
    public class ProblemDAO
    {
        private IRepository repo;
        public ProblemDAO()
        {
            repo = new HelpdeskRepository();
        }

        public Problem GetById(string objectId)
        {
            Problem pro = null;
            var builder = Builders<Problem>.Filter;
            var filter = builder.Eq("Id", new ObjectId(objectId));

            try
            {
                pro = repo.GetOne<Problem>(filter);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "GetById");
            }
            return pro;
        }

        public List<Problem> GetAll()
        {
            List<Problem> pro = null;

            try
            {
                pro = repo.GetAll<Problem>();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "GetById");
            }
            return pro;
        }

        public Problem Create(Problem pro)
        {
            try
            {
                repo.Create<Problem>(pro);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Create");
            }
            return pro;
        }

        public long Delete(string objectId)
        {
            try
            {
                return repo.Delete<Problem>(objectId);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Create");
                return 0;
            }
        }

        public UpdateStatus Update(Problem pro)
        {
            UpdateStatus status = UpdateStatus.Failed;
            try
            {
                var filter = Builders<Problem>.Filter.Eq("Id", pro.Id) & Builders<Problem>.Filter.Eq("Version", pro.Version);
                var update = Builders<Problem>.Update
                    .Set("Description", pro.Description)
                    .Inc("Version", 1);

                status = repo.Update(pro.Id.ToString(), filter, update);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Update");
            }
            return status;
        }
    }
}
