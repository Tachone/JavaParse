using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;

namespace Tokenizer
{
    class JavaRead
    {
        List<JavaWord> javaWords=new List<JavaWord>();
        public RichTextBox richTextBox;
        public JavaRead()
        {
            javaWords = new List<JavaWord>();
        }

        public struct JavaWord
        {
            string word;
            // 0 Comment 注释,1 Identifier 标识符,2 Keyword 关键字,3 Operator 运算符,4 Punctuator 标点,5 StringLiteral  字符串常值,6 Whitespace 空格
            int type;
            public JavaWord(string word, int type)  //词，类型
            {
                this.word = word;
                this.type = type;
            }
            public string Word
            {
                get { return word; }
                set { word = value; }
            }
            

            public int Type
            {
                get { return type; }
                set { type = value; }
            }
        }

      

        static string[] keywords = new string[50]{"abstract", "boolean", "break", "byte", "case", "catch", "char", 
       "class", "const", "continue", "default", "do", "double", "else", "extends", "false", 
       "final", "finally", "float", "for", "goto", "if", "implements", "import", "instanceof", 
       "int", "interface", "long", "native", "new", "null", "package", "private", "protected", 
       "public", "return", "short", "static", "super", "switch", "synchronized", "this", 
       "throw", "throws", "transient", "true", "try", "void", "volatile", "while"};      /*存储关键字 50个*/



        public List<JavaWord> BeginParse(string str)
        {
            StreamReader sr = new StreamReader(str, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                getwords(clearnotes(line), 0);  //去掉注释

                javaWords.Add( new JavaWord("\n", 1)); 
            }
            return javaWords;
        }


