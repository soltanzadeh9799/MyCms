using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataLayer
{
    public class PageCommentRepository : IPageCommentRepository
    {
        private MyCmsContext db;

        public PageCommentRepository(MyCmsContext context)
        {
            this.db = context;
        }

        public IEnumerable<PageComment> GetCommentByNewsId(int pageId)
        {
            return db.PageComments.Where(c => c.PageID == pageId);
        }
        public bool AddComment(PageComment pageComment)
        {
            try
            {
                db.PageComments.Add(pageComment);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
