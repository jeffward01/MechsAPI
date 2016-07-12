﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models;

namespace UMPG.USL.API.Data.LicenseData
{
    public class SolrIndexQueueRepository : ISolrIndexQueueRepository
    {
        public SolrIndexQueueItem Add(SolrIndexQueueItem indexQueueItem)
        {
            using (var context = new AuthContext())
            {
                context.SolrIndexQueues.Add(indexQueueItem);
                context.SaveChanges();
                return indexQueueItem;
            }
        }

        public SolrIndexQueueItem Update(SolrIndexQueueItem indexQueueItem)
        {
            using (var context = new AuthContext())
            {
                context.Entry(indexQueueItem).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                return indexQueueItem;
            }
        }


        public void Delete(SolrIndexQueueItem indexQueueItem)
        {
            using (var context = new AuthContext())
            {
                context.SolrIndexQueues.Attach(indexQueueItem);
                context.SolrIndexQueues.Remove(indexQueueItem);
                context.SaveChanges();
            }
        }

        public SolrIndexQueueItem ProcessItemFromQueue()
        {
            using (var context = new AuthContext())
            {
                var solrItem =
                    context.SolrIndexQueues.Where(i => i.SolrQueueStatus == (int) SolrIndexQueueState.Pending)
                        .OrderBy(i => i.SolrIndexQueueId)
                        .FirstOrDefault();
                return solrItem;
            }
        }
    }
}
