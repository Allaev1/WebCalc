using Blazor.IndexedDB.WebAssembly;
using Microsoft.JSInterop;
using WebCalc.IndexedDbStorage.Constant.Model;

namespace WebCalc.IndexedDbStorage.Data
{
    public class WebCalcDb : IndexedDb
    {
        public WebCalcDb(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version)
        {
        }

        public IndexedSet<ConstantModel> Constants { get; set; }
    }
}