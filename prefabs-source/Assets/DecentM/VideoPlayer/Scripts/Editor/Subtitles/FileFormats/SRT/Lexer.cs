using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DecentM.Subtitles.Editor
{
    public enum SrtToken
    {
        Unknown,
        Char,
        Number,
        Arrow,
        Colon,
        Comma,
        Newline,
        Space,
    }

    public class SrtLexer : Lexer<SrtToken>
    {
        public override List<Token> Lex(string text)
        {
            int cursor = 0;
            List<Token> tokens = new List<Token>();

            void AddToken(SrtToken type, object value)
            {
                Token token = new Token(type, value);
                tokens.Add(token);
                cursor++;
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

            while (cursor < text.Length)
            {
                char current = text[cursor];

                if (current == '-' && FindWord("-->"))
                {
                    AddToken(SrtToken.Arrow, "-->");
                    cursor += 2;
                    continue;
                }

                if (current == ':')
                {
                    AddToken(SrtToken.Colon, ":");
                    continue;
                }

                if (current == ',')
                {
                    AddToken(SrtToken.Comma, ",");
                    continue;
                }

                if (current == '\n')
                {
                    AddToken(SrtToken.Newline, "\n");
                    continue;
                }

                if (current == ' ')
                {
                    AddToken(SrtToken.Space, " ");
                    continue;
                }

                int intValue;
                if (int.TryParse(current.ToString(), out intValue) == true)
                {
                    Token intToken = new Token(SrtToken.Number, intValue);
                    tokens.Add(intToken);
                    cursor++;
                    continue;
                }

                // If we're down here, that means we have a char, so we just treat it as part of
                // the subtitle text
                Token token = new Token(SrtToken.Char, current.ToString());
                tokens.Add(token);
                cursor++;
                continue;
            }

            return tokens;
        }
    }
}
