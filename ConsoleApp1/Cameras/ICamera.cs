using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Cameras
{
    public interface ICamera
    {
        Matrix4 Projection { get; }
        Matrix4 View { get; }
    }
}
