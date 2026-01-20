using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PackingApplication.Helper
{
    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable(typeof(T).Name);

            var props = typeof(T).GetProperties(
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);

            foreach (var prop in props)
            {
                Type colType = Nullable.GetUnderlyingType(prop.PropertyType)
                               ?? prop.PropertyType;

                table.Columns.Add(prop.Name, colType);
            }

            foreach (var item in data)
            {
                DataRow row = table.NewRow();
                foreach (var prop in props)
                {
                    object value = prop.GetValue(item, null);
                    row[prop.Name] = value ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }

}
