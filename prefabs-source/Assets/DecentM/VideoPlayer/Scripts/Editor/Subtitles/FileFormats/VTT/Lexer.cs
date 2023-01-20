﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DecentM.Subtitles.Editor
{
    public enum VttToken
    {
        Unknown,
        Char,
        Number,
        Arrow,
        Colon,
        Comma,
        Newline,
        Space,
        WEBVTTHeader,
        Note,
        DoubleNewline,
        Style,
    }

    public class VttLexer : Lexer<VttToken>
    {
        public override List<Token> Lex(string text)
        {
            int cursor = 0;
            List<Token> tokens = new List<Token>();

            void AddToken(VttToken type, object value)
            {
                Token token = new Token(type, value);
                tokens.Add(token);
                cursor++;
            }

            string ConsumeUntilDoubleNewline(int skip = 0)
            {
                string result = "";
                cursor += skip;

                bool lastCharWasNewline = false;
                while (cursor < text.Length)
                {
                    result += text[cursor];
                    cursor++;

                    if (text[cursor] == '\n' && lastCharWasNewline)
                        break;
                    if (text[cursor] == '\n')
                        lastCharWasNewline = true;
                    else
                        lastCharWasNewline = false;
                }

                return result;
            }

            bool FindWord(string word)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (cursor + i >= text.Length)
                        return false;
                    if (word[i] != text[cursor + i])
                        return false;
                }

                return true;
            }

            bool CheckNextCharIsNewline()
            {
                if (cursor >= text.Length - 1)
                    return false;

                bool nextIsCR = text[cursor + 1] == '\r';
                return text[cursor + (nextIsCR ? 2 : 1)] == '\n';
            }

            bool CheckPreviousCharIsNewline()
            {
                if (cursor == 0)
                    return false;

                bool nextIsCR = text[cursor - 1] == '\r';
                return text[cursor - (nextIsCR ? 2 : 1)] == '\n';
            }

            while (cursor < text.Length)
            {
                char current = text[cursor];

                string noteWord = "NOTE";
                if (FindWord(noteWord))
                {
                    string noteValue = ConsumeUntilDoubleNewline(noteWord.Length);
                    AddToken(VttToken.Note, noteValue);
                    AddToken(VttToken.DoubleNewline, "\n\n");
                    cursor--;
                    continue;
                }

                string webvttWord = "WEBVTT";
                if (FindWord(webvttWord))
                {
                    string value = ConsumeUntilDoubleNewline(webvttWord.Length);
                    AddToken(VttToken.WEBVTTHeader, value);
                    AddToken(VttToken.DoubleNewline, "\n\n");
                    cursor--;
                    continue;
                }

                string styleWord = "STYLE";
                if (FindWord(styleWord))
                {
                    string value = ConsumeUntilDoubleNewline(styleWord.Length);
                    AddToken(VttToken.Style, value);
                    AddToken(VttToken.DoubleNewline, "\n\n");
                    cursor--;
                    continue;
                }

                if (current == '-' && FindWord("-->"))
                {
                    AddToken(VttToken.Arrow, "-->");
                    cursor += 2;
                    continue;
                }

                if (current == ':')
                {
                    AddToken(VttToken.Colon, ":");
                    continue;
                }

                if (current == ',')
                {
                    AddToken(VttToken.Comma, ",");
                    continue;
                }

                if (current == '\n')
                {
                    if (CheckNextCharIsNewline())
                    {
                        AddToken(VttToken.DoubleNewline, "\n\n");
                        continue;
                    }

                    if (!CheckPreviousCharIsNewline())
                    {
                        AddToken(VttToken.Newline, "\n");
                        continue;
                    }

                    cursor++;
                    continue;
                }

                if (current == ' ')
                {
                    AddToken(VttToken.Space, " ");
                    continue;
                }

                int intValue;
                if (int.TryParse(current.ToString(), out intValue) == true)
                {
                    AddToken(VttToken.Number, intValue);
                    continue;
                }

                // If we're down here, that means we have a char, so we just treat it as part of
                // the subtitle text
                AddToken(VttToken.Char, current.ToString());
                continue;
            }

            return tokens;
        }
    }
}