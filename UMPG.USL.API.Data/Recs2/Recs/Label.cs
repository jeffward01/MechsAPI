using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public class LabelRepository : ILabelRepository
    {

        public Int64 Add(Label label)
        {
            using (var context = new AuthContext())
            {
                context.Labels.Add(label);
                context.SaveChanges();

                return label.label_id;
            }
        }

        public Label Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Labels.FirstOrDefault(c => c.label_id == id);
            }
        }

        public List<Label> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.Labels.OrderBy(c => c.name).ToList();
            }
        }

        public List<Label> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var Labels = context.Labels.Where(c => c.name == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return Labels.Where(c => c.name.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return Labels.ToList();
                }
            }
        }

    }
}
