using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSandbox.App.Models;

namespace TestSandbox.App.Services
{
    public interface ICloud
    {
        RawVideo[] GetRawVideos(int amount, DateTime from, DateTime to);
    }

    public class Cloud
    {

    }
}
