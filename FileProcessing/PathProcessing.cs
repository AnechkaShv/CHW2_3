using System.Runtime.CompilerServices;
using System;
using System.IO;

namespace FileProcessing
{
    public class PathProcessing
    {
        char[] invalidPathChars = Path.GetInvalidPathChars();
        private string? _fPath;
        private string? _nPath;
        private bool _isExist;
        public bool IsExist{ get { return _isExist; } set { _isExist = value; } }
        /// <summary>
        /// This property sets and checks initial path value.
        /// </summary>
        public string FPath
        {
            get
            {
                // Checking path and return its value.
                if (CheckFPath(_fPath))
                    return _fPath;
                else
                    throw new ArgumentException();
            }
            set
            {
                // Checking path and set value
                if (CheckFPath(value))
                    _fPath = value;
                else
                    throw new ArgumentException();
            }
        }
        /// <summary>
        /// This property sets and checks new path value.
        /// </summary>
        public string NPath
        {
            get
            {
                // Checking path and return its value.
                if (CheckNPath(_nPath, ref _isExist))
                    return _nPath;
                else
                    throw new ArgumentException();
            }
            set
            {
                // Checking path and set value
                if (CheckNPath(value, ref _isExist))
                    _nPath = value;
                else
                    throw new ArgumentException();
            }
        }
        public PathProcessing() { }
        public PathProcessing(string fPath)
        {
            FPath = fPath;
        }
        public PathProcessing(string fPath, string nPath)
        {
            FPath = fPath;
            NPath = nPath;
        }
        /// <summary>
        /// This method checks initial path and throws exception if it is incorrect.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool CheckFPath(string? path)
        {
            // Allowing files with only csv extension.
            if (path is null || path.Length<=0 || !File.Exists(path) || path.IndexOfAny(invalidPathChars) != -1 || path[^4..] != ".csv")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        ///<summary>
        /// This method checks new path and throws exception if it is incorrect.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool CheckNPath(string? path, ref bool isExist)
        {
            if(path == null || path.IndexOfAny(invalidPathChars) != -1 || path.Length <= 0 || path == " " || path[^4..] != ".csv")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}