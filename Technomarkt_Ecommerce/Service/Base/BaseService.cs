using Core;
using Core.Service;
using DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Service.Base
{
    public class BaseService<T> : ICoreService<T> where T : EntityRepository
    {
        ApplicationContext db = new ApplicationContext();

        public string Add(T model)
        {
            try
            {
                model.ID = Guid.NewGuid();
                db.Set<T>().Add(model);
                db.SaveChanges();
                return $"Data added successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public bool Any(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Any(exp);
        }

        public string Delete(T model)
        {
            try
            {
                db.Set<T>().Remove(model);
                db.SaveChanges();
                return "Data deleted successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public T GetById(Guid id)
        {
            return db.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().Where(exp).ToList();
        }

        public List<T> GetList()
        {
            return db.Set<T>().ToList();
        }

        public string Update(T model)
        {
            try
            {
                T updated = GetById(model.ID);
                updated.UpdatedDate = DateTime.Now.ToString("dddd, dd MMMM yyyy", new CultureInfo("en-GB"));
                updated.UpdatedComputerName = Environment.MachineName;
                updated.UpdatedIP = RemoteIpAddress.GetIpAddress();
                updated.UpdatedAdUsername = "Admin";
                updated.UpdatedBy = "Admin";
                DbEntityEntry entity = db.Entry(updated);
                entity.CurrentValues.SetValues(model);
                db.SaveChanges();
                return "Data updated successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
