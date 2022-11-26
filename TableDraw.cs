using ConsoleTables;
using System.Data;
using System.Reflection;
using DataRow = System.Data.DataRow;

namespace TeaStorel
{
    public static class TableDraw
    {
        public static void Table<T>(this IEnumerable<T> enumerable)
        {
            if (!enumerable.Any()) return;  // enumrable rong thi thoat

            DataTable data = TableDraw.ToDataTable(enumerable);

            string[] columnNames = data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            DataRow[] rows = data.Select();

            var table = new ConsoleTable(columnNames);

            foreach (DataRow row in rows)
            {
                table.AddRow(row.ItemArray);
            }

            table.Write(Format.Alternative);
        }

        //Ham Draw table co so cot duoc lua chon
        public static void TableHavePropSelected<T>(this IEnumerable<T> enumerable, string[] props)
        {
            DataTable data = TableDraw.ToDataTable(enumerable);

            var tb = new DataTable(typeof(T).Name);
            foreach (var prop in props)
            {
                tb.Columns.Add(prop, typeof(string));
            }
            foreach (var item in enumerable)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = TableDraw.GetPropValue(item, props[i]);
                }
                tb.Rows.Add(values);
            }

            string[] columnNames = tb.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            DataRow[] rows = tb.Select();
            var table = new ConsoleTable(props);

            foreach (DataRow row in rows)
            {
                table.AddRow(row.ItemArray);
            }

            table.Write(Format.Alternative);
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
