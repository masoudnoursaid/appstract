#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8625
#pragma warning disable CS0414
//Resharper disable all

namespace Application.Common.Utils
{
    public class Minify
    {
        private const int EOF = -1;
        private string mErr = "";
        private string mFileName = "";
        private bool mIsError;
        private string mModifiedData = "";
        private string mOriginalData = "";
        private BinaryReader mReader;

        /// <summary>
        ///     Constructor - does all the processing
        /// </summary>
        /// <param name="f">file path</param>
        public Minify(string f)
        {
            try
            {
                if (File.Exists(f))
                {
                    mFileName = f;

                    StreamReader rdr = new StreamReader(mFileName);
                    mOriginalData = rdr.ReadToEnd();
                    rdr.Close();

                    mReader = new BinaryReader(new FileStream(mFileName, FileMode.Open));
                    DoProcess();
                    mReader.Close();

                    string outFile = mFileName + ".min";
                    StreamWriter wrt = new StreamWriter(outFile);
                    wrt.Write(mModifiedData);
                    wrt.Close();
                }
                else
                {
                    mIsError = true;
                    mErr = "File does not exist";
                }
            }
            catch (Exception ex)
            {
                mIsError = true;
                mErr = ex.Message;
            }
        }

        /// <summary>
        ///     Main process
        /// </summary>
        private void DoProcess()
        {
            int lastChar = 1;
            int thisChar = -1;
            int nextChar = -1;
            bool endProcess = false;
            bool ignore = false;
            bool inComment = false;
            bool isDoubleSlashComment = false;


            while (!endProcess)
            {
                endProcess = (mReader.PeekChar() == -1);
                if (endProcess)
                    break;

                ignore = false;
                thisChar = mReader.ReadByte();

                if (thisChar == '\t')
                    thisChar = ' ';
                else if (thisChar == '\t')
                    thisChar = '\n';
                else if (thisChar == '\r')
                    thisChar = '\n';

                if (thisChar == '\n')
                    ignore = true;

                if (thisChar == ' ')
                {
                    if ((lastChar == ' ') || IsDelimiter(lastChar) == 1)
                        ignore = true;
                    else
                    {
                        endProcess = (mReader.PeekChar() == -1);
                        if (!endProcess)
                        {
                            nextChar = mReader.PeekChar();
                            if (IsDelimiter(nextChar) == 1)
                                ignore = true;
                        }
                    }
                }


                if (thisChar == '/')
                {
                    nextChar = mReader.PeekChar();
                    if (nextChar == '/' || nextChar == '*')
                    {
                        ignore = true;
                        inComment = true;
                        if (nextChar == '/')
                            isDoubleSlashComment = true;
                        else
                            isDoubleSlashComment = false;
                    }
                }

                if (inComment)
                {
                    while (true)
                    {
                        thisChar = mReader.ReadByte();
                        if (thisChar == '*')
                        {
                            nextChar = mReader.PeekChar();
                            if (nextChar == '/')
                            {
                                thisChar = mReader.ReadByte();
                                inComment = false;
                                break;
                            }
                        }

                        if (isDoubleSlashComment && thisChar == '\n')
                        {
                            inComment = false;
                            break;
                        }
                    }

                    ignore = true;
                }


                if (!ignore)
                    AddToOutput(thisChar);

                lastChar = thisChar;
            }
        }


        /// <summary>
        ///     Add character to modified data string
        /// </summary>
        /// <param name="c">char to add</param>
        private void AddToOutput(int c)
        {
            mModifiedData += (char)c;
        }


        /// <summary>
        ///     Original data from file
        /// </summary>
        /// <returns></returns>
        public string GetOriginalData()
        {
            return mOriginalData;
        }

        /// <summary>
        ///     Modified data after processing
        /// </summary>
        /// <returns></returns>
        public string GetModifiedData()
        {
            return mModifiedData;
        }

        /// <summary>
        ///     Check if a byte is alphanumeric
        /// </summary>
        /// <param name="c">byte to check</param>
        /// <returns>retval - 1 if yes. else 0</returns>
        private int IsAlphanumeric(int c)
        {
            int retval = 0;

            if ((c >= 'a' && c <= 'z') ||
                (c >= '0' && c <= '9') ||
                (c >= 'A' && c <= 'Z') ||
                c == '_' || c == '$' || c == '\\' || c > 126)
                retval = 1;

            return retval;
        }

        /// <summary>
        ///     Check if a byte is a delimiter
        /// </summary>
        /// <param name="c">byte to check</param>
        /// <returns>retval - 1 if yes. else 0</returns>
        private int IsDelimiter(int c)
        {
            int retval = 0;

            if (c == '(' || c == ',' || c == '=' || c == ':' ||
                c == '[' || c == '!' || c == '&' || c == '|' ||
                c == '?' || c == '+' || c == '-' || c == '~' ||
                c == '*' || c == '/' || c == '{' || c == '\n' ||
                c == ','
               )
            {
                retval = 1;
            }

            return retval;
        }
    }
}