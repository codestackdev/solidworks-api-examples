using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;

namespace CodeStack.FaceIndexer
{
    /// <summary>
    /// Face Indexer API Callback
    /// </summary>
    [ComVisible(true)]
    public interface IFaceIndexerCallback
    {
        /// <summary>
        /// Invoked when in-process indexing is completed
        /// </summary>
        /// <param name="assm">Assembly with indexed faces</param>
        /// <param name="count">Count of indexed faces</param>
        void IndexFacesCompleted(IAssemblyDoc assm, int count);
    }

    /// <summary>
    /// Face Indexer API Definition
    /// </summary>
    [ComVisible(true)]
    public interface IFaceIndexerAddIn
    {
        /// <summary>
        /// Puts the face indexing request into the processing queue
        /// </summary>
        /// <param name="assm">Assembly to index faces</param>
        /// <param name="callback">Callback to call once faces are indexed</param>
        /// <remarks>The faces are indexed in-process</remarks>
        void BeginIndexFaces(IAssemblyDoc assm, IFaceIndexerCallback callback);

        /// <summary>
        /// Index faces out-of-process
        /// </summary>
        /// <param name="assm">Assembly to index faces</param>
        /// <returns>Count of indexed faces</returns>
        /// <remarks>Faces are indexed out-of-process</remarks>
        int IndexFaces(IAssemblyDoc assm);
    }
}