        /*******************从扫描缓冲区解析获取单词********************/
        public  void getwords(string str, int state)
        {
            str = str + '\0';
            char[] bufferscan = str.ToCharArray();
            char[] word = new char[100];
            int charCount = 0;
            int finish = 0;
            int i, j, k;
            for (i = 0; bufferscan[i] != '\0'; i++)
            {
                switch (state / 10)
                {
                    case 0:
                        switch (charkind(bufferscan[i]))
                        {
                            case 1:
                                word[charCount++] = bufferscan[i];
                                state = 10;
                                break;
                            case 2:
                                word[charCount++] = bufferscan[i];
                                state = 20;
                                break;
                            case 3:
                                word[charCount++] = bufferscan[i];
                                state = 30;
                                break;
                            case 0:
                                word[charCount++] = bufferscan[i];
                                switch (bufferscan[i])
                                {
                                    case '"':
                                        state = 41;
                                        break;
                                    case '\'':
                                        state = 42;
                                        break;
                                    case '(':
                                    case ')':
                                    case '{':
                                    case '}':
                                    case '[':
                                    case ']':
                                    case ';':
                                    case ',':
                                    case '.':
                                        state = 50;
                                        word[charCount] = '\0';
                                        finish = 1;
                                        break;
                                    case '=':
                                        state = 43;
                                        break;
                                    default:
                                        state = 40;
                                        break;
                                }
                                break;
                            default:
                                word[charCount++] = bufferscan[i]; 
                                break;
                        }
                        break;
                    case 1:
                        switch (charkind(bufferscan[i]))
                        {
                            case 1:
                                word[charCount++] = bufferscan[i];
                                state = 10;
                                break;
                            case 2:
                                word[charCount++] = bufferscan[i];
                                state = 20;
                                break;
                            case 3:
                                word[charCount++] = bufferscan[i];
                                state = 30;
                                break;
                            case 0:
                                word[charCount] = '\0';
                                i--;
                                finish = 1;
                                state = 50;
                                break;
                            default: word[charCount++] = bufferscan[i]; break;
                        }
                        break;
                    case 2:
                        switch (charkind(bufferscan[i]))
                        {
                            case 1:
                                word[charCount++] = bufferscan[i];
                                state = 20;
                                break;
                            case 2:
                                word[charCount++] = bufferscan[i];
                                state = 20;
                                break;
                            case 3:
                                word[charCount++] = bufferscan[i];
                                state = 30;
                                break;
                            case 0:
                                if (bufferscan[i] == '.')
                                {
                                    word[charCount++] = bufferscan[i];
                                    state = 20;
                                    break;
                                }
                                word[charCount] = '\0';
                                i--;
                                finish = 1;
                                state = 50;
                                break;
                            default: word[charCount++] = bufferscan[i]; break;
                        }
                        break;
                    case 3:
                        switch (charkind(bufferscan[i]))
                        {
                            case 1:
                                word[charCount++] = bufferscan[i];
                                state = 30;
                                break;
                            case 2:
                                word[charCount++] = bufferscan[i];
                                state = 30;
                                break;
                            case 3:
                                word[charCount++] = bufferscan[i];
                                state = 30;
                                break;
                            case 0:
                                word[charCount] = '\0';
                                i--;
                                finish = 1;
                                state = 50;
                                break;
                            default: word[charCount++] = bufferscan[i]; break;
                        }
                        break;
                    case 4:
                        switch (state)
                        {
                            case 40:
                                switch (charkind(bufferscan[i]))
                                {
                                    case 1:
                                        word[charCount] = '\0';
                                        i--;
                                        finish = 1;
                                        state = 50;
                                        break;
                                    case 2:
                                        word[charCount] = '\0';
                                        i--;
                                        finish = 1;
                                        state = 50;
                                        break;
                                    case 3:
                                        word[charCount] = '\0';
                                        i--;
                                        finish = 1;
                                        state = 50;
                                        break;
                                    case 0:
                                        word[charCount++] = bufferscan[i];
                                        state = 40;
                                        break;
                                    default: word[charCount++] = bufferscan[i]; break;
                                }
                                break;
                            case 41:
                                word[charCount++] = bufferscan[i];
                                if (bufferscan[i] == '"')
                                {
                                    if (charkind(bufferscan[i - 1]) == 4)
                                    {
                                    }
                                    else
                                    {
                                        word[charCount] = '\0';
                                        finish = 1;
                                        state = 50;
                                    }
                                }
                                break;
                            case 42:
                                word[charCount++] = bufferscan[i];
                                if (bufferscan[i] == '\'')
                                {
                                    word[charCount] = '\0';
                                    finish = 1;
                                    state = 50;
                                }
                                break;
                            case 43:
                                if (bufferscan[i] == '=')
                                {
                                    word[charCount++] = bufferscan[i];
                                    state = 43;
                                }
                                else
                                {
                                    word[charCount] = '\0';
                                    finish = 1;
                                    i--;
                                    state = 50;
                                }
                                break;
                            default: word[charCount++] = bufferscan[i]; break;
                        }
                        break;
                    case 5:
                        finish = 0;
                        state = 0;
                        charCount = 0;
                        i--;

                        string strResult = "";
                        for (int ii = 0; ii < word.Length; ii++)
                        {
                            if (word[ii] == '\0')
                            {
                                break;
                            }
                            else
                                strResult = strResult + word[ii];
                        }


                        wordkind(strResult);
                        break;
                    default: break;
                }
                if (bufferscan[i + 1] == '\0')
                {
                    word[charCount] = '\0';
      


                    string strResult = "";
                    for (int ii = 0; ii < word.Length; ii++)
                    {
                        if (word[ii] == '\0')
                        {
                            break;
                        }
                        else
                            strResult = strResult + word[ii];
                    }


                    wordkind(strResult);
                }
            }
        }





