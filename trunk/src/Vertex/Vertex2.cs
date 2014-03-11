using System;

namespace owmapNET.Vertex
{
    public struct Vertex2<T>
    {
        public T X;
        public T Y;

        public override string ToString()
        {
            return string.Format("[Vertex2<{0}>: X = {1}, Y = {2}]", typeof(T), X, Y);
        }
    }
}
