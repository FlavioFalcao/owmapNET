using owmapNET.Vertex;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;

namespace owmapNET
{
    public class Service
    {
        public string AppId
        {
            get { return AppId; }
            set { appId = string.IsNullOrEmpty(value) ? "undefined" : value; }
        }
        private string appId = "undefined";

        /// <summary>
        /// The longitude/latitude boundaries of the region to monitor weather map
        /// </summary>
        public Vertex2<float>[] Bounds
        {
            get { return (Vertex2<float>[])bounds.Clone(); }
        }
        private Vertex2<float>[] bounds;

        /// <summary>
        /// Where the cached, pieced-together region bitmap is stored.
        /// </summary>
        public string CachedFile
        {
            get { return cachedFile; }
        }
        private string cachedFile;

        // TODO: What am I monitoring?! Precipitation? Cloud cover? Snow? Pressure?

        /// <summary>
        /// Update Interval, in minutes. Minimum = 1, Default = 10
        /// </summary>
        public int UpdateInterval
        {
            get { return updateInterval; }
            set { updateInterval = value < 1 ? 1 : value; }
        }
        private int updateInterval = 10;

        // we want to start a service, complete with a timer, that queries a series of tiles
        // from open weather map, sews together into a single bitmap,
        // stores the single bitmap in cache,
        // and defers to the cache if it's less than 10 minutes old

        // need to figure out the appropriate zoom level based on the coverage area/bounds
        
        public Service(Vertex2<float>[] bounds, string cachedFile)
        {
            if (bounds == null || bounds.Length < 3)
            {
                throw new ArgumentException("bounds cannot be null and must have at least three lat/lon values");
            }

            this.bounds = (Vertex2<float>[])bounds.Clone();

            // verify that the file can be opened for writing
            using (File.OpenWrite(cachedFile)) {}

            this.cachedFile = cachedFile;

            // snag the current thread's context
            context = SynchronizationContext.Current;
        }

        /// <summary>
        /// Returns the pixel color for the provided longitude/latitude.
        /// </summary>
        public Color GetColor(Vertex2<float> lon, Vertex2<float> lat) { return Color.Black; }

        #region Private Members

        // holds the synchronization context of the thread that creates
        // this object so that merging can be marshaled back to it later
        private readonly SynchronizationContext context;

        #endregion
    }
}
