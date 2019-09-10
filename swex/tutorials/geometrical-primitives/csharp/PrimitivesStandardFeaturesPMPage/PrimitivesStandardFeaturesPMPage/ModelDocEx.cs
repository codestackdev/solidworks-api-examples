//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using SolidWorks.Interop.swconst;
using System;
using System.Runtime.InteropServices;

namespace SolidWorks.Interop.sldworks
{
    public static class ModelDocEx
    {
        public static void CreateBox(this IModelDoc2 model, IEntity reference, double width, double length, double height)
        {
            CreateExtrudedPrimitive(model, reference, skMgr => skMgr.CreateCenterRectangle(0, 0, 0, width / 2, height / 2, 0) != null, height);
        }

        public static void CreateCylinder(this IModelDoc2 model, IEntity reference, double diam, double height)
        {
            CreateExtrudedPrimitive(model, reference, skMgr => skMgr.CreateCircleByRadius(0, 0, 0, diam / 2) != null, height);
        }

        private static void CreateExtrudedPrimitive(IModelDoc2 model, IEntity reference, Func<ISketchManager, bool> creator, double height)
        {
            try
            {
                if (!reference.SelectByMark(false, 0))
                {
                    throw new Exception("Failed to select reference");
                }

                model.IActiveView.EnableGraphicsUpdate = false;
                model.FeatureManager.EnableFeatureTree = false;

                var sketch = CreateSketch(model, creator);

                ExtrudeSketch(model, sketch, height);
            }
            finally
            {
                model.IActiveView.EnableGraphicsUpdate = true;
                model.FeatureManager.EnableFeatureTree = true;
            }
        }

        private static Sketch CreateSketch(IModelDoc2 model, Func<ISketchManager, bool> creator)
        {
            if (model == null)
            {
                throw new NullReferenceException("Model is null");
            }

            var skMgr = model.SketchManager;
            
            skMgr.InsertSketch(true);
            skMgr.AddToDB = true;

            var sketch = skMgr.ActiveSketch;

            if (sketch == null)
            {
                throw new NullReferenceException("Sketch is null");
            }

            if (!creator.Invoke(skMgr))
            {
                throw new NullReferenceException("Failed to create rectangle");
            }

            skMgr.AddToDB = false;
            skMgr.InsertSketch(true);

            return sketch;
        }

        private static void ExtrudeSketch(IModelDoc2 model, Sketch sketch, double height)
        {
            if ((sketch as IFeature).Select2(false, 0))
            {
                var feat = model.FeatureManager.FeatureExtrusion3(true, false, false,
                    (int)swEndConditions_e.swEndCondBlind, (int)(int)swEndConditions_e.swEndCondBlind,
                    height, 0, false, false, false, false, 0, 0, false, false, false, false, false, false, false,
                    (int)swStartConditions_e.swStartSketchPlane, 0, false);

                if (feat == null)
                {
                    throw new NullReferenceException("Failed to create extrusion feature");
                }
            }
            else
            {
                throw new Exception("Failed to select sketch");
            }
        }
    }
}
