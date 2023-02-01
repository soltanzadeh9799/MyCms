using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IPageCommentRepository:IDisposable
    {
        IEnumerable<PageComment> GetCommentByNewsId(int pageId);
        bool AddComment(PageComment pageComment);
       
    }
}
