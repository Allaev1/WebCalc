using DnetIndexedDb;
using Microsoft.JSInterop;
using WebCalc.IndexedDbStorage.Constant.Model;

namespace WebCalc.IndexedDbStorage.Data
{
    public class WebCalcDb : IndexedDbInterop
    {
        public WebCalcDb(IJSRuntime jSRuntime, IndexedDbOptions<WebCalcDb> options) : base(jSRuntime, options)
        {
        }
    }
}