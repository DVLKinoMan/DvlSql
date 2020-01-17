using System;
using System.Data;
using Dvl_Sql.Models;

namespace Dvl_Sql.Extensions.Types
{
    public static partial class TypeHelpers
    {
        public static DvlSqlType<string> NVarChar(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.NVarChar, size);

        public static DvlSqlType NVarCharType(string name, int size) =>
            new DvlSqlType(name, SqlDbType.NVarChar, size);

        public static DvlSqlType<string> NVarCharMax(string name, string value) =>
            NVarChar(name, value, -1);

        public static DvlSqlType<string> NVarCharMax(string value) =>
            new DvlSqlType<string>(value, SqlDbType.NVarChar);

        public static DvlSqlType NVarCharMaxType(string name) =>
            new DvlSqlType(name, SqlDbType.NVarChar);

        public static DvlSqlType<string> VarChar(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.VarChar, size);

        public static DvlSqlType VarCharType(string name, int size) =>
            new DvlSqlType(name, SqlDbType.VarChar, size);

        public static DvlSqlType<string> VarChar(string value, int size) =>
            new DvlSqlType<string>(value, SqlDbType.VarChar, size);

        public static DvlSqlType<string> VarCharMax(string value) =>
            VarChar(value, -1);

        public static DvlSqlType VarCharMaxType(string name) =>
            VarCharType(name, -1);

        public static DvlSqlType<string> VarCharMax(string name, string value) =>
            VarChar(name, value, -1);

        public static DvlSqlType<string> Char(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.Char, size);

        public static DvlSqlType CharType(string name, int size) =>
            new DvlSqlType(name, SqlDbType.Char, size);

        public static DvlSqlType<string> Char(string value, int size) =>
            new DvlSqlType<string>(value, SqlDbType.Char, size);

        public static DvlSqlType<string> CharMax(string name, string value) =>
            Char(name, value, -1);

        public static DvlSqlType<string> CharMax(string value) =>
            Char(value, -1);

        public static DvlSqlType CharMaxType(string name) =>
            Char(name, -1);

        public static DvlSqlType<string> NChar(string name, string value, int size) =>
            new DvlSqlType<string>(name, value, SqlDbType.NChar, size);

        public static DvlSqlType<string> NChar(string value, int size) =>
            new DvlSqlType<string>(value, SqlDbType.NChar, size);

        public static DvlSqlType NCharType(string name, int size) =>
            new DvlSqlType(name, SqlDbType.NChar, size);

        public static DvlSqlType<string> NCharMax(string name, string value) =>
            NChar(name, value, -1);

        public static DvlSqlType<string> NCharMax(string value) =>
            NChar(value, -1);

        public static DvlSqlType NCharMaxType(string name) =>
            NCharType(name, -1);

        public static DvlSqlType<string> Text(string name, string value) => value.Length <= Math.Pow(2, 31) - 1
            ? new DvlSqlType<string>(name, value, SqlDbType.Text)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType TextType(string name) =>
            new DvlSqlType(name, SqlDbType.Text);

        public static DvlSqlType<string> Text(string value) => value.Length <= Math.Pow(2, 31) - 1
            ? new DvlSqlType<string>(value, SqlDbType.Text)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType<string> NText(string name, string value) => value.Length <= Math.Pow(2, 30) - 1
            ? new DvlSqlType<string>(name, value, SqlDbType.NText)
            : throw new ArgumentException("value length not allowed");

        public static DvlSqlType NTextType(string name) =>
            new DvlSqlType(name, SqlDbType.NText);

        public static DvlSqlType<string> NText(string value) => value.Length <= Math.Pow(2, 30) - 1
            ? new DvlSqlType<string>(value, SqlDbType.NText)
            : throw new ArgumentException("value length not allowed");
    }
}
