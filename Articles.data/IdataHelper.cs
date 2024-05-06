using Articles.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.data
{
    public interface IdataHelper<table>
    {
      List<table> GetAllData();
        List<table> GetDataByUser(string UserId);
        List<table> Search(string SerchItem);
        table Find(int Id);
        int Add(table table);
        int Edit(table table,int Id);
        int Delete(int Id);
    }
}
