using Articles.core;
using Microsoft.EntityFrameworkCore;
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
   public class AuthorEntity : IdataHelper<Author>
    {
        private DBContext Db;
        private Author _table;
        public AuthorEntity()
        {
            Db = new DBContext();
        }
        public int Add(Author table)
        {
            if (Db.Database.CanConnect())
            {

                Db.Author.Add(table);
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


                Db.Author.Remove(_table);
                Db.SaveChanges();

                return 1;

            }
            else
            {
                return 0;


            }
        }

        public int Edit(Author table, int Id)
        {
            Db = new DBContext();
            if (Db.Database.CanConnect())
            {

                Db.Author.Update(table);
                Db.SaveChanges();
                return 1;

            }
            else
            {
                return 0;


            }
        }

        

        public Author Find(int Id)
        {
            if (Db.Database.CanConnect())
            {
                //return Db.Author.AsNoTracking().FirstOrDefault(p => p.Id == Id);

               return Db.Author.Where(x => x.Id == Id).First();


            }
            else {
                return null;
            
            
            }

        }

        public List<Author> GetAllData()

        {
            if (Db.Database.CanConnect())
            {

                return Db.Author.ToList();


            }
            else
            {
                return null;


            }
        }

        public List<Author> GetDataByUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<Author> Search(string SerchItem)
        {
            
            if (Db.Database.CanConnect())
            {
                return Db.Author.Where(
                    x => x.FullName.Contains(SerchItem) ||
                     x.UserId.ToString().Contains(SerchItem) ||
                    x.UserName.Contains(SerchItem) ||
                    x.Bio.Contains(SerchItem) ||
                    x.Facebook.Contains(SerchItem) ||
                    x.Twitter.Contains(SerchItem) ||
                    x.Instagram.Contains(SerchItem) ||





                x.Id.ToString().Contains(SerchItem)).ToList();


            }
            else
            {
                return null;


            }
        }
    }
}
