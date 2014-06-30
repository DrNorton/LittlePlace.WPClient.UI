using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace LittlePlace.Api.Cache
{
  [Table]
    public class CacheResult
    {
      private int _cacheRowId;
      private int _key;
      private string _resultText;

       [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
      public int CacheRowId
      {
          get { return _cacheRowId; }
          set { _cacheRowId = value; }
      }

       [Column(DbType = "INT NOT NULL")]
      public int Key
      {
          get { return _key; }
          set { _key = value; }
      }

       [Column(DbType = "ntext", UpdateCheck = UpdateCheck.Never)]
      public string ResultText
      {
          get { return _resultText; }
          set { _resultText = value; }
      }

     
    }
}