        /*******************过滤注释及多余空格*******************/
        public string clearnotes(string str)
        {
            str = str + '\0';
            char[] bufferin = str.ToCharArray();
     
            int i, j, k;
            int noteCount = 0;
            int flag = 0;
            char[] note = new char[100];

            /*注释*/
            for (i = 0; bufferin[i] != '\0'; i++)
            {
                if (bufferin[i] == '"')
                {
                    flag = 1 - flag;
                    continue;
                }
                if (bufferin[i] == '/' && flag == 0)
                {
                    if (bufferin[i + 1] == '/')
                    {
                        for (j = i; bufferin[j] != '\0'; j++)
                        {
                            note[noteCount++] = bufferin[j];
                        }
                        note[noteCount] = '\0';
                        noteCount = 0;
                        bufferin[i] = '\0';

                        string strTemp = "";
                        for (int ii = 0; ii < note.Length; ii++)
                        {
                            if (note[ii] == '\0')
                            {
                                break;
                            }
                            else
                                strTemp = strTemp + note[ii];
                        }
                        javaWords.Add(new JavaWord(strTemp,0)); //注释
                        bufferin[i] = '\0';
                        break;
                    }

                    if (bufferin[i + 1] == '*')
                    {
                        note[noteCount++] = '/';
                        note[noteCount++] = '*';
                        for (j = i + 2; bufferin[j] != '\0'; j++)
                        {
                            note[noteCount++] = bufferin[j];
                            if (bufferin[j] == '*' && bufferin[j + 1] == '/')
                            {
                                j += 2;
                                note[noteCount++] = bufferin[j];
                                note[noteCount] = '\0';
                                noteCount = 0;
                                bufferin[i] = '\0';

                                string strTemp = "";
                                for (int ii = 0; ii < note.Length; ii++)
                                {
                                    if (note[ii] == '\0')
                                    {
                                        break;
                                    }
                                    else
                                        strTemp = strTemp + note[ii];
                                }
                                javaWords.Add(new JavaWord(strTemp, 0));//注释
                            
                                break;
                            }
                        }
                        for (; bufferin[j] != '\0'; j++, i++)
                        {
                            bufferin[i] = bufferin[j];
                        }
                        if (bufferin[j] == '\0')
                        {
                            bufferin[i] = '\0';
                        }
                    }
                }
            }

            /*空格*/
            for (i = 0, flag = 0; bufferin[i] != '\0'; i++)
            {
                if (bufferin[i] == '"')
                {
                    flag = 1 - flag;
                    continue;
                }
                if (bufferin[i] == ' ' && flag == 0)
                {
                    for (j = i + 1; bufferin[j] != '\0' && bufferin[j] == ' '; j++)
                    {
                    }
                    if (bufferin[j] == '\0')
                    {
                        bufferin[i] = '\0';
                        break;
                    }
                    if (bufferin[j] != '\0' && ((spaces(bufferin[j]) == 1) || (i > 0 && spaces(bufferin[i - 1]) == 1)))
                    {
                        for (k = i; bufferin[j] != '\0'; j++, k++)
                        {
                            bufferin[k] = bufferin[j];
                        }
                        bufferin[k] = '\0';
                        i--;
                    }
                }
            }

            /*制表符*/
            for (i = 0, flag = 0; bufferin[i] != '\0'; i++)
            {
                if (bufferin[i] == '\t')
                {
                    for (j = i; bufferin[j] != '\0'; j++)
                    {
                        bufferin[j] = bufferin[j + 1];
                    }
                    i = -1;
                }
            }

            string strResult = "";
            for (i = 0; i < bufferin.Length; i++)
            {
                if (bufferin[i] == '\0')
                {
                    break;
                }
                else
                    strResult = strResult + bufferin[i];
            }

            return strResult; ;

        }


        /*******************判断是否为可消除空格的字符/操作符*******************/
        int spaces(char c)
        {
            if ((c > 'z' || (c < 'a' && c > 'Z') || (c < 'A' && c > '9') || (c < '0')) && c != '_' && c != '$')
            {
                return 1;
            }
            return 0;
        }


