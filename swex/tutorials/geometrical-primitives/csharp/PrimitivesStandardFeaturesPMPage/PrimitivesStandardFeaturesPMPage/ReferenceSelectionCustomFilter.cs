//**********************
//SwEx - development tools for SOLIDWORKS
//Copyright(C) 2019 www.codestack.net
//License: https://github.com/codestackdev/swex-common/blob/master/LICENSE
//Product URL: https://www.codestack.net/labs/solidworks/swex
//**********************

using CodeStack.SwEx.PMPage.Base;
using CodeStack.SwEx.PMPage.Controls;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace CodeStack.PrimitivesStandardFeaturesPMPage
{
    public class ReferenceSelectionCustomFilter : SelectionCustomFilter<IEntity>
    {
        protected override bool Filter(IPropertyManagerPageControlEx selBox, IEntity selection, swSelectType_e selType, ref string itemText)
        {
            if (selType == swSelectType_e.swSelFACES)
            {
                if (selType == swSelectType_e.swSelFACES)
                {
                    var face = selection as IFace2;

                    if (!face.IGetSurface().IsPlane())
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
