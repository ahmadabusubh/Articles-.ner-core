using Articles.core;
using Articles.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.data.SqlServerEF
{
    public class StudentEntity : IdataHelper<Student>
    {
        private DBContext dbContextt;
        private Student _table;

        public StudentEntity()
        {
            dbContextt = new DBContext();

        }
        public int Add(Student table)
        {
            if (dbContextt.Database.CanConnect())
            {

                dbContextt.Students.Add(table);
                dbContextt.SaveChanges();

                return 1;

            }
            else
            {
                return 0;

            }
        }

        public int Delete(int Id)
        {


            if (dbContextt.Database.CanConnect())
            {
                _table = Find(Id);


                dbContextt.Students.Remove(_table);
                dbContextt.SaveChanges();

                return 1;

            }
            else
            {
                return 0;


            }
        }

        public int Edit(Student table, int Id)
        {

            if (dbContextt.Database.CanConnect())
            {

                dbContextt.Students.Update(table);
                dbContextt.SaveChanges();
                return 1;

            }
            else
            {
                return 0;


            }
        }

        public Student Find(int Id)
        {
            if (dbContextt.Database.CanConnect())
            {
                return dbContextt.Students.FirstOrDefault(x => x.Id == Id);


            }
            else
            {
                return null;


            }
        }

        public List<Student> GetAllData()
        {
            if (dbContextt.Database.CanConnect())
            {
                return dbContextt.Students.ToList();


            }
            else
            {
                return new List<Student>();


            }
        }

        public List<Student> GetDataByUser(string UserId)
        {
            throw new NotImplementedException();
        }

        public List<Student> Search(string SerchItem)
        {

            if (dbContextt.Database.CanConnect())
            {
                return dbContextt.Students.Where(x => x.Name.Contains(SerchItem) ||
                x.StudentId.Contains(SerchItem) ||
                x.Id.ToString().Contains(SerchItem)).ToList(); 


            }
            else
            {
                return null;


            }

        }
    }
}
