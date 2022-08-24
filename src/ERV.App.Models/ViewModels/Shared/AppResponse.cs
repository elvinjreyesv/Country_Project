using ERV.App.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERV.App.Models.ViewModels.Shared
{
    public class AppResponse : AppResponse<string>
    {
        public static AppResponse Create(EAppResponse status) => new AppResponse() { Status = status };

        public static AppResponse<TStatusEntry, string> CreateByStatus<TStatusEntry>(TStatusEntry status, string value = "") where TStatusEntry : struct
        {
            return new AppResponse<TStatusEntry, string> { Status = status, Result = value };
        }
    }
    public class AppResponse<TEntity> : AppResponse<EAppResponse, TEntity>
    {
        public static AppResponse<TEntityEntry> Create<TEntityEntry>(EAppResponse status, TEntityEntry value) => new AppResponse<TEntityEntry> { Status = status, Result = value };
        public static AppResponse<TEntityEntry> Create<TEntityEntry>(EAppResponse status) => new AppResponse<TEntityEntry> { Status = status, Result = default(TEntityEntry) };
    }
    public class AppResponse<TStatus, TEntity>
    {
        public TStatus Status { get; set; }
        public TEntity Result { get; set; }

        public static AppResponse<TStatusEntry, TEntityEntry> Create<TStatusEntry, TEntityEntry>(TStatusEntry status, TEntityEntry value)
        {
            return new AppResponse<TStatusEntry, TEntityEntry> { Status = status, Result = value };
        }

        public static AppResponse<TStatusEntry, TEntityEntry> Create<TStatusEntry, TEntityEntry>(TStatusEntry status) where TStatusEntry : struct
        {
            return new AppResponse<TStatusEntry, TEntityEntry> { Status = status, Result = default(TEntityEntry) };
        }

        public static AppResponse<TStatusEntry, List<TEntityEntry>> CreateEmptyList<TStatusEntry, TEntityEntry>(TStatusEntry status)
            where TStatusEntry : struct
        {
            return new AppResponse<TStatusEntry, List<TEntityEntry>> { Status = status, Result = Enumerable.Empty<TEntityEntry>().ToList() };
        }
    }
}
