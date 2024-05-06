using Articles.core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Articles.data.SqlServerEF
{
   public class CategoryEntity : IdataHelper<category>
    {
        private DBContext Db;
        private category _table;
        public CategoryEntity()
        {
            Db = new DBContext();
        }
        public int Add(category table)
        {
            if (Db.Database.CanConnect())
            {

                Db.Category.Add(table);
                Db.SaveChanges();

                return 1;

            }
            else {
                return 0;
            
            
            }

          
        }

        public int Delete(int Id)
        {
            if (Db.Database.CanConnect())
            {
                _table = Find(Id);


                Db.Category.Remove(_table);
                Db.SaveChanges();

                return 1;

            }
            else
            {
                return 0;


            }
        }

        public int Edit(category table, int Id)
        {
            Db = new DBContext();
            if (Db.Database.CanConnect())
            {

                Db.Category.Update(table);
                Db.SaveChanges();
                return 1;

            }
            else
            {
                return 0;


            }
        }

        

        public category Find(int Id)
        {
            if (Db.Database.CanConnect())
            {
                return Db.Category.Where(x => x.Id == Id).First();


            }
            else {
                return null;
            
            
            }

        }

        public List<category> GetAllData()
        {
            if (Db.Database.CanConnect())
            {
                return Db.Category.ToList();


            }
            else
            {
                return new List<category>();


            }
        }

        public List<category> GetDataByUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<category> Search(string SerchItem)
        {
            
            if (Db.Database.CanConnect())
            {
                return Db.Category.Where(x => x.name.Contains(SerchItem) ||
                x.Id.ToString().Contains(SerchItem)).ToList();


            }
            else
            {
                return null;


            }
        }
    }
}
