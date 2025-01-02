using System.Collections.Generic;

namespace WorkflowNet.Core.Interfaces
{
    public interface IPages
    {
        IEnumerable<IPage> GetPages();
        IPage GetPage(string name);
        void AddPage(IPage page);
        void AddPages(IEnumerable<IPage> pages);
        void SetPage(IPage page);
        void RemovePage(IPage page);
    }
}
