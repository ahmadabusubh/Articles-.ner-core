using Articles.core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.data.SqlServerEF
{
    public class AuthorPostEntity : IdataHelper<AuthorPost>
    {
        private DBContext Db;
        private AuthorPost _table;

        public AuthorPostEntity()
        {
            Db = new DBContext();
        }
        public int Add(AuthorPost table)
        {
            if (Db.Database.CanConnect())
            {

                Db.AuthorPost.Add(table);
                Db.SaveChanges();

                return 1;

            }
            else
            {
                return 0;


            }
        }

        public int Delete(int Id)
        {
            if (Db.Database.CanConnect())
            {
                _table = Find(Id);
                Db.AuthorPost.Remove(_table);
                Db.SaveChanges();
                return 1;



            }
            else {
                return 0;
            
            }
        }

        public int Edit(AuthorPost table, int Id)
        {
            if (Db.Database.CanConnect())
            {

                Db.AuthorPost.Update(table);
                Db.SaveChanges();
                return 1;



            }
            else {
                return 0;
            
            }
        }

        public AuthorPost Find(int Id)
        {
            if (Db.Database.CanConnect())
            {
                //return Db.AuthorPost.Where(x => x.Id == Id).First();
                var  Authorpost= Db.AuthorPost.AsNoTracking().FirstOrDefault(p => p.Id == Id);
                return Authorpost;

            }
            else
            {
                return null;


            }
        }

        public List<AuthorPost> GetAllData()
        {

            if (Db.Database.CanConnect())
            {
                return Db.AuthorPost.ToList();


            }
            else
            {
                return null;


            }
        }

        public List<AuthorPost> GetDataByUser(string UserId)
        {
            if (Db.Database.CanConnect())
            {
                return Db.AuthorPost.Where(x=>x.UserId==UserId).ToList();


            }
            else
            {
                return null;


            }
        }

        public List<AuthorPost> Search(string SerchItem)
        {
            if (Db.Database.CanConnect())
            {
                return Db.AuthorPost.Where(
                    x => x.FullName.Contains(SerchItem) ||
                     x.UserId.ToString().Contains(SerchItem) ||
                    x.UserName.Contains(SerchItem) ||
                    x.PostDescription.Contains(SerchItem) ||
                    x.PostImageUrl.Contains(SerchItem) ||
                    x.PostCategory.Contains(SerchItem) ||
                    x.PostTitle.Contains(SerchItem) ||
                    x.AuthorId.ToString().Contains(SerchItem) ||
                    x.CategoryId.ToString().Contains(SerchItem) ||





                x.Id.ToString().Contains(SerchItem)).ToList();


            }
            else
            {
                return null;


            }
        }
    }
}
