﻿using System;
using System.Threading;

using static Console_game.NativeMethods;

namespace Console_game
{
    public static class ConsoleListener
    {
        internal static event ConsoleMouseEvent MouseEvent;

        internal static event ConsoleKeyEvent KeyEvent;

        internal static event ConsoleWindowBufferSizeEvent WindowBufferSizeEvent;

        private static bool Run = false;
        private static bool ThreadExists = false;

        public static void Start()
        {
            if (!Run && !ThreadExists)
            {
                Run = true;

                IntPtr inHandle = GetStdHandle(STD_INPUT_HANDLE);
				uint mode = 0;
				// Setting some shit
				GetConsoleMode(inHandle, ref mode);
				mode &= ~ENABLE_QUICK_EDIT_MODE; //disable
				mode |= ENABLE_WINDOW_INPUT;	 //enable
				mode |= ENABLE_MOUSE_INPUT;		 //enable
				SetConsoleMode(inHandle, mode);

                new Thread(() =>
                {
                    while (true)
                    {
                        uint numRead = 0;
                        INPUT_RECORD[] record = new INPUT_RECORD[1];
                        record[0] = new INPUT_RECORD();
                        ReadConsoleInput(inHandle, record, 1, ref numRead);
                        if (Run)
                            switch (record[0].EventType)
                            {
                                case INPUT_RECORD.MOUSE_EVENT:
                                    MouseEvent?.Invoke(record[0].MouseEvent);
                                    break;
                                case INPUT_RECORD.KEY_EVENT:
                                    KeyEvent?.Invoke(record[0].KeyEvent);
                                    break;
                                case INPUT_RECORD.WINDOW_BUFFER_SIZE_EVENT:
                                    WindowBufferSizeEvent?.Invoke(record[0].WindowBufferSizeEvent);
                                    break;
                            }
                        else
                        {
                            uint numWritten = 0;
                            WriteConsoleInput(inHandle, record, 1, ref numWritten);
                            return;
                        }
                    }
                }).Start();
                ThreadExists = true;
            }
            else
            {
                Continue();
            }
        }

        public static void Continue() => Run = true;

        public static void Stop() => Run = false;

        internal delegate void ConsoleMouseEvent(MOUSE_EVENT_RECORD r);

        internal delegate void ConsoleKeyEvent(KEY_EVENT_RECORD r);

        internal delegate void ConsoleWindowBufferSizeEvent(WINDOW_BUFFER_SIZE_RECORD r);
    }
}