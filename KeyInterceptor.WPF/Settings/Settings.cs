using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KeyInterceptor.WPF.Settings
{
    abstract class Settings<T>
    {
        protected const char Separator = '|';
        private string _fileName;
        protected abstract string[] ColumnHeaders { get; }

        public Settings(string fileName)
        {
            _fileName = fileName;

            if (!File.Exists(_fileName))
            {
                CreateNew(_fileName);
            }
        }

        private void CreateNew(string fileName)
        {
            File.WriteAllLines(fileName, new[]
            {
                string.Join(Separator.ToString(), ColumnHeaders)
            });
        }

        public IEnumerable<T> Read()
        { 
            return File.ReadAllLines(_fileName).Skip(1).Select(ParseEntry);
        }

        protected abstract T ParseEntry(string line);

        public void Write(IEnumerable<T> data)
        {
            if (File.Exists(_fileName))
                File.Delete(_fileName);

            CreateNew(_fileName);

            File.AppendAllLines(_fileName, data.Select(ToLine));
        }

        protected abstract string ToLine(T entry);
    }
}
