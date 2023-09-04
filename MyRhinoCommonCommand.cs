using Eto.Forms.ThemedControls;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;

namespace MyRhinoPlugin1
{
    public class MyRhinoCommonCommand : Command
    {
        public MyRhinoCommonCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static MyRhinoCommonCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "MyRhinoCommonCommand";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            RhinoApp.WriteLine("The {0} command will add a rectangular surface.", EnglishName);

            double length, width, x, y, z;

            // Get user input for length
            var getLength = new GetNumber();
            getLength.SetCommandPrompt("Enter length:");
            getLength.Get();
            if (getLength.CommandResult() != Result.Success)
                return getLength.CommandResult();
            length = getLength.Number();

            // Get user input for width
            var getWidth = new GetNumber();
            getWidth.SetCommandPrompt("Enter width:");
            getWidth.Get();
            if (getWidth.CommandResult() != Result.Success)
                return getWidth.CommandResult();
            width = getWidth.Number();

            // Get user input for X coordinate
            var getX = new GetNumber();
            getX.SetCommandPrompt("Enter X coordinate:");
            getX.Get();
            if (getX.CommandResult() != Result.Success)
                return getX.CommandResult();
            x = getX.Number();

            // Get user input for Y coordinate
            var getY = new GetNumber();
            getY.SetCommandPrompt("Enter Y coordinate:");
            getY.Get();
            if (getY.CommandResult() != Result.Success)
                return getY.CommandResult();
            y = getY.Number();

            // Get user input for Z coordinate
            var getZ = new GetNumber();
            getZ.SetCommandPrompt("Enter Z coordinate:");
            getZ.Get();
            if (getZ.CommandResult() != Result.Success)
                return getZ.CommandResult();
            z = getZ.Number();

            Point3d basePoint = new Point3d(x, y, z);
            Plane plane = new Plane(basePoint, Vector3d.ZAxis);

            Point3d corner0 = plane.PointAt(-length / 2, -width / 2);
            Point3d corner1 = plane.PointAt(length / 2, -width / 2);
            Point3d corner2 = plane.PointAt(length / 2, width / 2);
            Point3d corner3 = plane.PointAt(-length / 2, width / 2);

            NurbsSurface rectangleSurface = NurbsSurface.CreateFromCorners(corner0, corner1, corner2, corner3);

            Brep surface = Brep.CreateFromSurface(rectangleSurface);

            doc.Objects.AddBrep(surface);
            doc.Views.Redraw();

            RhinoApp.WriteLine("The {0} command added a rectangular surface to the document.", EnglishName);

            return Result.Success;
        }
    }
}
