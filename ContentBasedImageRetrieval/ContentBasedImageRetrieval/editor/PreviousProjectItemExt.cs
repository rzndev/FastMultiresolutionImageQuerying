using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ContentBasedImageRetrieval
{
    public static class PreviousProjectItemExt
    {
        public static void AddItem(
            this ObservableCollection<PreviousProjectItem> items,
            string item)
        {
            items.Add(new PreviousProjectItem()
            {
                DateUpd  = DateTime.Now, 
                ProjectPath = item
            });

            var list = items.OrderByDescending(x => x.DateUpd)
                .GroupBy(x => x.ProjectPath)
                .Select(y => y.First())
                .Take(15)
                .ToList();

            items.Clear();

            foreach (var li in list)
                items.Add(li);

        }
    }
}