        /*******************判断是否为字母*******************/
        int characters(char c)
        {
            if ((c <= 'z' && c >= 'a') || (c <= 'Z' && c >= 'A'))
            {
                return 1;
            }
            return 0;
        }

        /*******************判断是否为数字*******************/
        int numbers(char c)
        {
            if (c <= '9' && c >= '0')
            {
                return 1;
            }
            return 0;
        }

        /*******************判断是否为整型*******************/
        public int integers(string str)
        {
            int i;
            if (str[0] == '-' || numbers(str[0]) == 1)
            {
                for (i = 0; i < str.Length; i++)
                {
                    if (str[i] == '.')
                    {
                        return 0;
                    }
                    if ((str[i] == 'x' || str[i] == 'X') && (((str[0] == '-' || str[0] == '+') && (str[1] != '0' || i > 2)) || (str[0] != '-' && str[0] != '+' && (str[0] != '0' || i > 1))))
                    {
                        return 0;
                    }
                    if ((i < str.Length - 1) && numbers(str[i]) == 0 && str[i] != 'x' && str[i] != 'X')
                    {
                        if (str.Length > 2 && String.Equals("0x", str.Substring(0, 2)) == true || String.Equals("-0x", str.Substring(0, 3)) == true)
                        {
                            if (str[i] >= 'A' && str[i] <= 'F')
                            {
                                continue;
                            }
                        }
                        return 0;
                    }
                    if ((i == str.Length - 1) && numbers(str[i]) == 0 && str[i] != 'L')
                    {
                        if (str.Length > 2 && String.Equals("0x", str.Substring(0, 2)) == true || String.Equals("-0x", str.Substring(0, 3)) == true)
                        {
                            if (str[i] >= 'A' && str[i] <= 'F')
                            {
                                continue;
                            }
                        }
                        return 0;
                    }
                }
                return 1;
            }
            return 0;
        }
        /*******************判断是否为浮点型*******************/
        public int floats(string str)
        {
            int i;
            int flag = 0;
            if (str[0] == '-' || numbers(str[0]) == 1)
            {
                for (i = 0; i < str.Length; i++)
                {
                    if (str[i] == '.')
                    {
                        if (flag == 0)
                        {
                            flag = 1;
                            continue;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    if ((str[i] == 'x' || str[i] == 'X') && (((str[0] == '-' || str[0] == '+') && (str[1] != '0' || i > 2)) || (str[0] != '-' && str[0] != '+' && (str[0] != '0' || i > 1))))
                    {
                        return 0;
                    }
                    if (numbers(str[i]) == 0 && str[i] != 'x' && str[i] != 'X')
                    {
                        if (str.Length > 2 && String.Equals("0x", str.Substring(0, 2)) == true || String.Equals("-0x", str.Substring(0, 3)) == true)
                        {
                            if (str[i] >= 'A' && str[i] <= 'F')
                            {
                                continue;
                            }
                        }
                        return 0;
                    }
                }
                return flag;
            }
            return 0;
        }
        /*******************判断字符类型*******************/
        public int charkind(char c)
        {
            if (characters(c) == 1)
            {
                return 1;
            }
            if (numbers(c) == 1)
            {
                return 2;
            }
            if (c == '$' || c == '_')
            {
                return 3;
            }
            if (c == '\\')
            {
                return 4;
            }
            if (c == '=')
            {
                return 5;
            }
            return 0;
        }
        /*******************判断是否为关键字*******************/
        public int keyword(string str)
        {
            int i;
            for (i = 0; i < 50; i++)
            {
                if (String.Equals(str, keywords[i]) == true)
                {
                    return 1;
                }
            }
            return 0;
        }
        /*******************判断是否为标识符*******************/
        public int signwords(string str)
        {
            int i;
            if (str[0] == '$' || str[0] == '_' || characters(str[0]) == 1)
            {
                for (i = 0; i < str.Length; i++)
                {
                    if (spaces(str[i]) == 1)
                    {
                        return 0;
                    }
                }
                return 1;
            }
            return 0;
        }

        /*******************获取单词类型*******************/
        public void wordkind(string str)
        {
            int i, j, k;
            int flag = 0;

            /*判断是否为关键字或标识符*/
            if (keyword(str) == 1)
            {
                javaWords.Add(new JavaWord(str, 2));  //布尔变量
                if (String.Equals(str, "true") == true || String.Equals(str, "false") == true){}
                else{}
            }
            else if (signwords(str) == 1)
            {
                javaWords.Add(new JavaWord(str, 1));//标识符
            }
            else if (integers(str) == 1)
            {
                javaWords.Add(new JavaWord(str, 1));//整型
        
            }
            else if (floats(str) == 1)
            {
                javaWords.Add(new JavaWord(str, 1));//浮点型
             
            }
            else if (str[0] == '\'' && str[str.Length - 1] == '\'')
            {
                javaWords.Add(new JavaWord(str, 1));//字符型
               
            }
            else if (str[0] == '"' && str[str.Length - 1] == '"')
            {
                javaWords.Add(new JavaWord(str, 5));//字符串
           
            }
            else if (spaces(str[0]) == 1 && str[0] != '"' && str[0] != '\'')
            {
                
                if (String.Equals(str, "<") == true || String.Equals(str, ">") == true || String.Equals(str, "<=") == true || String.Equals(str, ">=") == true)
                {
                    javaWords.Add(new JavaWord(str, 1));//【< > <= >=】
               	
                }
                else if (String.Equals(str, "<<") == true || String.Equals(str, ">>") == true || String.Equals(str, ">>>") == true || String.Equals(str, "<<<") == true)
                {
                    javaWords.Add(new JavaWord(str, 1));//【<< >> <<< >>>】
                  
                }
                else if (str.Contains('=') == true)
                {
                    javaWords.Add(new JavaWord(str, 1));//【== !=】 ,【特殊符号】
                    if (String.Equals(str, "==") == true || String.Equals(str, "!=") == true)
                    { }
                    else
                    {

                    }
                }
                else if (String.Equals(str, "||") == true)
                {
                    javaWords.Add(new JavaWord(str, 1));//【||】
                 
                }
                else if (String.Equals(str, "&&") == true)
                {
                    javaWords.Add(new JavaWord(str, 1));//【&&】
             
                }
                else if (String.Equals(str, "++") == true || String.Equals(str, "--") == true || String.Equals(str, "!") == true || String.Equals(str, "~") == true)
                {
                    javaWords.Add(new JavaWord(str, 1));//【++ -- +(正) -(负) ! ~】
                   
                }
                else if (str.Length == 1)
                {
                    switch (str[0])
                    {
                        case '?':
                        case ':': javaWords.Add(new JavaWord(str, 1)); break;
                        case ' ': javaWords.Add(new JavaWord(str, 6)); break;
                        case '{':
                        case '}': javaWords.Add(new JavaWord(str, 1)); break;
                        case '[':
                        case ']':
                        case '(':
                        case ')':
                        case '.': javaWords.Add(new JavaWord(str, 1)); break;
                        case ',': javaWords.Add(new JavaWord(str, 1)); break;
                        case ';': javaWords.Add(new JavaWord(str, 1)); break;
                        case '+':
                        case '-': javaWords.Add(new JavaWord(str, 1)); break;
                        case '*':
                        case '/':
                        case '%': javaWords.Add(new JavaWord(str, 1)); break;
                        case '|': javaWords.Add(new JavaWord(str, 1)); break;
                        case '^': javaWords.Add(new JavaWord(str, 1)); break;
                        case '&': javaWords.Add(new JavaWord(str, 1)); break;
                        default: javaWords.Add(new JavaWord(str, 1)); break;
                    }
                }
            }
            else
            {
                javaWords.Add(new JavaWord(str, 1));
             
            }
        }


    }
}
