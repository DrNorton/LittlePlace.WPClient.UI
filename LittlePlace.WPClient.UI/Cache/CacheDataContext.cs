using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using LittlePlace.Api.Cache;

namespace LittlePlace.WPClient.UI.Cache
{
    public class CacheDataContext:DataContext
    {
        public CacheDataContext(string connectionString)
            :base(connectionString)
        {
            
        }

        public Table<CacheResult> CasheResults;
    }
}
