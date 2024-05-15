﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Reteno
{
    // Forked from  https://github.com/Jackyjjc/MiniJSON.cs
    // version: 6de00beb134bbab9d873033a48b32e4067ed0c25

    // Example usage:
    //
    //  using UnityEngine;
    //  using System.Collections;
    //  using System.Collections.Generic;
    //  using MiniJSON;
    //
    //  public class MiniJSONTest : MonoBehaviour {
    //      void Start () {
    //          var jsonString = "{ \"array\": [1.44,2,3], " +
    //                          "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
    //                          "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
    //                          "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
    //                          "\"int\": 65536, " +
    //                          "\"float\": 3.1415926, " +
    //                          "\"bool\": true, " +
    //                          "\"null\": null }";
    //
    //          var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
    //
    //          Debug.Log("deserialized: " + dict.GetType());
    //          Debug.Log("dict['array'][0]: " + ((List<object>) dict["array"])[0]);
    //          Debug.Log("dict['string']: " + (string) dict["string"]);
    //          Debug.Log("dict['float']: " + (double) dict["float"]); // floats come out as doubles
    //          Debug.Log("dict['int']: " + (long) dict["int"]); // ints come out as longs
    //          Debug.Log("dict['unicode']: " + (string) dict["unicode"]);
    //
    //          var str = Json.Serialize(dict);
    //
    //          Debug.Log("serialized: " + str);
    //      }
    //  }

    /// <summary>
    ///     This class encodes and decodes JSON strings.
    ///     Spec. details, see http://www.json.org/
    ///     JSON uses Arrays and Objects. These correspond here to the datatypes IList and IDictionary.
    ///     All numbers are parsed to doubles.
    /// </summary>
    public static class Json
    {
        /// <summary>
        ///     Parses the string json into a value
        /// </summary>
        /// <param name="json">A JSON string.</param>
        /// <returns>An List&lt;object&gt;, a Dictionary&lt;string, object&gt;, a double, an integer,a string, null, true, or false</returns>
        public static object Deserialize(string json)
        {
            // save the string for debug information
            if (json == null) return null;

            return Parser.Parse(json);
        }

        /// <summary>
        ///     Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
        /// </summary>
        /// <param name="json">A Dictionary&lt;string, object&gt; / List&lt;object&gt;</param>
        /// <param name="humanReadable">
        ///     Whether output as human readable format with spaces and
        ///     indentations.
        /// </param>
        /// <param name="indentSpaces">Number of spaces for each level of indentation.</param>
        /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
        public static string Serialize(object obj,
            bool humanReadable = false,
            int indentSpaces = 2)
        {
            return Serializer.MakeSerialization(obj, humanReadable, indentSpaces);
        }

        private sealed class Parser : IDisposable
        {
            private const string WORD_BREAK = "{}[],:\"";

            private StringReader json;

            private Parser(string jsonString)
            {
                json = new StringReader(jsonString);
            }

            private char PeekChar => Convert.ToChar(json.Peek());

            private char NextChar => Convert.ToChar(json.Read());

            private string NextWord
            {
                get
                {
                    var word = new StringBuilder();

                    while (!IsWordBreak(PeekChar))
                    {
                        word.Append(NextChar);

                        if (json.Peek() == -1) break;
                    }

                    return word.ToString();
                }
            }

            private TOKEN NextToken
            {
                get
                {
                    EatWhitespace();

                    if (json.Peek() == -1) return TOKEN.NONE;

                    switch (PeekChar)
                    {
                        case '{':
                            return TOKEN.CURLY_OPEN;
                        case '}':
                            json.Read();
                            return TOKEN.CURLY_CLOSE;
                        case '[':
                            return TOKEN.SQUARED_OPEN;
                        case ']':
                            json.Read();
                            return TOKEN.SQUARED_CLOSE;
                        case ',':
                            json.Read();
                            return TOKEN.COMMA;
                        case '"':
                            return TOKEN.STRING;
                        case ':':
                            return TOKEN.COLON;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case '-':
                            return TOKEN.NUMBER;
                        default:
                            switch (NextWord)
                            {
                                case "false":
                                    return TOKEN.FALSE;
                                case "true":
                                    return TOKEN.TRUE;
                                case "null":
                                    return TOKEN.NULL;
                                default:
                                    return TOKEN.NONE;
                            }
                    }
                }
            }

            public void Dispose()
            {
                json.Dispose();
                json = null;
            }

            public static bool IsWordBreak(char c)
            {
                return char.IsWhiteSpace(c) || WORD_BREAK.IndexOf(c) != -1;
            }

            public static object Parse(string jsonString)
            {
                using (var instance = new Parser(jsonString))
                {
                    return instance.ParseValue();
                }
            }

            private Dictionary<string, object> ParseObject()
            {
                var table = new Dictionary<string, object>();

                // ditch opening brace
                json.Read();

                while (true)
                    switch (NextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.CURLY_CLOSE:
                            return table;
                        default:
                            // name
                            var name = ParseString();
                            if (name == null) return null;

                            // :
                            if (NextToken != TOKEN.COLON) return null;

                            // ditch the colon
                            json.Read();

                            // value
                            table[name] = ParseValue();
                            break;
                    }
            }

            private List<object> ParseArray()
            {
                var array = new List<object>();

                // ditch opening bracket
                json.Read();

                // [
                var parsing = true;
                while (parsing)
                {
                    var nextToken = NextToken;

                    switch (nextToken)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.SQUARED_CLOSE:
                            parsing = false;
                            break;
                        default:
                            var value = ParseByToken(nextToken);

                            array.Add(value);
                            break;
                    }
                }

                return array;
            }

            private object ParseValue()
            {
                var nextToken = NextToken;
                return ParseByToken(nextToken);
            }

            private object ParseByToken(TOKEN token)
            {
                switch (token)
                {
                    case TOKEN.STRING:
                        return ParseString();
                    case TOKEN.NUMBER:
                        return ParseNumber();
                    case TOKEN.CURLY_OPEN:
                        return ParseObject();
                    case TOKEN.SQUARED_OPEN:
                        return ParseArray();
                    case TOKEN.TRUE:
                        return true;
                    case TOKEN.FALSE:
                        return false;
                    case TOKEN.NULL:
                        return null;
                    default:
                        return null;
                }
            }

            private string ParseString()
            {
                var s = new StringBuilder();
                char c;

                // ditch opening quote
                json.Read();

                var parsing = true;
                while (parsing)
                {
                    if (json.Peek() == -1) break;

                    c = NextChar;
                    switch (c)
                    {
                        case '"':
                            parsing = false;
                            break;
                        case '\\':
                            if (json.Peek() == -1)
                            {
                                parsing = false;
                                break;
                            }

                            c = NextChar;
                            switch (c)
                            {
                                case '"':
                                case '\\':
                                case '/':
                                    s.Append(c);
                                    break;
                                case 'b':
                                    s.Append('\b');
                                    break;
                                case 'f':
                                    s.Append('\f');
                                    break;
                                case 'n':
                                    s.Append('\n');
                                    break;
                                case 'r':
                                    s.Append('\r');
                                    break;
                                case 't':
                                    s.Append('\t');
                                    break;
                                case 'u':
                                    var hex = new char[4];

                                    for (var i = 0; i < 4; i++) hex[i] = NextChar;

                                    s.Append((char)Convert.ToInt32(new string(hex), 16));
                                    break;
                                default:
                                    s.Append(c);
                                    break;
                            }

                            break;
                        default:
                            s.Append(c);
                            break;
                    }
                }

                return s.ToString();
            }

            private object ParseNumber()
            {
                var number = NextWord;

                if (number.IndexOf('.') == -1 && number.IndexOf('E') == -1 && number.IndexOf('e') == -1)
                {
                    long parsedInt;
                    long.TryParse(number, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out parsedInt);
                    return parsedInt;
                }

                double parsedDouble;
                double.TryParse(number, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out parsedDouble);
                return parsedDouble;
            }

            private void EatWhitespace()
            {
                while (char.IsWhiteSpace(PeekChar))
                {
                    json.Read();

                    if (json.Peek() == -1) break;
                }
            }

            private enum TOKEN
            {
                NONE,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            }
        }

        private sealed class Serializer
        {
            private readonly StringBuilder builder;
            private readonly bool humanReadable;
            private readonly int indentSpaces;
            private int indentLevel;

            private Serializer(bool humanReadable, int indentSpaces)
            {
                builder = new StringBuilder();
                this.humanReadable = humanReadable;
                this.indentSpaces = indentSpaces;
                indentLevel = 0;
            }

            public static string MakeSerialization(object obj, bool humanReadable, int indentSpaces)
            {
                var instance = new Serializer(humanReadable, indentSpaces);

                instance.SerializeValue(obj);

                return instance.builder.ToString();
            }

            private void SerializeValue(object value)
            {
                IList asList;
                IDictionary asDict;
                string asStr;

                if (value == null)
                    builder.Append("null");
                else if ((asStr = value as string) != null)
                    SerializeString(asStr);
                else if (value is bool)
                    builder.Append((bool)value ? "true" : "false");
                else if ((asList = value as IList) != null)
                    SerializeArray(asList);
                else if ((asDict = value as IDictionary) != null)
                    SerializeObject(asDict);
                else if (value is char)
                    SerializeString(new string((char)value, 1));
                else
                    SerializeOther(value);
            }

            private void AppendNewLineFunc()
            {
                builder.AppendLine();
                builder.Append(' ', indentSpaces * indentLevel);
            }

            private void SerializeObject(IDictionary obj)
            {
                var first = true;

                builder.Append('{');
                ++indentLevel;

                foreach (var e in obj.Keys)
                {
                    if (first)
                    {
                        if (humanReadable) AppendNewLineFunc();
                    }
                    else
                    {
                        builder.Append(',');
                        if (humanReadable) AppendNewLineFunc();
                    }

                    SerializeString(e.ToString());
                    builder.Append(':');
                    if (humanReadable) builder.Append(' ');

                    SerializeValue(obj[e]);

                    first = false;
                }

                --indentLevel;
                if (humanReadable && obj.Count > 0) AppendNewLineFunc();

                builder.Append('}');
            }

            private void SerializeArray(IList anArray)
            {
                builder.Append('[');
                ++indentLevel;

                var first = true;

                for (var i = 0; i < anArray.Count; i++)
                {
                    var obj = anArray[i];
                    if (first)
                    {
                        if (humanReadable) AppendNewLineFunc();
                    }
                    else
                    {
                        builder.Append(',');
                        if (humanReadable) AppendNewLineFunc();
                    }

                    SerializeValue(obj);

                    first = false;
                }

                --indentLevel;
                if (humanReadable && anArray.Count > 0) AppendNewLineFunc();

                builder.Append(']');
            }

            private void SerializeString(string str)
            {
                builder.Append('\"');

                var charArray = str.ToCharArray();
                for (var i = 0; i < charArray.Length; i++)
                {
                    var c = charArray[i];
                    switch (c)
                    {
                        case '"':
                            builder.Append("\\\"");
                            break;
                        case '\\':
                            builder.Append("\\\\");
                            break;
                        case '\b':
                            builder.Append("\\b");
                            break;
                        case '\f':
                            builder.Append("\\f");
                            break;
                        case '\n':
                            builder.Append("\\n");
                            break;
                        case '\r':
                            builder.Append("\\r");
                            break;
                        case '\t':
                            builder.Append("\\t");
                            break;
                        default:
                            var codepoint = Convert.ToInt32(c);
                            if (codepoint >= 32 && codepoint <= 126)
                            {
                                builder.Append(c);
                            }
                            else
                            {
                                builder.Append("\\u");
                                builder.Append(codepoint.ToString("x4"));
                            }

                            break;
                    }
                }

                builder.Append('\"');
            }

            private void SerializeOther(object value)
            {
                // NOTE: decimals lose precision during serialization.
                // They always have, I'm just letting you know.
                // Previously floats and doubles lost precision too.
                if (value is float)
                    builder.Append(((float)value).ToString("R", CultureInfo.InvariantCulture));
                else if (value is int
                         || value is uint
                         || value is long
                         || value is sbyte
                         || value is byte
                         || value is short
                         || value is ushort
                         || value is ulong)
                    builder.Append(value);
                else if (value is double
                         || value is decimal)
                    builder.Append(Convert.ToDouble(value)
                        .ToString("R", CultureInfo.InvariantCulture));
                else
                    SerializeString(value.ToString());
            }
        }
    }
}