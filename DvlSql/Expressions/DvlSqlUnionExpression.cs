using System;
using System.Collections;
using System.Collections.Generic;

namespace DvlSql.Expressions
{
    public class DvlSqlUnionExpression : DvlSqlExpression, IList<(DvlSqlFullSelectExpression Expression, UnionType? Type)>
    {
        private readonly List<(DvlSqlFullSelectExpression expression, UnionType? type)> _selectExpressions = 
            new List<(DvlSqlFullSelectExpression, UnionType?)>();

        public override void Accept(ISqlExpressionVisitor visitor) => visitor.Visit(this);
        public override DvlSqlExpression Clone()
        {
            throw new NotImplementedException();
        }

        IEnumerator<(DvlSqlFullSelectExpression, UnionType?)> IEnumerable<(DvlSqlFullSelectExpression Expression, UnionType? Type)>.GetEnumerator() => 
            new UnionExpressionEnumerator(this._selectExpressions);

        public IEnumerator<(DvlSqlFullSelectExpression Expression, UnionType? Type)> GetEnumerator() => 
            new UnionExpressionEnumerator(this._selectExpressions);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void Add((DvlSqlFullSelectExpression, UnionType?) item) => 
            this._selectExpressions.Add(item);
        
        public void Add(DvlSqlFullSelectExpression item) => 
            this._selectExpressions.Add((item, default));

        public void Clear() => this._selectExpressions.Clear();

        public bool Contains((DvlSqlFullSelectExpression, UnionType?) item) => 
            this._selectExpressions.Contains(item);

        public void CopyTo((DvlSqlFullSelectExpression, UnionType?)[] array, int arrayIndex) => 
            this._selectExpressions.CopyTo(array, arrayIndex);

        public bool Remove((DvlSqlFullSelectExpression, UnionType?) item) =>
            this._selectExpressions.Remove(item);

        public int Count => this._selectExpressions.Count;

        public bool IsReadOnly => false;

        public int IndexOf((DvlSqlFullSelectExpression, UnionType?) item) =>
            this._selectExpressions.IndexOf(item);

        public void Insert(int index, (DvlSqlFullSelectExpression, UnionType?) item) =>
            this._selectExpressions.Insert(index, item);

        public void RemoveAt(int index) =>
            this._selectExpressions.RemoveAt(index);

        public (DvlSqlFullSelectExpression Expression, UnionType? Type) this[int index]
        {
            get => this._selectExpressions[index];
            set => this._selectExpressions[index] = value;
        }
    }

    public class UnionExpressionEnumerator(List<(DvlSqlFullSelectExpression, UnionType?)> selectExpressions) : IEnumerator<(DvlSqlFullSelectExpression Expression, UnionType? Type)>
    {
        private readonly List<(DvlSqlFullSelectExpression, UnionType?)> _selectExpressions = selectExpressions;
        private int _position = -1;

        public bool MoveNext()
        {
            this._position++;
            return this._position < _selectExpressions.Count;
        }

        public void Reset() => this._position = -1;

        public (DvlSqlFullSelectExpression Expression, UnionType? Type) Current
        {
            get
            {
                try
                {
                    return this._selectExpressions[this._position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() => Reset();
    }

    public enum UnionType
    {
        Union,
        UnionAll
    }
}